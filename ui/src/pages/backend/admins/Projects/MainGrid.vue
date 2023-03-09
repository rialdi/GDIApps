<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="60%" height="80%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ kDialogTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" title="Reset Data"></kButton>
            </div>
        </template>
        <EditForm ref="editFormRef" :data-item="dataItemInEdit" @save="onSave" :client-list="clientList" />
        <kDialogActionsBar  v-if="editFormRef?.showFormSubmitButton">
          <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
          <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
          <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" title="Save"> Save </kButton>
        </kDialogActionsBar>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
     
    <!-- Main Data Grid -->
    <KStandardGrid 
        :responsive-column-title="'Projects'"
        :grid-data="gridData.data" 
        :grid-colum-properties="gridColumProperties" 
        :grid-data-total="gridData.total" 
        :grid-export-file-name="'Projects'" 
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
<script setup lang="ts">
// import { formatDate } from "@/utils"
// import { toDateFmt } from '@servicestack/client'
import { client } from "@/api"
import { ProjectView, QueryProjects, CreateProject, UpdateProject, DeleteProject } from "@/dtos"
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForm from './EditForm.vue'

const editFormRef = ref<InstanceType<typeof EditForm>>()

// const props = 
defineProps<{
    selectedClientId: any,
    clientList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
}>()

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add Project")

onMounted(async () => {
  await refreshDatas()
});

let ProjectData = ref<ProjectView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
let dataItemInEdit = ref<ProjectView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const responsiveCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const elParentInfoTitle = h('strong',{}, ['Client'])
  const elParentInfo = h('p',{},[props.dataItem.clientCode + ' - ' + props.dataItem.clientName])
  
  const elDataInfoTitle = h('strong',{}, ['Bank'])
  const elDataInfo = h('p',{},[props.dataItem.bankName + ' - ' + props.dataItem.accountNo])

  return h(tdElement, {}, elParentInfoTitle, elParentInfo, elDataInfoTitle, elDataInfo);
}

const clientCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  return h(tdElement, {}, [props.dataItem.clientCode + ' - ' + props.dataItem.clientName])
}

let currSelectedClientId = ref<number | undefined>()
const updateSelectedClientId = (val: any) => {
  currSelectedClientId.value = val
  skip.value = 0
  // Show Hide Client Columns
  gridColumProperties.map( col => {
    if(col.title == "Client") {
      col.hidden = !(!currSelectedClientId.value)
    }
  })

  refreshDatas() 
}

let gridColumProperties = [
  { cell: responsiveCellTemplate, filterable: false, title: 'Projects', hidden: true },
  { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'code', title: 'Code', width:150 },
  { field: 'name', title: 'Name'},
//   { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { field: 'isActive', title: 'Is Main', cell: 'isActiveTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async () => {
  const currClientId = currSelectedClientId.value
  const queryProjects = (currClientId) ? 
    new QueryProjects({ clientId: currClientId, take: take.value, skip: skip.value}) : 
    new QueryProjects({ take: take.value, skip: skip.value }) 

  const api = await client.api(queryProjects)
  if (api.succeeded) {
    ProjectData.value = api.response!.results ?? []
    gridData.data = process(ProjectData.value, {
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
  kDialogTitle.value = "Add Project"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteProject({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Project"
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
    const request = new CreateProject({
      clientId: currData.clientId,
      cContractId : currData.cContractId,
      code : currData.code,
      name : currData.name,
      description: currData.description,
      durationDays: currData.durationDays,
      projectOwner: currData.projectOwner,
      projectManager: currData.projectManager,
      estimatedStartDate: currData.estimatedStartDate,
      estimatedEndDate: currData.estimatedEndDate,
      actualtartDate : currData.actualStartDate,
      actualEndDate : currData.actualEndDate,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Add Project', 'Successfully added new Project data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Project', 'Failed to add new Project data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Project', 'Failed to add new Project data')
      }
    }
  }
  else{
    const request = new UpdateProject({
      id : currData.id,
      clientId: currData.clientId,
      cContractId : currData.cContractId,
      code : currData.code,
      name : currData.name,
      description: currData.description,
      durationDays: currData.durationDays,
      projectOwner: currData.projectOwner,
      projectManager: currData.projectManager,
      estimatedStartDate: currData.estimatedStartDate,
      estimatedEndDate: currData.estimatedEndDate,
      actualtartDate : currData.actualStartDate,
      actualEndDate : currData.actualEndDate,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
    } 
  }
}

defineExpose({
    updateSelectedClientId,
    refreshDatas
})

</script>