<template>
    <KLabel :editor-id="id" :disabled="disabled" :optional="optional" class="form-label">
        {{label}}
    </KLabel>
    <div class="k-form-field-wrap">
        <KTextArea 
            :id="id"
            :value="modelValue"
            :disabled="disabled"
            :placeholder="placeholder"
            :required="required"
            :validation-message="validationMessage"
            :maxlength="max"
            :icon-name="iconName"
            :rows="rows"
            @input="handleChange"
            @blur="handleBlur"
            @focus="handleFocus"
            />
        <KError v-if="showValidationMessage">
          {{ validationMessage }}
          <KHint :direction="'end'"> {{ modelValue?.length }} / {{ max }} </KHint>
        </KError>
        <div v-else style="display: flex; justify-content: space-between">
          <KHint>{{ hint }}</KHint>
          <KHint :direction="'end'"> {{ modelValue?.length }} / {{ max }} </KHint>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { TextArea as KTextArea } from '@progress/kendo-vue-inputs';

const props = 
defineProps<{
    id: string,
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
    iconName?: string | undefined,
    max?: string,
    rows? : number | 3 
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