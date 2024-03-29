<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="500" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
            {{ kDialogTitle }} 
            <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" ></kButton>
            <!-- <span class="k-icon k-i-reset float-end" v-on:click="onResetForm"/> -->
            </div>
        </template>
        <kForm :initialValues="dataItemInEdit" @submit="onSave">
            <!-- <EditForm :client-list="props.clientList" ref="editFormRef"/> -->
        </kForm>
        <kDialogActionsBar>
        <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
        <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
        </kDialogActionsBar>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
    
    <!-- Main Data Grid -->
    <kGrid ref="grid"
        :data-items="gridData"
        :sortable="props.sortable"
        :pageable="props.pageable"
        :total="total"
        :filterable="props.filterable"
        :filter="filter"
        @filterchange="filterChangeHandler"
        @sortchange="sortChangeHandler"
        :columns="gridColumProperties"
    >
    <kGridToolbar>
        <kButton icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'>Add New</kButton>
        <kButton v-if="props.showExportButton" icon="excel" title="Export to Excel" :theme-color="'primary'" @click='onExportToExcel'>Export to Excel</kButton>
    </kGridToolbar>
    <template v-slot:actionTemplate="{props}">
        <CommandCell :data-item="props.dataItem" 
                @edit="onEdit"
                @save="onSave" 
                @remove="onRemove"
                @cancel="onCancelChanges"/>
    </template>
    <template v-slot:responsiveTemplate="{ props }">
        <td :colspan="1">
        <strong>Email</strong>
        <p>
        {{ props.dataItem.Email }}
        </p>
        <strong>Name</strong>
        <p>
        {{ props.dataItem.fullName }}
        </p>
        </td>
    </template>
    <template v-slot:isActiveTemplate="{ props }">
        <td :colspan="1" style="text-align:center">
        <input type="checkbox" id="isActive" v-model="props.dataItem.isActive" :disabled="!props.dataItem.inEdit"/>
        </td>
    </template>
    </kGrid>
    <!-- End Main Data Grid -->
</template>
<script setup lang="ts">
import { client } from "@/api"
import { AppUser, GetUserList } from "@/dtos"
// import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Form as kForm } from "@progress/kendo-vue-form"
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { saveExcel } from '@progress/kendo-vue-excel-export';
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'
// import { SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import CommandCell from '@/layouts/partials/KGridCommandCell.vue'
// import { App } from "vue"


const props = defineProps<{
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

function useBreakpoints() {
  let windowWidth = ref(window.innerWidth)

  const onWidthChange = () => windowWidth.value = window.innerWidth
  
  onMounted(() => window.addEventListener('resize', onWidthChange))
  onUnmounted(() => window.removeEventListener('resize', onWidthChange))

  const currWindowType = computed(() => {
    if (windowWidth.value < 550) return 'xs'
    if (windowWidth.value >= 550 && windowWidth.value < 1200) return 'md'
    if (windowWidth.value >= 1200) return 'lg'
    return null;
  })

  const width = computed(() =>  windowWidth.value)
  return { width, currWindowType }
}

let lastWindowType = ref<string | null>("lg")
const { width, currWindowType } = useBreakpoints()

const onShowHideColumns = () => {
  if(currWindowType.value != lastWindowType.value && width.value > 100) {
    gridColumProperties.map( col => {
      if(col.title != "Action") {
        if(col.title == "User"){
          col.hidden = !col.hidden
        } else {
          col.hidden = !col.hidden
        }
      }
    })
    lastWindowType.value = currWindowType.value
    refreshDatas()
  }
}

onMounted(async () => {
  window.addEventListener('resize', onShowHideColumns)
  await refreshDatas()
});

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add User")


let AppUserList = ref<AppUser[]>([])

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>(10)

let dataItemInEdit = ref<AppUser>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
  { cell: 'responsiveTemplate', filterable: false, title: 'User', hidden: true },
  { field: 'email', title: 'Email', width:150 },
  { field: 'firstName', title: 'Name'},
  { field: 'phoneNumber', title: 'Phone No'},
  { field: 'roles', title: 'Role'},
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async () => {
  
  console.log('Get User List')
  const api = await client.api(new GetUserList())
  if (api.succeeded) {
    AppUserList.value = api.response! ?? []
    gridData.data = process(AppUserList.value, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(AppUserList.value,{}).total;
    dataItemInEdit.value = undefined
  }
}

const onInsert = () => {
  kDialogTitle.value = "Add Project"
  // Set Default Value
  dataItemInEdit.value = {
    // code: '', name: "Project Name", 
    // isActive: true
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    // const request = new DeleteProject({
    //   id: e.dataItem.id
    // });
    // const api = await client.api(request)
    // if (api.succeeded) {
    //   // showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
    // }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Project"
  dataItemInEdit.value = e.dataItem
//   editFormRef.value?.focus
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}

const onResetForm = () => {
//   editFormRef.value?.resetForm()
}

const onSave = async (e: any) => {
//   const currData = e;
  // console.log(currData)
//   if( currData.id == null) {
//     const request = new CreateProject({
//       clientId: currData.clientId,
//       code: currData.code,
//       name : currData.name,
//       description : currData.description,
//       isActive : currData.isActive
//     })
//     const api = await client.api(request)
//     if (api.succeeded) {
//         refreshDatas(props.selectedClientId)
//         showNotifSuccess('Add Project', 'Successfully added new Project data 🎉')
//     } else {
//       if(api.error != null) {
//         showNotifError('Add Project', 'Failed to add new Project data.<br/>' + api.error.message)
//       }
//       else {
//         showNotifError('Add Project', 'Failed to add new Project data')
//       }
//     }
//   }
//   else{
//     const request = new UpdateProject({
//       id : currData.id,
//       clientId: currData.clientId,
//       code: currData.code,
//       name : currData.name,
//       description : currData.description,
//       isActive : currData.isActive
//     })
//     const api = await client.api(request)
//     if (api.succeeded) {
//         refreshDatas(props.selectedClientId)
//         showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
//     } 
//   }
}
const sortChangeHandler = (e: any) => {
  if(e.sort.length > 0)
  {
    let existingSortItem = sort.value
    if(existingSortItem != undefined && existingSortItem?.length > 0)
    {
      existingSortItem[0].dir = (existingSortItem[0].dir === "asc") ? "desc" : "asc" 
      sort.value = existingSortItem
    }
    else{
      e.sort[0].dir = (e.sort[0].dir === "asc") ? "desc" : "asc" 
      sort.value = e.sort;
    }
    refreshDatas()
  }
}

const filterChangeHandler = (e: any) => {
  filter.value = e.filter
  refreshDatas()
}

const onExportToExcel = () => {
  saveExcel({
      data: gridData.data as any[],
      fileName: "myFile",
      columns: gridColumProperties
  });
}

defineExpose({
    refreshDatas
})

</script>