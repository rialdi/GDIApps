<script setup lang="ts">
import { client } from "@/api"
// import { AppUser } from "@/dtos"
import { 
    ProjectPlan, QueryProjectPlans, 
    CreateProjectPlan, UpdateProjectPlan, DeleteProjectPlan, 
    // GetUserList
} from "@/dtos"
import { showNotifError } from '@/stores/commons'
// import { showNotifError, showNotifSuccess } from '@/stores/commons'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import { GridColumnProps, GridDataStateChangeEvent } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query';

const props = 
defineProps<{
    selectedProjectId: any,
    selectedVersionNo: any,
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    groupable?: boolean,
    showExportButton?: boolean,
}>()

onMounted(async () => {
  await refreshDatas()
});


let ProjectPlanData = ref<ProjectPlan[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const titleCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const codeTitle = h('pre',{}, [props.dataItem.codeTitle])
  return h(tdElement, {}, codeTitle)
}


//disable column when edit
function isEditable(e : any){
	// var dataSource = $("#grid").data("kendoGrid").dataSource;
	return (e.hasChild == true);
}


// function isEditable (e: any){
//   console.log('isEditable')
//   console.log(e)
//     return (!e.hasChild);
// }

let gridColumProperties = [
    { cell: titleCellTemplate, title: 'Task Code', width:135},
    { field: 'taskTitle', title: 'Task Title'},
    { field: 'dependecyTaskCode', title: 'Dependency Task Code'},
    { field: 'durationDays', title: 'Duration (days)', width:130, editable: isEditable},
    { field: 'startDate', title: 'Start', cell: 'datePickerTemplate', format:'DD-MMM-yyyy' , width:130},
    { field: 'endDate', title: 'End', cell: 'datePickerTemplate', format:'DD-MMM-yyyy', width:130 },
    { field: 'completedPercentage', title: 'Completed', format:"{0:p0}" , width:130},
    { field: 'resourceCost', title: 'Cost', format:"{0:n0}", width:130},
    { field: 'hasChild', title: 'Has Child', width:130},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];


let currSelectedProjectId = ref<number | undefined>()
let currSelectedVersionNo = ref<number | undefined>()
const refresGridData = (projectId: any, versionNo: any) => {
    currSelectedProjectId.value = projectId
    currSelectedVersionNo.value = versionNo
    skip.value = 0
    refreshDatas() 
}

const refreshDatas = async () => {
    const queryProjectPlans = new QueryProjectPlans({ 
        projectId: currSelectedProjectId.value, 
        versionNo: currSelectedVersionNo.value, 
        take: take.value, 
        skip: skip.value,
    })

    const api = await client.api(queryProjectPlans)
    if (api.succeeded) {
        ProjectPlanData.value = api.response!.results ?? []
        gridData.data = process(ProjectPlanData.value, {
        sort: sort.value,
        filter: filter.value,
        // group: groups.value,
    }).data;
    gridData.total = api.response!.total as number
  }
}

const createAppState = (dataState: any) => {
    take.value = dataState.take;
    skip.value = dataState.skip;
    refreshDatas()
}

const onDataStateChange = (event: GridDataStateChangeEvent) => {
  createAppState(event.data);
}

const pageChangeHandler = (e: any) => {
  take.value = e.page.take
  skip.value = e.page.skip
  refreshDatas()
}

const onItemChange = (e: any) => {
    console.log('onItemChange')
    console.log(e)
  if (e.dataItem.id) {
    let index = gridData.data.findIndex(p => p.id === e.dataItem.id);
    // let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    let updated = Object.assign({},gridData.data[index], {[e.field]:e.value});
    gridData.data.splice(index, 1, updated);
  } else {
    e.dataItem[e.field] = e.value;
  }
}

const onInsert = () => {
    const dataItem = { projectId: props.selectedProjectId, inEdit: true };
    gridData.data.splice(0, 0, dataItem)
}
const onRemove = async(e: any) => {
    const currData = e;
    if( currData !== null) {
        // let index = gridData.data.findIndex((p: { id: any; }) => p.id === currData.id);
        // gridData.data.splice(index, 1);
        const request = new DeleteProjectPlan({
        id: currData.id
        });
        const api = await client.api(request)
        if (api.succeeded) {
            refreshDatas();
        // showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
        }
    }
}
const onEdit = (e: any) => {
    // currAppUser.value = e.dataItem.appUserId;
    let index = gridData.data.findIndex(p => p.id === e.dataItem.id);
    let updated = Object.assign({},gridData.data[index], {inEdit:true});
    if(gridData.data[index]["hasChild"])
    {
      console.log(e)
      console.log(gridData.data[index])
      // e.container.find("input[name='durationDays']").attr('disabled',true);
      console.log("Has Child")
    }
    gridData.data.splice(index, 1, updated);
}



const onCancelChanges = () => {
  refreshDatas()
}

const onSave = async (e: any) => {
  // console.log(e);
  const currData = e;
    console.log('save')
    console.log(currData);
  if( currData.id == null) {
    const request = new CreateProjectPlan({
      projectId: currData.projectId,
      appUserId: currData.appUserId,
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        // showNotifSuccess('Add Project', 'Successfully added new Project data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Project Team', 'Failed to add new Project Team data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Project Team', 'Failed to add new Project Team data')
      }
    }
  }
  else{
    const request = new UpdateProjectPlan({
      id : currData.id,
      projectId: currData.projectId,
      appUserId: currData.appUserId,
      versionNo: currData.versionNo,
      taskLevel: currData.taskLevel,
      taskNo: currData.taskNo,
      parentCode: currData.parentCode,
      taskCode: currData.taskCode,
      dependecyTaskCode: currData.dependecyTaskCode,
      taskTitle: currData.taskTitle,
      durationDays: currData.durationDays,
      startDate: currData.startDate,
      endDate: currData.endDate,
      completedPercentage: currData.completedPercentage,
      resourceCost: currData.resourceCost
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        // showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
    } 
  }
}

defineExpose({
  refresGridData,
  refreshDatas
})

</script>
<template> 
    <KStandardGrid 
        :responsive-column-title="'ProjectPlans'"
        :grid-data="gridData.data" 
        :grid-colum-properties="gridColumProperties" 
        :grid-data-total="gridData.total" 
        :grid-export-file-name="'ProjectPlan'" 
        :show-export-button="showExportButton"
        :filterable="filterable"
        :sortable="sortable"
        :pageable="pageable"
        :is-inline-edit="true"
        :edit-field="'inEdit'"
        @onItemChange="onItemChange"
        @onDataStateChange="onDataStateChange"
        @pagechange="pageChangeHandler"
        @onCancelChanges="onCancelChanges"
        @refreshData="refreshDatas"
        @onInsert="onInsert"
        @onRemove="onRemove"
        @onEdit="onEdit"
        @onSave="onSave"
    />
</template> 
