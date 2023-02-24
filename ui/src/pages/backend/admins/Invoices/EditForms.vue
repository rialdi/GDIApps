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
                <KComboBox 
                    :id="'cboCContract'" :showLabel="true" :label="'Contract'" :valueField="'id'" :textField="'contractNo'"
                    :dataItems="cContractList" v-model="dataItemInEdit.cContractId" :value="dataItemInEdit.cContractId" 
                    @change="cboCContractOnChange" @filterchange="cboCContractOnFilter"
                    :valid="true" 
                />
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <KComboBox 
                    :id="'cboCBank'" :showLabel="true" :label="'Bank'" :valueField="'id'" :textField="'bankName'" :required="true"
                    :dataItems="cBankList" v-model="dataItemInEdit.cBankId" :value="dataItemInEdit.cBankId" 
                    @change="cboCBankOnChange" @filterchange="cboCBankOnFilter"
                    :valid="true"
                />
            </div>
            <div class="col-6 p-1">
                <KComboBox 
                    :id="'cboCAddress'" :showLabel="true" :label="'Address'" :valueField="'id'" :textField="'addressName'"
                    :dataItems="cAddressList" v-model="dataItemInEdit.cAddressId" :value="dataItemInEdit.cAddressId" 
                    @change="cboCAddressOnChange" @filterchange="cboCAddressOnFilter"
                    :valid="true" 
                />
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
import { process, filterBy } from '@progress/kendo-data-query'

import { Invoice, CContract, QueryCContracts, CBank, QueryCBanks, CAddress, QueryCAddresss } from "@/dtos"

import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"

import { nameValidator } from "@/stores/validators"

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

onMounted(async () => {
    resetForm()
    getCContractList()
    getCBankList()
    getCAddressList()
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

const cboCContractOnChange = (e: any) => {
    // selectedCContractId.value = e.value ? e.value.id : undefined
}

const cboCContractOnFilter = (e : any) => {
  const data = process(sourceCContractList.value, {}).data as any[]
  cContractList.value = filterBy(data, e.filter)
}
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

const cboCBankOnChange = (e: any) => {
    // selectedCBankId.value = e.value ? e.value.id : undefined
}

const cboCBankOnFilter = (e : any) => {
  const data = process(sourceCBankList.value, {}).data as any[]
  cBankList.value = filterBy(data, e.filter)
}
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

const cboCAddressOnChange = (e: any) => {
    // selectedCAddressId.value = e.value ? e.value.id : undefined
//   mainGridtRef.value?.updateSelectedClientId(selectedClientId.value)
}

const cboCAddressOnFilter = (e : any) => {
  const data = process(sourceCAddressList.value, {}).data as any[]
  cAddressList.value = filterBy(data, e.filter)
}
/* END of Combobox CContract */

defineExpose({
    resetForm
})
</script>