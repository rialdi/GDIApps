<template>
   
    <kTabStrip :tabs="tabList" :selected="selectedtab" @select="onSelectTab" class="w-100">
        <template v-slot:tabInvoice class="w-100">
            <form @submit.prevent="onSubmit" id="mainForm" class="k-form w-100">
                <fieldset>
                <div class="row">
                    <div class="col-6 p-1">
                        <KComboBox 
                            :id="'cboClient'" :showLabel="true" :label="'Client'" :valueField="'id'" :textField="'name'" :required="true"
                            :dataItems="clientList" v-model="dataItemInEdit.clientId" :value="dataItemInEdit.clientId" 
                            @change="cboClientOnChange"
                            :valid="true"
                        />
                    </div>
                    <div class="col-6 p-1">
                        <PopupSearchGrid :id="'cContractId'" v-model="dataItemInEdit.cContractId"
                            :grid-source-data="cContractList" 
                            :grid-colum-properties="gridColumPropCContract" 
                            :model-value-text-field="'contractNo'"
                            :show-label="true" :label="'Contract No'"
                            :filterable="true" :pageable="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <PopupSearchGrid :id="'cBankId'" v-model="dataItemInEdit.cBankId"
                            :grid-source-data="cBankList" 
                            :grid-colum-properties="gridColumPropCBank" 
                            :model-value-text-field="'bankDisplay'"
                            :show-label="true" :label="'Bank'"
                            :filterable="false" :pageable="false" />
                    </div>
                    <div class="col-6 p-1">
                        <PopupSearchGrid :id="'cAddressId'" v-model="dataItemInEdit.cAddressId"
                            :grid-source-data="cAddressList" 
                            :grid-colum-properties="gridColumPropCAddress" 
                            :model-value-text-field="'addressName'"
                            :show-label="true" :label="'Contract Address'"
                            :filterable="false" :pageable="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KTextInput id="invoiceNo" v-model="dataItemInEdit.invoiceNo" :validator="nameValidator"
                            :showLabel="true" :label="'Invoice No'" :type="'string'" />
                    </div>
                    <div class="col-6 p-1">
                        <KDatePicker :id="'invoiceDate'"  v-model="invoiceDateInEdit" 
                            :showLabel="true" :label="'Invoice Date'"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-2">
                        <KNumericTextBox id="paymentTermDays" v-model="dataItemInEdit.paymentTermDays" 
                            :showLabel="true" :label="'Payment Term Days'" :format="'n0'"/>
                    </div>
                    <div class="col-6 p-2">
                        <KTextInput id="invoiceStatus" v-model="dataItemInEdit.invoiceStatus" 
                            :showLabel="true" :label="'Invoice Status '" :type="'string'" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-2">
                        <KTextInput id="description" v-model="dataItemInEdit.description" 
                            :showLabel="true" :label="'Description'" :type="'string'" />
                    </div>
                    <div class="col-6 p-2">
                        <KTextInput id="poNumber" v-model="dataItemInEdit.poNumber" 
                            :showLabel="true" :label="'PO Number'" :type="'string'" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-2">
                        <KTextInput id="vat" v-model="dataItemInEdit.vat" 
                            :showLabel="true" :label="'VAT'" :type="'string'" />
                    </div>
                    <div class="col-6 p-2">
                        <KTextInput id="wht" v-model="dataItemInEdit.wht" 
                            :showLabel="true" :label="'WHT '" :type="'string'" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-2">
                        <KNumericTextBox id="totalAmount" v-model="dataItemInEdit.totalAmount" 
                            :showLabel="true" :label="'Total Amount'" />
                    </div>
                    <div class="col-6 p-2">
                        <KNumericTextBox id="vatAmount" v-model="dataItemInEdit.vatAmount" 
                            :showLabel="true" :label="'VAT Amount'" />
                    </div>
                </div>
                </fieldset>
            </form>
        </template>
        <template v-slot:tabAttachment>
            <kGrid
            :edit-field="'inEdit'"
                :data-items="invAttList"
                :columns="gridColInvAtt"
                @itemchange="itemChangeInvAtt"
            >
            <kGridToolbar class="k-form w-100">
                <kButton icon="add" title="Add New" :theme-color="'primary'" @click='addInvAtt'>Add New</kButton>
            </kGridToolbar>
            <template v-slot:attFileTemplate="{ props }">
                <td :colspan="1" style="text-align:center">
                    <a :href="props.dataItem.attachmentUrl">
                        <span class="k-icon k-i-attachment-45"/>
                    </a>
                </td>
            </template>
            <template v-slot:actionTemplate="{props}">
                <CommandCell :data-item="props.dataItem" 
                        @edit="onEditInvAtt"
                        @save="onSaveInvAtt" 
                        @remove="onRemoveInvAtt"
                        @cancel="onCancelChangesInvAtt"/>
            </template>
            </kGrid>
        </template>
    </kTabStrip>

     <!-- Kendo Dialog for Editing Data -->
     <kDialog v-if="showUploadNewInvAtt" width="60%" :title="'Add New Invoice Attachment'" @close="closeUploadNewInvAtt">
        <div class="w-100 mb-2">
            <kInput :style="{ width: '100%' }"  v-model="newInvAttFileName" :label="'File Name'"></kInput>
        </div>
<!-- :restrictions="{ allowedExtensions: [ '.xlsx', '.xls' ] }" -->
        <kUpload class="w-100 k-form"
            :auto-upload="false"
            :default-files="[]"
            :actions-layout="'end'"
            :batch="false"
            :multiple="false"
            :with-credentials="false"
            @add="onAddInvoiceAttachment"
            @beforeupload="onBeforeUploadInvAtt"
            @statuschange="onStatusChangeInvAtt" 
            :save-url="'/api/CreateInvoiceAttachment'"
        />
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
</template>
    
<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { client } from "@/api"

import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"
import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"

// import UploadFile from '@/components/form/UploadFile.vue'

import { Invoice, 
    InvoiceAttachment, QueryInvoiceAttachments, UpdateInvoiceAttachment, DeleteInvoiceAttachment,
    CContract, QueryCContracts, 
    CBank, QueryCBanks, 
    CAddress, QueryCAddresss 
} from "@/dtos"
import { nameValidator } from "@/stores/validators"

import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { TabStrip as kTabStrip} from '@progress/kendo-vue-layout'
import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Upload as kUpload } from '@progress/kendo-vue-upload'
import { Dialog as kDialog} from '@progress/kendo-vue-dialogs'
import { Input as kInput } from '@progress/kendo-vue-inputs'
import { process } from '@progress/kendo-data-query'

import CommandCell from '@/layouts/partials/KGridCommandCell.vue'

let dataItemInEdit = ref<Invoice>(new Invoice())
let invoiceDateInEdit = ref<Date | undefined>()

let props = defineProps<{
    dataItem: any,
    clientList: any[]
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    resetForm()
    getCContractList()
    getCBankList()
    getCAddressList()
    // addressSearchRef.value?.onCancel
});

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as Invoice)
    invoiceDateInEdit.value = toDate(dataItemInEdit.value.invoiceDate)
    refreshInvAtt()
}   

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.invoiceDate = toLocalISOString(invoiceDateInEdit.value)
    emit('save', { dataItem : dataItemInEdit.value })
}

/* Combobox Client */
const cboClientOnChange = (e: any) => {
    getCContractList()
    getCAddressList()
}
/* End Comboboc Client */

/* Popup Search */
/* CContract */
const gridColumPropCContract = [
  { field: 'contractNo', title: 'Contract No', width:150 },
  { field: 'description', title: 'description'},
  { field: 'currency', title: 'Currency'},
  { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 }
] as GridColumnProps[];

const sourceCContractList = ref<CContract[]>([])
let cContractList = ref<any[]>([])
const getCContractList = async() => {
  const api = await client.api(new QueryCContracts({ clientId: dataItemInEdit.value.clientId }))
  if (api.succeeded) {
    sourceCContractList.value = api.response!.results ?? []
    cContractList.value = process(sourceCContractList.value, {}).data as any[]
  }
}
/* END CContract */

/* CBank */
const gridColumPropCBank = [
  { field: 'bankName', title: 'Bank Name', width:150 },
  { field: 'accountName', title: 'Account Name'},
  { field: 'accountNo', title: 'Account No'},
  { field: 'currency', title: 'Currency'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 }
] as GridColumnProps[];

const sourceCBankList = ref<CBank[]>([])
let cBankList = ref<any[]>([])
const getCBankList = async() => {
  const api = await client.api(new QueryCBanks({ }))
  if (api.succeeded) {
    sourceCBankList.value = api.response!.results ?? []
    cBankList.value = process(sourceCBankList.value, {}).data as any[]
  }
}
/* END CBank */

/* CAddress */
const gridColumPropCAddress = [
  { field: 'addressName', title: 'AddressName', width:150 },
  { field: 'phoneNo', title: 'Phone No'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 }
] as GridColumnProps[];

const sourceCAddressList = ref<CAddress[]>([])
let cAddressList = ref<CAddress[]>([])
const getCAddressList = async() => {
  const api = await client.api(new QueryCAddresss({ clientId: dataItemInEdit.value.clientId }))
  if (api.succeeded) {
    sourceCAddressList.value = api.response!.results ?? []
    cAddressList.value = process(sourceCAddressList.value, {}).data as any[]
  }
}
/* END CAddress */
/* END Popup Search */

const selectedtab = ref<number>(0)
const tabList = [
    { title: "Invoice", content: "tabInvoice"},
    { title: "Attachment", content: "tabAttachment" }
]

const onSelectTab = (e: any) => {
    selectedtab.value = e.selected
}

const closeUploadNewInvAtt = () => {
    showUploadNewInvAtt.value = false
}
// const sourceCAddressList = ref<CAddress[]>([])
let invAttList = ref<InvoiceAttachment[]>([])

const refreshInvAtt = async() => {
    const api = await client.api(new QueryInvoiceAttachments({ invoiceId: dataItemInEdit.value.id }))
    if (api.succeeded) {
        const data  = api.response!.results ?? []
        invAttList.value = process(data, {}).data as InvoiceAttachment[]
    }
    showUploadNewInvAtt.value = false
}

const gridColInvAtt = [
  { field: 'fileName', title: 'File Name'},
  { cell: 'attFileTemplate', title: 'File', width:50},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

let showUploadNewInvAtt = ref<boolean>()
let newInvAttFileName = ref<string>()


const addInvAtt = (e : any) => {
    showUploadNewInvAtt.value = true
}

const onBeforeUploadInvAtt = (event: any) => {
  const attachmentUrl = "/uploads/invoiceAttachments/[" + dataItemInEdit.value.id + "]-" + event.files[0].name
  event.additionalData.invoiceId = dataItemInEdit.value.id 
  event.additionalData.fileName = newInvAttFileName.value
  event.additionalData.attachmentUrl = attachmentUrl
}

const onAddInvoiceAttachment = (event: any) => {
    newInvAttFileName.value = event.affectedFiles[0].name
}

const onStatusChangeInvAtt = (event: any) => {
  if (event.response) {
    refreshInvAtt()
  }
}

const onRemoveInvAtt = async(e: any) => {
    const request = new DeleteInvoiceAttachment({
        id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
        refreshInvAtt()
    }
}

const itemChangeInvAtt = (e : any) => {
    if (e.dataItem.id) {
        let index = invAttList.value.findIndex(p => p.id === e.dataItem.id);
        let updated = Object.assign({},invAttList.value[index], {[e.field]:e.value});
        invAttList.value.splice(index, 1, updated);
    } else {
        e.dataItem[e.field] = e.value;
    }
}

const onEditInvAtt = (e: any) => {
    let index = invAttList.value.findIndex(p => p.id === e.dataItem.id);
    let updated = Object.assign({},invAttList.value[index], {inEdit:true});
    invAttList.value.splice(index, 1, updated);
}

const onCancelChangesInvAtt = () => {
    refreshInvAtt()
}

const onSaveInvAtt = async (e: any) => {
    const currData = e.dataItem;
    const request = new UpdateInvoiceAttachment({
        id : currData.id,
        fileName: currData.fileName
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshInvAtt()
    } 
}

defineExpose({
    resetForm
})
</script>

<style>
    .swal2-container {
        z-index: 10005;
    }
    .k-animation-container {
        width: 100%;
    }
</style>