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
            <KComboBox 
                :id="'bankName'" :label="'Bank Name'" :valueField="'bankName'" :textField="'bankName'"
                :dataItems="masterBankStore.masterBankList" v-model="dataItemInEdit.bankName" :value="dataItemInEdit.bankName" :valid="true" :showLabel="true"
                @change="onBankChange" @filterchange="masterBankStore.onFilterMasterBank"
            />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-2">
            <KTextInput id="accountName" v-model="dataItemInEdit.accountName" :validator="nameValidator"
                :showLabel="true" :label="'Account Name'" :type="'string'" />
        </div>
        <div class="col-6 p-2">
            <KTextInput id="accountNo" v-model="dataItemInEdit.accountNo" :validator="bankAccountNoValidator"
                :showLabel="true" :label="'Account No'" :type="'number'" />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-2">
            <KTextInput id="currency" v-model="dataItemInEdit.currency" 
                :showLabel="true" :label="'Currency'" :type="'string'" />
        </div>
        <div class="col-6 p-2">
            <KTextInput id="swiftCode" v-model="dataItemInEdit.swiftCode" 
                :showLabel="true" :label="'Swift Code'" :type="'string'" />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-1">
            <KCheckbox id="isMain" v-model="dataItemInEdit.isMain" 
                :showLabel="true" :label="'Is Active'" />
        </div>
    </div>
    </fieldset>
</form>
</template>

<script setup lang="ts">
import { CBank } from "@/dtos"
import KComboBox from "@/components/kendo/KComboBox.vue"
import KCheckbox from "@/components/kendo/KCheckbox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"

import { nameValidator, bankAccountNoValidator } from "@/stores/validators"

import { useMasterBankStore } from "@/stores/masterbanks"
const masterBankStore = useMasterBankStore()


let dataItemInEdit = ref<CBank>(new CBank())

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
    dataItemInEdit.value = Object.assign({}, props.dataItem as CBank)
}

const onBankChange = async (e: Event) => {
    masterBankStore.onChangeMasterBank(e)
    dataItemInEdit.value.swiftCode = masterBankStore.selectedMasterBank?.swiftCode
}

const onSubmit = async (e: Event) => {
    emit('save', { dataItem : dataItemInEdit.value })
}

defineExpose({
    resetForm
})
</script>