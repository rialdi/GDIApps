<template>
    <KLabel :editor-id="id" :disabled="disabled" :optional="optional" class="form-label">
        {{label}}
    </KLabel>
    <div class="k-form-field-wrap">
        <KInput 
            :id="id"
            :type="type"
            :value="modelValue"
            :disabled="disabled"
            :placeholder="placeholder"
            :required="required"
            :validation-message="validationMessage"
            @input="handleChange"
            @blur="handleBlur"
            @focus="handleFocus"
            />
        <KError v-if="showValidationMessage">
            {{validationMessage}}
        </KError>
        <KHint v-else>{{hint}}</KHint>
    </div>
</template>
<script setup lang="ts">
import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { Input as KInput } from '@progress/kendo-vue-inputs';

const props = 
defineProps<{
    id: string,
    type: string | undefined,
    optional?: boolean|true,
    modelValue?: string | undefined,
    showLabel?: boolean|true,
    label?: string,
    valid?: boolean | true,
    validationMessage?: string,
    touched?: boolean | false,
    hint?: string ,
    disabled?: boolean | false,
    placeholder? : string | undefined,
    required?: boolean | false
    // status?: ResponseStatus|null
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