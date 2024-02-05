<script setup lang="ts">
import { client } from "@/api"
import { EmpFamilyMember,
  GENDER, FAMILY_MEMBER_TYPE, LIVING_STATUS,
    QueryEmpFamilyMembers, 
    UpdateEmpFamilyMember, CreateEmpFamilyMember, 
    DeleteEmpFamilyMember
} from "@/dtos"

import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import { Form as KForm } from "@progress/kendo-vue-form";
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Button as kButton} from '@progress/kendo-vue-buttons'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForms from './EditForms.vue'
import { appUser } from "@/auth"

// const props = 
defineProps<{
    // selectedEmpFamilyMemberId: any,
    // EmpFamilyMemberList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

const memberEditRef = ref<InstanceType<typeof KForm>>()
let kDialogTitle = ref<string>("Add EmpFamilyMember")

let mainData = ref<EmpFamilyMember[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let dataItemInEdit = ref<EmpFamilyMember>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
//   { cell: responsiveCellTemplate, filterable: false, title: 'Banks', hidden: true },
//   { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'memberType', title: 'Type'},
  { field: 'memberNo', title: 'No'},
  { field: 'gender', title: 'Gender'},
  { field: 'fullName', title: 'Full Name'},
  { field: 'nickName', title: 'Nick Name'},
  { field: 'birthDate', title: 'Birth Date', cell: 'datePickerTemplate', format:'DD-MMM-yyyy' , width:130},
  { field: 'phoneNo', title: 'Phone No'},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];


const updateSelectedParentParam = () => {
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
  kDialogTitle.value = "Add Family Member"
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
    const request = new CreateEmpFamilyMember({
      appUserId: appUser.value.id,
      memberType: currData.memberType as FAMILY_MEMBER_TYPE,
      memberNo: currData.memberNo,
      livingStatus: currData.livingStatus as LIVING_STATUS,
      gender: currData.gender as GENDER,
      fullName: currData.fullName,
      nickName: currData.nickName,
      birthDate: currData.birthDate,
      placeOfBirth: currData.placeOfBirth,
      phoneNo: currData.phoneNo,
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
      appUserId: appUser.value.id,
      memberType: currData.memberType as FAMILY_MEMBER_TYPE,
      memberNo: currData.memberNo,
      livingStatus: currData.livingStatus as LIVING_STATUS,
      gender: currData.gender as GENDER,
      fullName: currData.fullName,
      nickName: currData.nickName,
      birthDate: currData.birthDate,
      placeOfBirth: currData.placeOfBirth,
      phoneNo: currData.phoneNo,
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
        <EditForms :data-item="dataItemInEdit"></EditForms>
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