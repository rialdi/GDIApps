<template>
<form @submit.prevent="onSubmit" id="mainForm" class="k-form">
    <fieldset>
    <div class="row">
        <div class="col-6 p-1">
            <KComboBox 
                :id="'client'" :label="'Client'" :valueField="'id'" :textField="'name'" :required="true"
                :dataItems="clientList" v-model="dataItemInEdit.clientId" :value="dataItemInEdit.clientId" :valid="true" :showLabel="true"
            />
        </div>
        <div class="col-6 p-1">
            <KTextInput id="contractNo" v-model="dataItemInEdit.contractNo" :validator="nameValidator"
                :showLabel="true" :label="'Contract No'" :type="'string'" />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-2">
            <KDatePicker :id="'startDate'"  v-model="startDateInEdit" />
        </div>
        <div class="col-6 p-2">
            <KDatePicker :id="'startDate'"  v-model="endDateInEdit" />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-2">
            <KTextInput id="currency" v-model="dataItemInEdit.currency" 
                :showLabel="true" :label="'Currency'" :type="'string'" />
        </div>
        <div class="col-6 p-2">
            <KNumericTextBox id="totalAmount" v-model="dataItemInEdit.totalAmount" 
                :showLabel="true" :label="'Total Amount'" />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-1">
            <KCheckbox id="isActive" v-model="dataItemInEdit.isActive" 
                :showLabel="true" :label="'Is Active'" />
        </div>
    </div>
    </fieldset>
</form>
</template>

<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { CContract } from "@/dtos"
import KComboBox from "@/components/kendo/KComboBox.vue"
import KCheckbox from "@/components/kendo/KCheckbox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"

import { nameValidator } from "@/stores/validators"

import { useMasterBankStore } from "@/stores/masterbanks"
const masterBankStore = useMasterBankStore()

let dataItemInEdit = ref<CContract>(new CContract())

let startDateInEdit = ref<Date | undefined>()
let endDateInEdit = ref<Date | undefined>()

// const startDate = computed (() => toDate(dataItemInEdit.value.startDate))

let props = defineProps<{
    dataItem: any,
    clientList: any[]
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    masterBankStore.getMasterBankList()
    resetForm()
});

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as CContract)
    startDateInEdit.value = toDate(dataItemInEdit.value.startDate)
    endDateInEdit.value = toDate(dataItemInEdit.value.endDate)
}

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.startDate = toLocalISOString(startDateInEdit.value)
    dataItemInEdit.value.endDate = toLocalISOString(endDateInEdit.value)
    emit('save', { dataItem : dataItemInEdit.value })
}

defineExpose({
    resetForm
})
</script>