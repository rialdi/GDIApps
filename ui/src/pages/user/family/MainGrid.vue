<script setup lang="ts">
import { client } from "@/api"
import { FAMILY_MEMBER_TYPE, EmpFamilyMember,
    QueryEmpFamilyMembers, UpdateEmpFamilyMember, CreateEmpFamilyMember, DeleteEmpFamilyMember 
} from "@/dtos"

import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForms from './EditForms.vue'
import { appUser } from "@/auth"

const editFormsRef = ref<InstanceType<typeof EditForms>>()

// const props = 
defineProps<{
    // selectedEmpFamilyMemberId: any,
    // EmpFamilyMemberList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

let kDialogTitle = ref<string>("Add EmpFamilyMember")

let mainData = ref<EmpFamilyMember[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let dataItemInEdit = ref<EmpFamilyMember>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
//   { cell: responsiveCellTemplate, filterable: false, title: 'Banks', hidden: true },
//   { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'code', title: 'Code', width:150 },
  { field: 'name', title: 'Name'},
  { field: 'eventStatus', title: 'Status'},
//   { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

let currSelectedEventStatus = ref<string | undefined>()
let currSelectedCode= ref<string | undefined>()
let currSelectedName= ref<EVENT_STATUS | undefined>()
const updateSelectedParentParam = (code: any, name: any, eventStatus: any) => {
    currSelectedCode.value = code
    currSelectedName.value = name
    currSelectedEventStatus.value = eventStatus
    refreshDatas() 
}

const refreshDatas = async () => {
    const mainQuery = new QueryEmpFamilyMembers({
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
  kDialogTitle.value = "Add Event"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteEmpFamilyMember({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete CBank', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Event"
  dataItemInEdit.value = e.dataItem
  // editFormRef.value?.focus
}
const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}
const onResetForm = () => {
  editFormsRef.value?.resetForm()
}
const onSave = async (e: any) => {
  const currData = e.dataItem;
  const startTime = ""
  const endTime = ""
//   const selectedEmpFamilyMemberCode = ""
  if( currData.id == null) {
    const request = new CreateEmpFamilyMember({
        code: currData.code,
        name: currData.name,
        isTBD: currData.isTBD,
        startTime: startTime,
        endTime: endTime,
        timeZone: currData.timeZone,
        calDescription: currData.calDescription,
        calEventOrganizer: currData.calEventOrganizer,
        calEventOrganizerEmail: currData.calEventOrganizerEmail,
        isLocationDecided: currData.isLocationDecided,
        locationType: currData.locationType,
        gMapsUrl: currData.gMapsUrl,
        locationAddress: currData.locationAddress,
        virtualMeetingLink: currData.virtualMeetingLink,
        eventStatus: currData.eventStatus,
        isActive: currData.isActive,
        emailTemplateText: currData.emailTemplateText,
        htmlTemplate: currData.htmlTemplate
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
    const request = new UpdateEmpFamilyMember({
      id : currData.id,
      code: currData.code,
        name: currData.name,
        isTBD: currData.isTBD,
        startTime: startTime,
        endTime: endTime,
        timeZone: currData.timeZone,
        calDescription: currData.calDescription,
        calEventOrganizer: currData.calEventOrganizer,
        calEventOrganizerEmail: currData.calEventOrganizerEmail,
        isLocationDecided: currData.isLocationDecided,
        locationType: currData.locationType,
        gMapsUrl: currData.gMapsUrl,
        locationAddress: currData.locationAddress,
        virtualMeetingLink: currData.virtualMeetingLink,
        eventStatus: currData.eventStatus,
        isActive: currData.isActive,
        emailTemplateText: currData.emailTemplateText,
        htmlTemplate: currData.htmlTemplate
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
        <EditForms ref="editFormsRef" :data-item="dataItemInEdit" @save="onSave" />
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