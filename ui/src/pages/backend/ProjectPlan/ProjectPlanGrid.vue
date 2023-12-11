<script setup lang="ts">
import { client } from "@/api"
// import { formatDate } from "@/utils"
// import { AppUser } from "@/dtos"
import { 
    ProjectPlan, QueryProjectPlans, 
    CreateProjectPlan, UpdateProjectPlan, DeleteProjectPlan, 
    // GetUserList
} from "@/dtos"
import { showNotifError } from '@/stores/commons'
// import { showNotifError, showNotifSuccess } from '@/stores/commons'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult, GroupDescriptor } from '@progress/kendo-data-query'

// import { Button as kButton} from '@progress/kendo-vue-buttons'
// import KGriDropDownCell from "@/components/grids/KGridDropDownCell.vue"
// import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"
// import { DropDownList as kDropdownList} from '@progress/kendo-vue-dropdowns';

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
//   getUserListLookup()
});

// let currAppUser = ref<string |undefined>()

// let userList = ref<AppUser[]>([])
// let gridColumPropertiesUserList = [
// // { field: 'id', title: 'Team Name' },
//   { field: 'userName', title: 'User Name' },
//   { field: 'email', title: 'Email'},
// ] as GridColumnProps[];

// const getUserListLookup = async() => {
//   const api = await client.api(new GetUserList())
//   if (api.succeeded) {
//     userList.value = api.response! as any[]?? []
//   }
// }


let ProjectPlanData = ref<ProjectPlan[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});
const groups = ref<GroupDescriptor[] | undefined>(
    [
        { field: "group1" }, 
        { field: "group2" }, 
        { field: "group3" }, 
        { field: "group4" }
    ]
);

// const groups = ref<GroupDescriptor[] | undefined>(
//     [
        
//         // { field: "taskLevel" }, 
//         { field: "parentCode" },
//         { field: "taskCode" }, 
//     ]
// );

// const allGroupedData = ref<any[]>([])
// const collapsedGroups = ref<any[]>([])

// const groups = ref<GroupDescriptor[] | undefined>(
//     [
//         { field: "taskLevel" }
//     ]
// );

// const startDateCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
//   return h(tdElement, {}, [ formatDate(props.dataItem.startDate, "DD MMM YYYY") ])
// }

// const endDateCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
//   return h(tdElement, {}, [ formatDate(props.dataItem.endDate, "DD MMM YYYY") ])
// }

let gridColumProperties = [
    // { field: 'parentCode', title: 'Parent Code'},
    // { field: 'id', title: 'Id'},
    // { field: 'taskLevel', title: 'Level'},
    // { field: 'parentCode', title: 'parentCode'},
    // { field: 'taskNo', title: 'taskNo'},
    // { field: 'group1', title: 'Group 1'},
    // { field: 'group2', title: 'Group 2'},
    // { field: 'group3', title: 'Group 3'},
    // { field: 'group4', title: 'Group 4'},
    { field: 'codeTitle', title: 'Title'},
    { field: 'durationDays', title: 'Duration (days)'},
    // { cell: startDateCellTemplate, title: 'Start'},
    // { cell: endDateCellTemplate, title: 'End'},
    { field: 'completedPercentage', title: 'Completed', format:"{0:p0}" },
//   { field: 'appUserId', title: 'Team Name', cell: 'appUserIdTemplate' },
//   { field: 'ProjectPlanRole', title: 'Role', cell: 'myDropDownCell'},
//   { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];


let currSelectedProjectId = ref<number | undefined>()
const updateSelectedProjectId = (val: any) => {
    currSelectedProjectId.value = val
    skip.value = 0
    refreshDatas() 
}

const refreshDatas = async () => {
    const queryProjectPlans = new QueryProjectPlans({ 
        projectId: currSelectedProjectId.value, 
        versionNo: currSelectedProjectId.value, 
        take: take.value, 
        skip: skip.value,
    })

    const api = await client.api(queryProjectPlans)
    if (api.succeeded) {
        ProjectPlanData.value = api.response!.results ?? []
        gridData.data = process(ProjectPlanData.value, {
        sort: sort.value,
        filter: filter.value,
        group: groups.value,
    }).data;
    gridData.total = api.response!.total as number
  }
}

// const onGridDataitemChange = (e : any) => {
//     if (e.dataItem.id) {
//         let index = gridData.data.findIndex(p => p.id === e.dataItem.id);
//         let updated = Object.assign({},gridData.data[index], {[e.field]:e.value});
//         gridData.data.splice(index, 1, updated);
//     } else {
//         e.dataItem[e.field] = e.value;
//     }
// }

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
    gridData.data.splice(index, 1, updated);
}

const onCancelChanges = () => {
  refreshDatas()
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
    // console.log('save')
    // console.log(currData);
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
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        // showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
    } 
  }
}

const createAppState = (dataState: any) => {
    groups.value = dataState.group;
    take.value = dataState.take;
    skip.value = dataState.skip;
    refreshDatas()
}

const onDataStateChange = (e: any) => {
    createAppState(e.data)
}
const onExpandChange = (e: any) => {
    e.dataItem[e.target.$props.expandField] = e.value;
}

defineExpose({
    updateSelectedProjectId,
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
        :filterable="filterable"
        :sortable="sortable"
        :pageable="pageable"
        :groupable="groupable"
        :group="groups"
        :expand-field="'expanded'"
        :show-export-button="showExportButton"
        @onCancelChanges="onCancelChanges"
        @refreshData="refreshDatas"
        @onInsert="onInsert"
        @onRemove="onRemove"
        @onEdit="onEdit"
        @onSave="onSave"
        @expandchange="onExpandChange"
        @onDataStateChange="onDataStateChange"
    />
</template> 
