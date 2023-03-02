<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="60%" height="80%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ kDialogTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" title="Reset Data"></kButton>
            </div>
        </template>
        <EditForms ref="editFormsRef" :data-item="dataItemInEdit" @save="onSave" :client-list="clientList" />
        <kDialogActionsBar>
        <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
        <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
        <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" title="Save"> Save </kButton>
        </kDialogActionsBar>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
    
    <!-- Main Data Grid -->
    <KStandardGrid 
        :responsive-column-title="'Banks'"
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
    <!-- End Main Data Grid -->
</template>
<script setup lang="ts">
import { formatDate } from "@/utils"
// import { toDateFmt } from '@servicestack/client'
import { client } from "@/api"
import { InvoiceView, QueryInvoices, CreateInvoice, UpdateInvoice, DeleteInvoice } from "@/dtos"
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

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add Client Address")

onMounted(async () => {
  await refreshDatas()
});

let InvoiceData = ref<InvoiceView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>(10)
let dataItemInEdit = ref<InvoiceView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

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

const invoiceDateCellTemplate = (h : any, tdElement : any , props : any, listeners : any ) => {
  return h(tdElement, {}, [ formatDate(props.dataItem.invoiceDate, "DD MMM YYYY") ])
}

let currSelectedClientId = ref<number | undefined>()
const updateSelectedClientId = (val: any) => {
  currSelectedClientId.value = val

  // Show Hide Client Columns
  gridColumProperties.map( col => {
    if(col.title == "Client") {
      col.hidden = !(!currSelectedClientId.value)
    }
  })

  refreshDatas() 
}

let gridColumProperties = [
  { cell: responsiveCellTemplate, filterable: false, title: 'Banks', hidden: true },
  { cell: clientCellTemplate, filterable: false, title: 'Client'},
  { field: 'invoiceNo', title: 'Invoice No', width:150 },
  { cell: invoiceDateCellTemplate, title: 'Invoice Date'},
  { field: 'description', title: 'Description'},
  { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  // { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async () => {
  const currClientId = currSelectedClientId.value
  const queryInvoices = (currClientId) ? 
    new QueryInvoices({ clientId: currClientId }) : 
    new QueryInvoices() 

  const api = await client.api(queryInvoices)
  if (api.succeeded) {
    InvoiceData.value = api.response!.results ?? []
    gridData.data = process(InvoiceData.value, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(InvoiceData.value,{}).total;
    dataItemInEdit.value = undefined
  }
}

const onInsert = () => {
  kDialogTitle.value = "Add Invoice"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteInvoice({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete Invoice', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Invoice"
  dataItemInEdit.value = e.dataItem
  // editFormRef.value?.focus
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}

const onResetForm = () => {
  // editFormRef.value?.resetForm()
  editFormsRef.value?.resetForm()
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
  // console.log("Country : " + currData.country)
  // return
  if( currData.id == null) {
    const request = new CreateInvoice({
      clientId: currData.clientId,
      cContractId : currData.cContractId,
      cBankId : currData.cBankId,
      cAddressId : currData.cAddressId,
      invoiceNo : currData.invoiceNo,
      paymentTermDays : currData.paymentTermDays,
      invoiceDate : currData.invoiceDate,
      description : currData.description,
      poNumber : currData.poNumber,
      vat : currData.vat,
      wht : currData.wht,
      totalAmount : currData.totalAmount,
      vatAmount : currData.vatAmount,
      invoiceStatus : currData.invoiceStatus
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Add Invoice', 'Successfully added new Invoice data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Invoice', 'Failed to add new Invoice data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Invoice', 'Failed to add new Invoice data')
      }
    }
  }
  else{
    const request = new UpdateInvoice({
      id : currData.id,
      clientId: currData.clientId,
      cContractId : currData.cContractId,
      cBankId : currData.cBankId,
      cAddressId : currData.cAddressId,
      invoiceNo : currData.invoiceNo,
      paymentTermDays : currData.paymentTermDays,
      invoiceDate : currData.invoiceDate,
      description : currData.description,
      poNumber : currData.poNumber,
      vat : currData.vat,
      wht : currData.wht,
      totalAmount : currData.totalAmount,
      vatAmount : currData.vatAmount,
      invoiceStatus : currData.invoiceStatus
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas()
        showNotifSuccess('Update Invoice', 'Successfully updated Invoice data 🎉')
    } 
  }
}

defineExpose({
    updateSelectedClientId,
    refreshDatas
})

</script>