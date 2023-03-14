<template>
    <div>
        <!-- <label v-if="useLabel && showLabel" :for="id" class="form-label">{{ useLabel }}</label> -->
        <kLabel v-if="showLabel" :editor-id="id" :editor-valid="valid" class="form-label">
            {{label}}
        </kLabel>
        <div class="k-form-field-wrap">
            <kComboBox
                :style="{ width: '100%' }" 
                :data-items="currentDataItemList"
                :id="id"
                :name="id"
                :label="(showLabel ? undefined : label)"
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
import { process, filterBy, CompositeFilterDescriptor } from '@progress/kendo-data-query'

// let currentDataItemList = ref<any[]>([])

const props = 
defineProps<{
    dataItems: any[],
    id: string,
    valueField?: string | undefined,
    textField?: string | undefined,
    value?: string | number | undefined | null,
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

onMounted(() => {
    // console.log('On Mounted' + props.id)
    // currentDataItemList.value = props.dataItems
});

let filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});
// let filter = ref<FilterDescriptor>({ operator:"and", field: ""})

const currentDataItemList = computed( () => {
    const data = process(props.dataItems, {}).data as any[]
    return filterBy(data, filter.value)
})

watch(
  () => props.value,
  () => {
    if(props.value === null || props.value === undefined) {
        // console.log(props.value)
        filter.value.filters = [];
    }
  }
);

const showValidationMessage = computed( () => props.touched && props.validationMessage )
const showHint = computed( () => !showValidationMessage && props.hint )

const handleChange = (e : any) =>{
    // console.log('handleChange' + e.target.value)
    emit('update:modelValue', e.target.value)
    emit('change', e);
}
const handleFilterChange = (e : any) =>{
    filter.value = e.filter 
    emit('filterchange', e);
}
const handleBlur = (e : any) =>{
    emit('blur', e);
}
const handleFocus = (e : any) =>{
    emit('focus', e);
}

</script>