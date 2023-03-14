<script lang="ts">
export default defineComponent({
  inheritAttrs: false,
});
</script>
<script setup lang="ts" name="TimeSheetGrid">
import { formatDate } from "@/utils"
// import { toDateFmt } from '@servicestack/client'
import { client } from "@/api"
import { TimeSheetView, QueryTimeSheets, CreateTimeSheet, UpdateTimeSheet, DeleteTimeSheet} from "@/dtos"
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import TimeSheetEditForm from './TimeSheetEditForm.vue'

const editFormRef = ref<InstanceType<typeof TimeSheetEditForm>>()

// const props = 
defineProps<{
    selectedClientId?: any,
    selectedProjectId?: any,
    // clientIdList?: number[],
    startDate? : Date,
    endDate? : Date,
    userIdList?: number[],
    // clientList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
}>()

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add Time Sheet")

onMounted(async () => {
  // resetGrid(props.selectedClientId, props.selectedProjectId)
  // updateselectedProjectId(props.selectedProjectId)
});

let clientIdList = ref<number[]>([])
let projectIdList = ref<number[]>([])
let tsDate = ref<string[]>([])
let userIdList = ref<number[]>([])
let sourceData = ref<TimeSheetView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
let dataItemInEdit = ref<TimeSheetView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const responsiveCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const elClientInfoTitle = h('strong',{}, ['Client'])
  const elClientInfo = h('p',{},[props.dataItem.clientCode + ' - ' + props.dataItem.clientName])

  const elProjectInfoTitle = h('strong',{}, ['Project'])
  const elProjectInfo = h('p',{},[props.dataItem.projectCode + ' - ' + props.dataItem.projectName])

  const elDataInfoTitle = h('strong',{}, ['Task'])
  const elDataInfo = h('p',{},[formatDate(props.dataItem.tsDate, "DD MMM YYYY") + ' - ' + props.dataItem.taskName])

  return h(tdElement, {}, elClientInfoTitle, elClientInfo, 
    elProjectInfoTitle, elProjectInfo, elDataInfoTitle, elDataInfo);
}

const tsDateCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  return h(tdElement, {}, [ formatDate(props.dataItem.tsDate, "DD MMM YYYY") ])
}

const clientProjectCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const elClientInfoTitle = h('strong',{}, ['Client'])
  const elClientInfo = h('p',{},[props.dataItem.clientCode + ' - ' + props.dataItem.clientName])

  const elProjectInfoTitle = h('strong',{}, ['Project'])
  const elProjectInfo = h('p',{},[props.dataItem.projectCode + ' - ' + props.dataItem.projectName])

  return h(tdElement, {}, elClientInfoTitle, elClientInfo, 
    elProjectInfoTitle, elProjectInfo);

  return h(tdElement, {}, [props.dataItem.clientCode + ' - ' + props.dataItem.clientName])
}

// const projectCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
//   return h(tdElement, {}, [props.dataItem.projectCode + ' - ' + props.dataItem.projectName])
// }

let gridColumProperties = [
  { cell: responsiveCellTemplate, filterable: false, title: 'ProjectTasks', hidden: true },
  { cell: clientProjectCellTemplate, filterable: false, title: 'Client Project'},
  // { cell: projectCellTemplate, filterable: false, title: 'Project'},
  { cell: tsDateCellTemplate, field:"tsDate", title: 'Date', width:120},
  { field: 'no', title: 'No', width:50},
  { field: 'taskName', title: 'Task Name'},
//   { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const resetGrid = (clientId: any, projectId: any, startDate: any, endDate: any) => {
  clientIdList.value = []
  projectIdList.value = []
  tsDate.value = []

  if(clientId != undefined) clientIdList.value.push(clientId)
  if(projectId != undefined) projectIdList.value.push(projectId)

  if(startDate != undefined) tsDate.value.push(startDate)
  if(endDate != undefined) tsDate.value.push(endDate)

  skip.value = 0
  refreshDatas()
}

const refreshDatas = async () => {
  const queryMain = new QueryTimeSheets(
    { 
        clientIds: clientIdList.value,
        projectIds: projectIdList.value,
        appUserIds: userIdList.value,
        tsDateBetween: tsDate.value,
        take: take.value, skip: skip.value
  }) 
  const api = await client.api(queryMain)
  if (api.succeeded) {
    sourceData.value = api.response!.results ?? []
    gridData.data = process(sourceData.value, {
      sort: sort.value,
      filter: filter.value,
    }).data;
    gridData.total = api.response!.total as number
    dataItemInEdit.value = undefined
  }
}

const pageChangeHandler = (e : any) => {
    take.value = e.page.take
    skip.value = e.page.skip
    refreshDatas()
}

const onInsert = () => {
  kDialogTitle.value = "Add Project Task"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    const request = new DeleteTimeSheet({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas()
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Time Sheet"
  dataItemInEdit.value = e.dataItem
  // editFormRef.value?.focus
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}

const onResetForm = () => {
  // editFormRef.value?.resetForm()
  editFormRef.value?.resetForm()
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
  // console.log("Country : " + currData.country)
  // return
  if( currData.id == null) {
    const request = new CreateTimeSheet({
        tsDate: currData.tsDate,
        appUserId: currData.appUserId,
        clientId: currData.clientId,
        projectId: currData.projectId,
        no : currData.no,
        taskName : currData.taskName,
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Add Project Task', 'Successfully added new Project data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Project Task', 'Failed to add new Project data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Project Task', 'Failed to add new Project data')
      }
    }
  }
  else{
    const request = new UpdateTimeSheet({
        id : currData.id,
        tsDate: currData.tsDate,
        appUserId: currData.appUserId,
        clientId: currData.clientId,
        projectId: currData.projectId,
        no : currData.no,
        taskName : currData.taskName,
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update TimeSheet', 'Successfully updated TimeSheet data 🎉')
    } 
  }
}

defineExpose({
  resetGrid,
    refreshDatas
})

</script>
<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="60%" height="80%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ kDialogTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" title="Reset Data"></kButton>
            </div>
        </template>
        <TimeSheetEditForm ref="editFormRef" :data-item="dataItemInEdit" @save="onSave"  />
        <kDialogActionsBar  v-if="editFormRef?.showFormSubmitButton">
          <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
          <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
          <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" title="Save"> Save </kButton>
        </kDialogActionsBar>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
     
    <!-- Main Data Grid -->
    <KStandardGrid 
        :responsive-column-title="'ProjectTasks'"
        :grid-data="gridData.data" 
        :grid-colum-properties="gridColumProperties" 
        :grid-data-total="gridData.total" 
        :grid-export-file-name="'ProjectTasks'" 
        :filterable="filterable"
        :sortable="sortable"
        :pageable="pageable"
        :skip="skip"
        :take="take"
        :show-export-button="showExportButton"
        @pagechange="pageChangeHandler"
        @onCancelChanges="onCancelChanges"
        @refreshData="refreshDatas"
        @onInsert="onInsert"
        @onRemove="onRemove"
        @onEdit="onEdit"
        @onSave="onSave"
    />
    <!-- End Main Data Grid -->
</template> 