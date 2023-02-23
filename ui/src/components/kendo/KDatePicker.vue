<template>
    <KLabel :editor-id="id" :editor-valid="isValid" :disabled="disabled" :optional="optional" class="form-label">
        {{label}}
    </KLabel>
    <div class="k-form-field-wrap">
        <kDatePicker 
            :id="id"
            :value="modelValue"
            :format="format"
            :disabled="disabled"
            :placeholder="placeholder"
            :required="required"
            :validation-message="valMessage"
            @change="handleChange"
            @blur="handleBlur"
            @focus="handleFocus"
            />
        <KError v-if="!isValid">
            {{ valMessage }}
        </KError>
        <KHint v-else>{{hint}}</KHint>
    </div>
</template>
<script setup lang="ts">
import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { DatePicker as kDatePicker } from '@progress/kendo-vue-dateinputs';

export interface Props {
    id: string,
    optional?: boolean|true,
    value?: Date |undefined,
    modelValue?: Date| undefined,
    format?: string | undefined,
    showLabel?: boolean|true,
    label?: string,
    valid?: boolean | true,
    validationMessage?: string,
    touched?: boolean | false,
    hint?: string,
    disabled?: boolean | false,
    placeholder? : string | undefined,
    required?: boolean | false,
    validator?: Function
}

const props = withDefaults(defineProps<Props>(), {
    format: 'dd-MMM-yyyy'
})

const emit = defineEmits<{
    (e:'update:modelValue', value:any): () => void
    (e:'change', value:any): () => void
    (e:'filterchange', value:any): () => void
    (e:'blur', value:any): () => void
    (e:'focus', value:any): () => void
}>()

let valMessage = ref<string | undefined>()
const isValid = computed( () => getIsValid(props.modelValue))
const getIsValid = (currValue : any) => {
    if (props.validator) {
        valMessage.value = props.validator(currValue) 
        return !valMessage.value
    }
    else if(props.required) {
        valMessage.value = props.validationMessage
        return !(!currValue) 
    }
    else {
        return true
    }
}

const handleChange = (e : any) =>{
    emit('update:modelValue', e.target.value)
    emit('change', e);
}
const handleBlur = (e : any) =>{
    emit('blur', e);
}
const handleFocus = (e : any) =>{
    emit('focus', e);
}

</script>