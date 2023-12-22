<script lang="ts" setup>
import { computed } from "vue";
import { client } from "@/api"
// import { AppUser } from "@/dtos"
import { 
    ProjectPlan, QueryProjectPlans, 
    // CreateProjectPlan, UpdateProjectPlan, DeleteProjectPlan, 
    // GetUserList
} from "@/dtos"

import { Grid, GridToolbar } from "@progress/kendo-vue-grid";
import { Button as KButton } from "@progress/kendo-vue-buttons";
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query';

// import KGridTimePickerCell from "@/components/grids/KGridTimePickerCell.vue"
import KGridDatePickerCell from "@/components/grids/KGridDatePickerCell.vue"


let ProjectPlanData = ref<ProjectPlan[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
let selectedField = ref<string | undefined>('selected')
let selectedID = ref<string | undefined>('1')
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const titleCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const codeTitle = h('pre',{}, [props.dataItem.codeTitle])
  return h(tdElement, {}, codeTitle)
}

let gridColumProperties = [
    { cell: titleCellTemplate, title: 'Task Code', width:135},
    { field: 'taskLevel', title: 'Task Level', editable:false},
    { field: 'taskNo', title: 'Task No', editable:false},
    { field: 'parentCode', title: 'Parent Code', editable:false},
    { field: 'taskCode', title: 'Task Code', editable:false},
    { field: 'taskTitle', title: 'Task Title'},
    { field: 'dependecyTaskCode', title: 'Dependency Task Code'},
    { field: 'durationDays', title: 'Duration (days)', width:130},
    { field: 'startDate', title: 'Start', cell: 'datePickerTemplate', format:'DD-MMM-yyyy' , width:130},
    // { field: 'endDate', title: 'End', cell: 'datePickerTemplate', format:'DD-MMM-yyyy', width:130 },
    { field: 'completedPercentage', title: 'Completed', format:"{0:p0}" , width:130, editor: "numeric",},
    { field: 'resourceCost', title: 'Cost', format:"{0:n0}", width:130, editor: "numeric",},
    { field: 'hasChild', title: 'Has Child', width:130, editor: "boolean"},
//   { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];


let currSelectedProjectId = ref<number | undefined>()
let currSelectedVersionNo = ref<number | undefined>()
let changes = ref<boolean>(false)
let editField = ref<string | undefined>(undefined)


const selectedItem = computed(() => {
  return gridData.data.find((item) => item.taskCode === selectedID.value)
})

const getMaxTaskNo = () => {
  return selectedItem && selectedItem.value.taskLevel
  // var taskLevel = 
  
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

      if(gridData.data.length > 0) {
        // selectedID.value = gridData.data[0].taskCode
        gridData.data[0].selected = true
      }
    }
}

const refresGridData = (projectId: any, versionNo: any) => {
    currSelectedProjectId.value = projectId
    currSelectedVersionNo.value = versionNo
    skip.value = 0
    refreshDatas() 
}

onMounted(async () => {
});

const saveChanges = (e: any) => {
    console.log("saveChanges")
    var updatedData = gridData.data.filter(data => data.isModified)
    console.log(updatedData)
    ProjectPlanData.value.splice(0, ProjectPlanData.value.length, ...gridData.data);
    gridData.data = ProjectPlanData.value.map((product) =>
        Object.assign({}, product)
      );
      console.log(gridData)
    editField.value = undefined;
    changes.value = false;
    exitEdit(undefined, true);
}

const cancelChanges = (e: any) => {
    changes.value = false;
    exitEdit(undefined, true);
}

const exitEdit = (dataItem: any, exitEdit: boolean) => {
    if (!exitEdit && dataItem.inEdit) {
        return;
      }
      gridData.data.forEach((d) => {
        if (d.inEdit) {
          d.inEdit = undefined;
        }
      });
      editField.value = undefined;
}

const itemChange = (e: any) => {
    if(e.dataItem[e.field] != e.value)
    {
        e.dataItem.isModified = true;
    }
    e.dataItem[e.field] = e.value;
    changes.value = true;
}

const rowClick = (e: any) => {
  selectedID.value = e.dataItem.taskCode
    gridData.data.forEach((d) => {
      // d.dataItem.selected = d.dataItem.taskCode === selectedID.value
      if (e.dataItem === d) {
        d.selected = true
      } 
      else {
        d.selected = false
      }
        if (d.inEdit) {
            if (e.dataItem !== d) {
              d.inEdit = undefined;
            }
        }
    });
}

const cellClick = (e: any) => {
    // console.log("e.dataItem")
    // console.log(e.dataItem)
    console.log(e.field)
    if(e.dataItem.hasChild && e.field != "taskTitle") return;

    if (e.dataItem.inEdit && e.field === editField.value) return;
        
    exitEdit(e.dataItem, true);
    editField.value = e.field;
    e.dataItem.inEdit = e.field;
    
}

const addTask = (e: any) => {
  var parentCode = ""
  var dependecyTaskCode = ""
  
  var inLevelLastTaskNo = 0
  var currTaskLevel = 1
  var currTaskNo = 1
  let prevTaskDataItem = ref<ProjectPlan>()

  if(prevTaskDataItem.value != undefined)
  {
    inLevelLastTaskNo = prevTaskDataItem.value.taskNo ?? 0
    currTaskNo = inLevelLastTaskNo + 1
    parentCode = prevTaskDataItem.value.parentCode ?? ""
    if(parentCode != "")
    {
      let parentDataItem = ref<ProjectPlan>()
      if(parentDataItem.value != undefined)
      {
        var parentTaskLevel = parentDataItem.value.taskLevel ?? 0
        currTaskLevel = parentTaskLevel + 1
      }
    }
  }

  const dataItem = { 
    projectId: currSelectedProjectId, 
    versionNo: currSelectedVersionNo, 
    parentCode: parentCode,
    taskLevel: currTaskLevel,
    taskNo: currTaskNo,
    dependecyTaskCode: dependecyTaskCode,
    durationDays: 1,
    inEdit: false 
  };
  gridData.data.splice(0, 0, dataItem)
}

const addChildTask = (e: any) => {
  var parentCode = selectedItem && selectedItem.value.taskCode
  var currTaskLevel = getMaxTaskNo() + 1

  var selectedTaskLevelData = gridData.data.filter(data => data.taskLevel === currTaskLevel && data.parentCode === parentCode)
  
  var maxTaskNo = 0
  if(selectedTaskLevelData.length > 0) {
    maxTaskNo = Math.max(...selectedTaskLevelData.map(property => property.taskNo))
  }

  // let index = gridData.data.findIndex(data => data.taskLevel === currTaskLevel && data.parentCode === parentCode && data.taskNo === maxTaskNo) + 1;
  
  var currTaskNo = maxTaskNo + 1
  var currTaskCode = parentCode + '.' + + currTaskNo
  var dependecyTaskCode = ""
  const dataItem = { 
    projectId: currSelectedProjectId.value, 
    versionNo: currSelectedVersionNo.value, 
    parentCode: parentCode,
    taskLevel: currTaskLevel,
    taskNo: currTaskNo,
    taskCode: currTaskCode,
    dependecyTaskCode: dependecyTaskCode,
    durationDays: 1,
    inEdit: false 
  };
  gridData.data.splice(0, 0, dataItem)

  let newGridData = gridData.data.sort(function(a, b){
		if(a.taskCode > b.taskCode) { return 1; }
		if(a.taskCode < b.taskCode) { return -1; }
		return 0;
	});	


      // gridData.data.slice().sort(function(a, b) {
      //   if (a.taskCode > b.taskCode)
      //                   return -1;
      //               if (a.taskCode < b.taskCode)
      //                   return 1;
      //               return 0;
      // })

  console.log(gridData.data)
  console.log(newGridData)
  // gridData.data = gridData.data.orderBy(gridData.data, 'taskCode')

  // console.log("currTaskLevel = " + currTaskLevel)
  // console.log("parentCode = " + parentCode)
  // console.log("maxTaskNo = " + maxTaskNo)
  // console.log("currTaskNo = " + currTaskNo)
  // console.log("currTaskCode = " + currTaskCode)
}

defineExpose({
  refresGridData
})


</script>
<template>
  Selected Item:  {{selectedItem && selectedItem.taskCode}} , {{selectedItem && selectedItem.taskTitle}}
    <Grid
    ref="grid"
    :data-items="gridData"
    :edit-field="'inEdit'"
    :selected-field="selectedField"
    @rowclick="rowClick"
    @cellclick="cellClick"
    @itemchange="itemChange"
    :columns="gridColumProperties"
  >
    <GridToolbar>
      <KButton title="Add" @click="addTask">
        Add Task
      </KButton>
      <KButton title="AddChild" @click="addChildTask">
        Add Child Task
      </KButton>
      <KButton title="Save Changes" @click="saveChanges" :disabled="!changes">
        Save Changes
      </KButton>
      <KButton
        title="Cancel Changes"
        @click="cancelChanges"
        :disabled="!changes"
      >
        Cancel Changes
      </KButton>
    </GridToolbar>
    <template v-slot:datePickerTemplate="{ props }">
    <td>
      <KGridDatePickerCell
        :field="props.field"
        :data-item="props.dataItem"
        :format="props.format"
      >
      </KGridDatePickerCell> 
    </td>
  </template>
  </Grid>
</template>