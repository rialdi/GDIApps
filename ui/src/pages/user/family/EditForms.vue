<script setup lang="ts">
import { 
    EmpFamilyMember, 
    GENDER, FAMILY_MEMBER_TYPE, LIVING_STATUS 
} from "@/dtos"
import { nameValidator, phoneValidatorNotRequired, requiredValidator } from "@/stores/validators"
import { toDate } from "@servicestack/client"

import { Field as KField, FormElement as KFormElement } from "@progress/kendo-vue-form"
import FormInput from "@/components/kform/FormInput.vue"
import FormComboBox from "@/components/kform/FormComboBox.vue"
import FormNumericTextBox from "@/components/kform/FormNumericTextBox.vue"
import FormDatePicker from "@/components/kform/FormDatePicker.vue"

const memberTypeList = Object.values(FAMILY_MEMBER_TYPE)
const livingStatusList = Object.values(LIVING_STATUS)
const genderList = Object.values(GENDER)
let dataItemInEdit = ref<EmpFamilyMember>(new EmpFamilyMember())


let props = 
defineProps<{
    dataItem: any
}>()

// const emit = 
defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    resetForm()
});

const onSave = (e: any) => {
    console.log('save')
}

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as EmpFamilyMember)
    // birthDate.value = toDate(dataItemInEdit.value.birthDate)
    dataItemInEdit.value.profileUrl = "/"
    // endDateInEdit.value = toDate(dataItemInEdit.value.endDate)
}

// const memberEditForm = inject(('kendoForm'))


// const onSubmit = (e: any) => {
//     // console.log('onsubmit')
//     // dataItemInEdit.value.startDate = toLocalISOString(startDateInEdit.value)
//     // dataItemInEdit.value.endDate = toLocalISOString(endDateInEdit.value)
//     emit('save', { dataItem : dataItemInEdit.value })
// }

// const onCancelChanges = () => {
//     dataItemInEdit.value = undefined 
// }

// const onResetForm = () => {
// //   dataItemInEdit.value = undefined 
// }

// const memberEditRef = ref<InstanceType<typeof KForm>>()

// const AllowSubmit = computed(() => {
//     return mainFormRef.value?.form.valid ?? false
// })

// const handleSubmit=() => {

// }

defineExpose({
    resetForm,
    onSave
    // AllowSubmit
})

</script>

<template>
    <KFormElement id="memberForm1">
        <fieldset class="k-form-fieldset">
            <div class="row">
                <KField class="col-md p-1" :name="'memberType'" :label="'Type'" :component="'myTemplate'" :data-items="memberTypeList" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'memberNo'" :label="'No'" :component="'myTemplate'" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormNumericTextBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'gender'" :label="'Type'" :component="'myTemplate'" :data-items="genderList" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'livingStatus'" :label="'Type'" :component="'myTemplate'" :data-items="livingStatusList" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'fullName'" :label="'Full Name'" :component="'myTemplate'" :validator="nameValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"></FormInput>
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'nickName'" :label="'Nick Name'" :component="'myTemplate'" :validator="nameValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"></FormInput>
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'birthDate'" :label="'Birth Date'" :component="'myTemplate'" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormDatePicker v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :value="toDate(props.value)"></FormDatePicker>
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'placeOfBirth'" :label="'Place Of Birth'" :component="'myTemplate'">
                    <template v-slot:myTemplate="{props}">
                        <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"></FormInput>
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'phoneNo'" :label="'Phone No'" :component="'myTemplate'" :validator="phoneValidatorNotRequired">
                    <template v-slot:myTemplate="{props}">
                        <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"></FormInput>
                    </template>
                </KField>
            </div>
            
        </fieldset>
    </KFormElement>
</template>