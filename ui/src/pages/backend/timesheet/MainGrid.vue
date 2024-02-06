<script setup lang="ts">
import { client } from "@/api"
import { TimeSheet,
    QueryTimeSheets, 
    UpdateTimeSheet, CreateTimeSheet, 
    DeleteTimeSheet
} from "@/dtos"

import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import { Form as KForm } from "@progress/kendo-vue-form";
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Button as kButton} from '@progress/kendo-vue-buttons'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForm from './EditForm.vue'
import { appUser } from "@/auth"

// const props = 
defineProps<{
    // selectedEmpTimeSheetId: any,
    // EmpTimeSheetList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

const memberEditRef = ref<InstanceType<typeof KForm>>()
let kDialogTitle = ref<string>("Add EmpTimeSheet")

let mainData = ref<TimeSheet[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let dataItemInEdit = ref<TimeSheet>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
//   { cell: responsiveCellTemplate, filterable: false, title: 'Banks', hidden: true },
//   { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'tsDate', title: 'TS Date', cell: 'datePickerTemplate', format:'DD-MMM-yyyy' , width:130},
  { field: 'clientCode', title: 'Client'},
  { field: 'projectCode', title: 'Project Code'},
  { field: 'projectName', title: 'Project Name'},
  { field: 'projectTaskId', title: 'Project Task'},
  { field: 'notes', title: 'Full Name'},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];


const updateSelectedParentParam = () => {
    refreshDatas() 
}

const refreshDatas = async () => {
  const mainQuery = new QueryTimeSheets({
      appUserId: appUser.value.id
  }) 

  const api = await client.api(mainQuery)
  if (api.succeeded) {
    mainData.value = api.response!.results ?? []
    gridData.data = process(mainData.value, {
      sort: sort.value,
      filter: filter.value
    }).data;
    gridData.total = process(mainData.value,{}).total;
    dataItemInEdit.value = undefined
  }
}
const onInsert = () => {
  kDialogTitle.value = "Add Family Member"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteTimeSheet({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete CBank', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Family Member"
  dataItemInEdit.value = e.dataItem
}
const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}
const onResetForm = () => {
  // console.log('onResetForm')
  // console.log(memberEditRef.value)

  memberEditRef.value?.resetForm()
}


const onSave = async (dataItem: any) => {
  const currData = dataItem;

  if( currData.id == null) {
    const request = new CreateTimeSheet({
      tsDate: currData.tsDate,
      appUserId: currData.appUserId,
      clientId: currData.clientId,
      projectId: currData.projectId,
      projectTaskId: currData.projectTaskId,
      notes: currData.notes
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Add Data', 'Successfully added new data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Data', 'Failed to add new data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Data', 'Failed to add new data')
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
      projectTaskId: currData.projectTaskId,
      notes: currData.notes
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update Data', 'Successfully update data 🎉')
    } 
  }
}

const formAllowSubmit = computed(() => {
  var isAllowSubmit = false
  if(memberEditRef.value)
  {
    isAllowSubmit = memberEditRef.value.form.modified
    if(isAllowSubmit) {
      isAllowSubmit = memberEditRef.value?.form.valid
    }
  }
  return isAllowSubmit
})

defineExpose({
    updateSelectedParentParam,
    refreshDatas
})

</script>

<template>
  <kDialog v-if="dataItemInEdit" @close="onCancelChanges" :title-render="'myTemplate'" width="90%" >
      <template v-slot:myTemplate="{}">
          <div class="w-100">
            {{ kDialogTitle }} 
            <kButton class="float-end" :theme-color="'warning'" icon="reset"  @click="onResetForm" title="Reset Data">Reset</kButton>
          </div>
      </template>
      <KForm id="memberEditForm" @submit="onSave" :initial-values="dataItemInEdit" ref="memberEditRef"> 
        <div style="max-height: 400px; overflow-y: auto;" class="pl-2 pr-2">
        <EditForm :data-item="dataItemInEdit"></EditForm>
      </div>
      </KForm>
      <kDialogActionsBar>
      <kButton :theme-color="'secondary'" @click="onCancelChanges">Cancel</kButton>
      <kButton :theme-color="'primary'" :type="'submit'" :disabled="!formAllowSubmit">Save</kButton>
      </kDialogActionsBar>
  </kDialog>
  
    
    <!-- END Kendo Dialog for Editing Data -->
    <KStandardGrid 
        :responsive-column-title="'Events'"
        :grid-data="gridData.data" 
        :grid-colum-properties="gridColumProperties" 
        :grid-data-total="gridData.total" 
        :grid-export-file-name="'EventList'" 
        :filterable="filterable"
        :sortable="sortable"
        :pageable="pageable"
        :show-export-button="showExportButton"
        @onCancelChanges="onCancelChanges"
        @refreshData="refreshDatas"
        @onInsert="onInsert"
        @onRemove="onRemove"
        @onEdit="onEdit"
        @onSave="onSave"
    />
</template>