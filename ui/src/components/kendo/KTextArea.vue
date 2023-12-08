<template>
    <div class="k-form-field-wrap">
        <div class="row align-items-center m-1 ">
            <div v-if="label != '' && labelPosition === 'left'" class="col-sm-3">
                <label class="">{{ label }}</label>
            </div>
            <div class="col-sm">
                <label v-if="label != '' && (labelPosition == 'top' || labelPosition == undefined)" :editor-id="id" :editor-valid="valid" class="form-label">
                    {{ label }}
                </label>
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
            </div>
        </div>
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
import { Error as KError, Hint as KHint } from '@progress/kendo-vue-labels';
import { TextArea as KTextArea } from '@progress/kendo-vue-inputs';

const props = 
defineProps<{
    id: string,
    optional?: boolean|true,
    modelValue?: string | undefined,
    labelPosition?: string | undefined,
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