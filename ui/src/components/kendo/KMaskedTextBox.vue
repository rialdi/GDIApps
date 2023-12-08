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
                <MaskedTextBox 
                    :id="id"
                    :mask="mask"
                    :value="value"
                    :default-value="modelValue"
                    :disabled="disabled"
                    :required="required"
                    @input="handleChange"
                    @blur="handleBlur"
                    @focus="handleFocus"
                />
            </div>
        </div>
        <KError v-if="showValidationMessage">
            {{validationMessage}}
        </KError>
        <KHint v-else>{{hint}}</KHint>
    </div>
</template>
<script setup lang="ts">
// import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { Error as KError, Hint as KHint } from '@progress/kendo-vue-labels';
import { MaskedTextBox } from '@progress/kendo-vue-inputs';

const props = 
defineProps<{
    id: string,
    mask: string,
    optional?: boolean | true,
    value?: string,
    touched?: boolean | false,
    modelValue?: string,
    labelPosition?: string | undefined,
    label?: string,
    valid?: boolean | true,
    validationMessage?: string,
    hint?: string ,
    disabled?: boolean | false,
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
onMounted(() => {
    
})
// let currValue = computed(() => props.modelValue) 

const showValidationMessage = computed( () => props.validationMessage )
// const showHint = computed( () => !showValidationMessage && props.hint )

const handleChange = (e : any) =>{
    // console.log(e.target.value)
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