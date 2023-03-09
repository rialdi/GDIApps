<template> 
    <kGrid
      :edit-field="'inEdit'"
          :data-items="gridData.data"
          :columns="gridColumProperties"
          @itemchange="onGridDataitemChange"
      >
      <kGridToolbar class="k-form w-100">
          <kButton icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'>Add New</kButton>
      </kGridToolbar>
      <template v-slot:myDropDownCell="{props}">
        <KGriDropDownCell
          :item-list="teamRole"
          :data-item="props.dataItem"
          :field="props.field"
          @change="(e) => roleChange(e, props.field, props.dataItem)"
        />
      </template>
      <template v-slot:appUserIdTemplate="{props}">
          <PopupSearchGrid :id="props.id" 
            :grid-colum-properties="gridColumPropertiesUserList" 
            :grid-source-data="userList" 
            :pageable="true"
            :filterable="true"
            :model-value-text-field="'userName'"
            :mode="'gridcell'"
            :gridcell-in-edit="props.dataItem.inEdit"
            :gridcell-field="props.field" 
            v-model="props.dataItem[props.field]"
            />

            <!-- @change="(e) => userChange(e, 'appUserId', props.dataItem)" -->
      </template>
      <template v-slot:actionTemplate="{props}">
          <CommandCell :data-item="props.dataItem" 
                  @edit="onEdit"
                  @save="onSave" 
                  @remove="onRemove"
                  @cancel="onCancelChanges"/>
      </template>
    </kGrid>
    <!-- End Main Data Grid -->
</template> 
<script setup lang="ts">
// import { formatDate } from "@/utils"
// import { toDateFmt } from '@servicestack/client'
import { client } from "@/api"
import { AppUser } from "@/dtos"
import { ProjectTeam, QueryProjectTeams, CreateProjectTeam, UpdateProjectTeam, DeleteProjectTeam, 
  PROJECT_TEAM_ROLE, GetUserList
} from "@/dtos"
import { showNotifError } from '@/stores/commons'
// import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'
import { Button as kButton} from '@progress/kendo-vue-buttons'
import CommandCell from '@/layouts/partials/KGridCommandCell.vue'
import KGriDropDownCell from "@/components/grids/KGridDropDownCell.vue"
import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"
// import { DropDownList as kDropdownList} from '@progress/kendo-vue-dropdowns';

const props = 
defineProps<{
    selectedProjectId: any,
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
}>()

onMounted(async () => {
  await refreshDatas()
});

let currAppUser = ref<string |undefined>()
const teamRole = Object.values(PROJECT_TEAM_ROLE)

let userList = ref<AppUser[]>([])
let gridColumPropertiesUserList = [
// { field: 'id', title: 'Team Name' },
  { field: 'userName', title: 'User Name' },
  { field: 'email', title: 'Email'},
] as GridColumnProps[];

const getUserListLookup = async() => {
  const api = await client.api(new GetUserList())
  if (api.succeeded) {
    userList.value = api.response! as any[]?? []
  }
}

// const userChange = (e : any, field: any, dataItem : any) => {
//   let index = gridData.data.findIndex(p => p.id === dataItem.id);
//   let updated = Object.assign({},gridData.data[index], {[field]:e.id});
//   gridData.data.splice(index, 1, updated);
// }

onMounted( () => {
  getUserListLookup()
})

let ProjectTeamData = ref<ProjectTeam[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let take = ref<number | undefined>(10)
let skip = ref<number | undefined>(0)
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
  { field: 'appUserId', title: 'Team Name', cell: 'appUserIdTemplate' },
  { field: 'projectTeamRole', title: 'Role', cell: 'myDropDownCell'},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const roleChange = (e : any, field: any, dataItem : any) => {
    let index = gridData.data.findIndex(p => p.id === dataItem.id);
    let updated = Object.assign({},gridData.data[index], {[field]:e.value});
    gridData.data.splice(index, 1, updated);
}

let currSelectedProjectId = ref<number | undefined>()
const updateSelectedProjectId = (val: any) => {
    currSelectedProjectId.value = val
    skip.value = 0
    refreshDatas() 
}

const refreshDatas = async () => {
const queryProjectTeams = new QueryProjectTeams({ projectId: currSelectedProjectId.value, take: take.value, skip: skip.value})

  const api = await client.api(queryProjectTeams)
  if (api.succeeded) {
    ProjectTeamData.value = api.response!.results ?? []
    gridData.data = process(ProjectTeamData.value, {
      sort: sort.value,
      filter: filter.value,
    }).data;
    gridData.total = api.response!.total as number
    // dataItemInEdit.value = undefined
  }
}

const onGridDataitemChange = (e : any) => {
    if (e.dataItem.id) {
        let index = gridData.data.findIndex(p => p.id === e.dataItem.id);
        let updated = Object.assign({},gridData.data[index], {[e.field]:e.value});
        gridData.data.splice(index, 1, updated);
    } else {
        e.dataItem[e.field] = e.value;
    }
}

const onInsert = () => {
    const dataItem = { projectId: props.selectedProjectId, inEdit: true };
    gridData.data.splice(0, 0, dataItem)
  // Set Default Value
//   dataItemInEdit.value = {
//   }
}
const onRemove = async(e: any) => {
    const currData = e;
  if( currData !== null) {
    // let index = gridData.data.findIndex((p: { id: any; }) => p.id === currData.id);
    // gridData.data.splice(index, 1);
    const request = new DeleteProjectTeam({
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
//   dataItemInEdit.value = e.dataItem
currAppUser.value = e.dataItem.appUserId;
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
    const request = new CreateProjectTeam({
      projectId: currData.projectId,
      appUserId: currData.appUserId,
      projectTeamRole: currData.projectTeamRole
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
    const request = new UpdateProjectTeam({
      id : currData.id,
      projectId: currData.projectId,
      appUserId: currData.appUserId,
      projectTeamRole: currData.projectTeamRole
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        // showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
    } 
  }
}

defineExpose({
    updateSelectedProjectId,
    refreshDatas
})

</script>