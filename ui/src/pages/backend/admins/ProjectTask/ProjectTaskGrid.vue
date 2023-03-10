<script lang="ts">
export default defineComponent({
  inheritAttrs: false,
});
</script>
<script setup lang="ts" name="ProjectGrid">
// import { formatDate } from "@/utils"
// import { toDateFmt } from '@servicestack/client'
import { client } from "@/api"
import { ProjectTaskView, QueryProjectTasks, CreateProjectTask, UpdateProjectTask, DeleteProjectTask} from "@/dtos"
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import ProjectTaskEditForm from './ProjectTaskEditForm.vue'

const editFormRef = ref<InstanceType<typeof ProjectTaskEditForm>>()

 const props = 
defineProps<{
    selectedProjectId: any,
    clientList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
}>()

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add Project Task")

onMounted(async () => {
  updateselectedProjectId(props.selectedProjectId)
});

let sourceData = ref<ProjectTaskView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
let dataItemInEdit = ref<ProjectTaskView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const responsiveCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const elParentInfoTitle = h('strong',{}, ['Project'])
  const elParentInfo = h('p',{},[props.dataItem.projectCode + ' - ' + props.dataItem.projectName])
  
  const elDataInfoTitle = h('strong',{}, ['Task'])
  const elDataInfo = h('p',{},[props.dataItem.taskName + ' - ' + props.dataItem.taskName])

  return h(tdElement, {}, elParentInfoTitle, elParentInfo, elDataInfoTitle, elDataInfo);
}

const projectCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  return h(tdElement, {}, [props.dataItem.projectCode + ' - ' + props.dataItem.projectName])
}

let currselectedProjectId = ref<number | undefined>()
const updateselectedProjectId = (val: any) => {
  currselectedProjectId.value = val ?? 0
  skip.value = 0
  // // Show Hide Client Columns
  // gridColumProperties.map( col => {
  //   if(col.title == "Client") {
  //     col.hidden = !(!currselectedProjectId.value)
  //   }
  // })

  refreshDatas() 
}

let gridColumProperties = [
  { cell: responsiveCellTemplate, filterable: false, title: 'ProjectTasks', hidden: true },
  { cell: projectCellTemplate, filterable: false, title: 'Project'},
  { field: 'no', title: 'No'},
  { field: 'taskName', title: 'Name'},
  { field: 'status', title: 'Status'},
//   { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async () => {
  const queryProjects = new QueryProjectTasks({ projectId: currselectedProjectId.value, take: take.value, skip: skip.value}) 
  const api = await client.api(queryProjects)
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
    const request = new DeleteProjectTask({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas()
      // showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Project Task"
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
    const request = new CreateProjectTask({
      projectId: currData.projectId,
      no : currData.no,
      taskName : currData.taskName,
      description: currData.description,
      durationDays: currData.durationDays,
      planStart: currData.planStart,
      planEnd: currData.planEnd,
      actualStart: currData.actualStart,
      actualEnd: currData.actualEnd,
      completed : currData.completed,
      status : currData.status,
      projectTeamId : currData.projectTeamId
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
    const request = new UpdateProjectTask({
      id : currData.id,
      projectId: currData.projectId,
      no : currData.no,
      taskName : currData.taskName,
      description: currData.description,
      durationDays: currData.durationDays,
      planStart: currData.planStart,
      planEnd: currData.planEnd,
      actualStart: currData.actualStart,
      actualEnd: currData.actualEnd,
      completed : currData.completed,
      status : currData.status,
      projectTeamId : currData.projectTeamId
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
    } 
  }
}

defineExpose({
    updateselectedProjectId,
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
        <ProjectTaskEditForm ref="editFormRef" :data-item="dataItemInEdit" @save="onSave"  />
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