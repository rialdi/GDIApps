<template>
    <KLabel :editor-id="id" :disabled="disabled" :optional="optional" class="form-label">
        {{label}}
    </KLabel>
    <!-- <div class="k-form-field-wrap"> -->
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
        <!-- <KError v-if="showValidationMessage">
            {{validationMessage}}
        </KError>
        <KHint v-else>{{hint}}</KHint> -->
    <!-- </div> -->
</template>
<script setup lang="ts">
// import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';
import { Label as KLabel } from '@progress/kendo-vue-labels';
import { MaskedTextBox } from '@progress/kendo-vue-inputs';

// const props = 
defineProps<{
    id: string,
    mask: string,
    optional?: boolean | true,
    value?: string,
    touched?: boolean | false,
    modelValue?: string,
    showLabel?: boolean | true,
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

// const showValidationMessage = computed( () => props.validationMessage )
// const showHint = computed( () => !showValidationMessage && props.hint )

const handleChange = (e : any) =>{
    console.log(e.target.value)
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