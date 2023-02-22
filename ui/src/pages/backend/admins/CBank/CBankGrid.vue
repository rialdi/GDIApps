<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="60%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ kDialogTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" title="Reset Data"></kButton>
            </div>
        </template>
        <EditForms ref="editFormsRef" :data-item="dataItemInEdit" @save="onSave" :client-list="clientList" />
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
        :grid-data="gridData.data" 
        :grid-colum-properties="gridColumProperties" 
        :grid-data-total="gridData.total" 
        :grid-export-file-name="'ClientBank'" 
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
<script setup lang="ts">
import { client } from "@/api"
import { CBankView, QueryCBanks, CreateCBank, UpdateCBank, DeleteCBank } from "@/dtos"
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import KStandardGrid from "@/components/grids/KStandardGrid.vue"
import EditForms from './EditForms.vue'

const editFormsRef = ref<InstanceType<typeof EditForms>>()

// const props = 
defineProps<{
    selectedClientId: any,
    clientList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
}>()

let kDialogTitle = ref<string>("Add Client Address")

onMounted(async () => {
  await refreshDatas()
});

let CBankData = ref<CBankView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>(10)
let dataItemInEdit = ref<CBankView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let currSelectedClientId = ref<number | undefined>()
const updateSelectedClientId = (val: any) => {
  currSelectedClientId.value = val
}

const responsiveCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  const elParentInfoTitle = h('strong',{}, ['Client'])
  const elParentInfo = h('p',{},[props.dataItem.clientCode + ' - ' + props.dataItem.clientName])
  
  const elDataInfoTitle = h('strong',{}, ['Bank'])
  const elDataInfo = h('p',{},[props.dataItem.bankName + ' - ' + props.dataItem.accountNo])

  return h(tdElement, {}, elParentInfoTitle, elParentInfo, elDataInfoTitle, elDataInfo);
}

const clientCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  return h(tdElement, {
                // on: {
                //     click: function(e : any){
                //         listeners.custom(e);
                //     }
                // }
            }, [props.dataItem.clientCode + ' - ' + props.dataItem.clientName])
}

let gridColumProperties = [
  // { cell: 'responsiveTemplate', filterable: false, title: 'CBanks', hidden: true },
  // { cell: 'clientTemplate', filterable: false, title: 'Client'},
  { cell: responsiveCellTemplate, filterable: false, title: 'CBanks', hidden: true },
  { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'bankName', title: 'Bank Name', width:150 },
  { field: 'accountName', title: 'Account Name'},
  { field: 'accountNo', title: 'Account No'},
  { field: 'currency', title: 'Currency'},
  // { field: 'description', title: 'Description'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async () => {
  const currClientId = currSelectedClientId.value
  console.log("currClientId : " + currClientId)
  const queryCBanks = (currClientId) ? 
    new QueryCBanks({ clientId: currClientId }) : 
    new QueryCBanks() 

  const api = await client.api(queryCBanks)
  if (api.succeeded) {
    // console.log('refreshData Parent')
    CBankData.value = api.response!.results ?? []
    gridData.data = process(CBankData.value, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(CBankData.value,{}).total;
    dataItemInEdit.value = undefined
  }
}
const onInsert = () => {
  kDialogTitle.value = "Add Client Address"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteCBank({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete CBank', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Client Address"
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
  if( currData.id == null) {
    const request = new CreateCBank({
      clientId: currData.clientId,
      bankName : currData.bankName,
      accountName : currData.accountName,
      accountNo : currData.accountNo,
      currency : currData.currency,
      swiftCode : currData.swiftCode,
      isMain : currData.isMain
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Add CBank', 'Successfully added new CBank data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add CBank', 'Failed to add new CBank data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add CBank', 'Failed to add new CBank data')
      }
    }
  }
  else{
    const request = new UpdateCBank({
      id : currData.id,
      clientId: currData.clientId,
      bankName : currData.bankName,
      accountName : currData.accountName,
      accountNo : currData.accountNo,
      currency : currData.currency,
      swiftCode : currData.swiftCode,
      isMain : currData.isMain
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update CBank', 'Successfully updated CBank data 🎉')
    } 
  }
}

defineExpose({
    updateSelectedClientId,
    refreshDatas
})
</script>