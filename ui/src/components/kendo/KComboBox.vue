<template>
    <div>
        <!-- <label v-if="useLabel && showLabel" :for="id" class="form-label">{{ useLabel }}</label> -->
        <kLabel :editor-id="id" :editor-valid="valid" class="form-label">
            {{label}}
        </kLabel>
        <div class="k-form-field-wrap">
            <kComboBox
                :data-items="dataItems"
                :id="id"
                :name="id"
                :value-field="valueField"
                :text-field="textField"
                :value-primitive="true"
                :value="value"
                :filterable="true"
                :disabled="disabled"
                :required="required"
                @filterchange="handleFilterChange"
                @change="handleChange"
                @blur="handleBlur"
                @focus="handleFocus"
                />
        </div>
        <kError v-if="showValidationMessage">
            {{validationMessage}}
        </kError>
        <kHint v-else-if="showHint">{{ hint }}</kHint>
    </div>
</template>
<script setup lang="ts">
import { ComboBox as kComboBox } from "@progress/kendo-vue-dropdowns";
import { Error as kError, Hint as kHint, Label as kLabel } from "@progress/kendo-vue-labels";

const props = 
defineProps<{
    dataItems: any[],
    id: string,
    valueField: string | undefined,
    textField: string | undefined,
    value?: string | number | undefined,
    showLabel?: boolean|true,
    label?: string,
    valid?: boolean | true,
    validationMessage?: string,
    touched?: boolean | false,
    hint?: string ,
    disabled?: boolean | false,
    required? : boolean | false
    // status?: ResponseStatus|null
 }>()

const emit = defineEmits<{
    (e:'update:modelValue', value:any): () => void
    (e:'change', value:any): () => void
    (e:'filterchange', value:any): () => void
    (e:'blur', value:any): () => void
    (e:'focus', value:any): () => void
}>()

const showValidationMessage = computed( () => props.touched && props.validationMessage )
const showHint = computed( () => !showValidationMessage && props.hint )

const handleChange = (e : any) =>{
    emit('update:modelValue', e.target.value)
    emit('change', e);
}
const handleFilterChange = (e : any) =>{
    emit('filterchange', e);
}
const handleBlur = (e : any) =>{
    emit('blur', e);
}
const handleFocus = (e : any) =>{
    emit('focus', e);
}

</script>