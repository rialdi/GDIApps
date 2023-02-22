<template>
    <KLabel :editor-id="id"  :optional="optional">
    {{label}}
    </KLabel>
    <div class="k-form-field-wrap">
    <span>
        <KCheckbox 
            :value="modelValue"
            :id="id"
            @change="handleChange"
            @blur="handleBlur"
            @focus="handleFocus" />
        </span>
        <KError v-if="showValidationMessage">
            {{validationMessage}}
        </KError>
        <KHint v-else>{{hint}}</KHint>
    </div>
</template>
<script setup lang="ts">
import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { Checkbox  as KCheckbox } from "@progress/kendo-vue-inputs";

const props = 
defineProps<{
    id: string,
    modelValue?: boolean | string,
    showLabel?: boolean|true,
    label?: string,
    optional?: boolean|true,
    valid?: boolean | true,
    validationMessage?: string,
    touched?: boolean | false,
    hint?: string ,
    disabled?: boolean | false,
    placeholder? : string | undefined,
    required?: boolean | false,
 }>()

 const emit = defineEmits<{
    (e:'update:modelValue', value:any): () => void
    (e:'change', value:any): () => void
    (e:'filterchange', value:any): () => void
    (e:'blur', value:any): () => void
    (e:'focus', value:any): () => void
}>()

const showValidationMessage = computed( () => props.validationMessage )
// const showHint = computed( () => !showValidationMessage && props.hint )

const handleChange = (e : any) =>{
    emit('update:modelValue', e.value)
    emit('change', e);
}
const handleBlur = (e : any) =>{
    emit('blur', e);
}
const handleFocus = (e : any) =>{
    emit('focus', e);
}

</script>