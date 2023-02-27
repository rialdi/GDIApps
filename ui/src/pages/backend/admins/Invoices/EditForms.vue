<template>
    <form @submit.prevent="onSubmit" id="mainForm" class="k-form">
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
    
<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { client } from "@/api"
import { process } from '@progress/kendo-data-query'

import { Invoice, CContract, QueryCContracts, CBank, QueryCBanks, CAddress, QueryCAddresss } from "@/dtos"

import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"

import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"

import { nameValidator } from "@/stores/validators"
import { GridColumnProps } from '@progress/kendo-vue-grid/dist/npm/interfaces/GridColumnProps'

// import { Input as KInput } from '@progress/kendo-vue-inputs';

// const addressSearchRef = ref<InstanceType<typeof PopupSearchGrid>>()
let dataItemInEdit = ref<Invoice>(new Invoice())

let invoiceDateInEdit = ref<Date | undefined>()
// let endDateInEdit = ref<Date | undefined>()

// const startDate = computed (() => toDate(dataItemInEdit.value.startDate))

let props = defineProps<{
    dataItem: any,
    clientList: any[]
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

const gridColumPropCContract = [
  { field: 'contractNo', title: 'Contract No', width:150 },
  { field: 'description', title: 'description'},
  { field: 'currency', title: 'Currency'},
  { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 }
] as GridColumnProps[];

const gridColumPropCAddress = [
  { field: 'addressName', title: 'AddressName', width:150 },
  { field: 'phoneNo', title: 'Phone No'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 }
] as GridColumnProps[];

const gridColumPropCBank = [
  { field: 'bankName', title: 'Bank Name', width:150 },
  { field: 'accountName', title: 'Account Name'},
  { field: 'accountNo', title: 'Account No'},
  { field: 'currency', title: 'Currency'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 }
] as GridColumnProps[];

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

/* Combobox CContract */
const sourceCContractList = ref<CContract[]>([])
let cContractList = ref<any[]>([])
// let selectedCContractId = ref<number | undefined>()

const getCContractList = async() => {
  const api = await client.api(new QueryCContracts({ clientId: dataItemInEdit.value.clientId }))
  if (api.succeeded) {
    sourceCContractList.value = api.response!.results ?? []
    cContractList.value = process(sourceCContractList.value, {}).data as any[]
  }
}

// const cboCContractOnChange = (e: any) => {
//     // selectedCContractId.value = e.value ? e.value.id : undefined
// }

// const cboCContractOnFilter = (e : any) => {
//   const data = process(sourceCContractList.value, {}).data as any[]
//   cContractList.value = filterBy(data, e.filter)
// }
/* END of Combobox CContract */

/* Combobox CBank */
const sourceCBankList = ref<CBank[]>([])
let cBankList = ref<any[]>([])
// let selectedCBankId = ref<number | undefined>()

const getCBankList = async() => {
  const api = await client.api(new QueryCBanks({ }))
  if (api.succeeded) {
    sourceCBankList.value = api.response!.results ?? []
    cBankList.value = process(sourceCBankList.value, {}).data as any[]
  }
}

// const cboCBankOnChange = (e: any) => {
//     // selectedCBankId.value = e.value ? e.value.id : undefined
// }

// const cboCBankOnFilter = (e : any) => {
//   const data = process(sourceCBankList.value, {}).data as any[]
//   cBankList.value = filterBy(data, e.filter)
// }
/* END of Combobox CContract */

/* Combobox CAddress */
const sourceCAddressList = ref<CAddress[]>([])
let cAddressList = ref<CAddress[]>([])
// let selectedCAddressId = ref<number | undefined>()

const getCAddressList = async() => {
  const api = await client.api(new QueryCAddresss({ clientId: dataItemInEdit.value.clientId }))
  if (api.succeeded) {
    sourceCAddressList.value = api.response!.results ?? []
    cAddressList.value = process(sourceCAddressList.value, {}).data as any[]
  }
}

// const cboCAddressOnChange = (e: any) => {
//     // selectedCAddressId.value = e.value ? e.value.id : undefined
// //   mainGridtRef.value?.updateSelectedClientId(selectedClientId.value)
// }

// const cboCAddressOnFilter = (e : any) => {
//   const data = process(sourceCAddressList.value, {}).data as any[]
//   cAddressList.value = filterBy(data, e.filter)
// }
/* END of Combobox CContract */

defineExpose({
    resetForm
})
</script>