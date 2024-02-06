<script setup lang="ts">
import { client } from "@/api"
import { DailyScrumMeeting,
    QueryDailyScrumMeetings, UpdateDailyScrumMeeting, CreateDailyScrumMeeting, DeleteDailyScrumMeeting 
} from "@/dtos"

import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForm from './EditForm.vue'
import { auth } from "@/auth"

const editFormRef = ref<InstanceType<typeof EditForm>>()

// const props = 
defineProps<{
    // selectedDailyScrumMeetingId: any,
    // DailyScrumMeetingList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

let kDialogTitle = ref<string>("Add DailyScrumMeeting")

let mainData = ref<DailyScrumMeeting[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let dataItemInEdit = ref<DailyScrumMeeting>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
//   { cell: responsiveCellTemplate, filterable: false, title: 'Banks', hidden: true },
//   { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'blockers', title: 'Blocker'},
//   { field: 'name', title: 'Name'},
//   { field: 'eventStatus', title: 'Status'},
//   { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

// let currSelectedEventStatus = ref<string | undefined>()
// let currSelectedCode= ref<string | undefined>()
// let currSelectedName= ref<EVENT_STATUS | undefined>()
const updateSelectedParentParam = () => {
    // currSelectedCode.value = code
    // currSelectedName.value = name
    // currSelectedEventStatus.value = eventStatus
    refreshDatas() 
}

const refreshDatas = async () => {
    const mainQuery = new QueryDailyScrumMeetings({
    //   codeContains: currSelectedCode.value,
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
  kDialogTitle.value = "Add Daily Scrumm Meeting"
  if(auth.value)
  {
    var appUserId = auth.value.userId ? Number(auth.value.userId) : 0
    // Set Default Value
    dataItemInEdit.value = new DailyScrumMeeting({appUserId : appUserId})
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteDailyScrumMeeting({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete CBank', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Daily Scrumm Meeting"
  dataItemInEdit.value = e.dataItem
  // editFormRef.value?.focus
}
const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}
const onResetForm = () => {
    editFormRef.value?.resetForm()
}
const onSave = async (e: any) => {
  const currData = e.dataItem

//   const selectedDailyScrumMeetingCode = ""
  if( currData.id == null) {
    const request = new CreateDailyScrumMeeting({
        appUserId: currData.appUserId,
        meetingDate: currData.meetingDate,
        blockers: currData.blockers,
        whatDidYouDoYesterday: currData.whatDidYouDoYesterday,
        whatGoalsToday: currData.whatGoalsToday
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
    const request = new UpdateDailyScrumMeeting({
      id : currData.id,
      appUserId: currData.appUserId,
        meetingDate: currData.meetingDate,
        blockers: currData.blockers,
        whatDidYouDoYesterday: currData.whatDidYouDoYesterday,
        whatGoalsToday: currData.whatGoalsToday
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update Data', 'Successfully update data 🎉')
    } 
  }
}

defineExpose({
    updateSelectedParentParam,
    refreshDatas
})

</script>


<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="60%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ kDialogTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" title="Reset Data"></kButton>
            </div>
        </template>
        <EditForm ref="editFormRef" :data-item="dataItemInEdit" @save="onSave" />
        <!-- <kForm :initialValues="dataItemInEdit" @submit="onSave">
            <EditForm :client-list="props.clientList" ref="editFormRef"/>
        </kForm> -->
        <kDialogActionsBar>
        <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
        <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
        <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" title="Save"> Save </kButton>
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