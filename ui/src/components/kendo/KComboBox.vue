<template>
    <div class="k-form-field-wrap">
        <div class="row align-items-center m-1 ">
            <div v-if="label != '' && labelPosition === 'left'" class="col-sm-3">
                <label class="">{{ label }}</label>
            </div>
            <div class="col-sm">
                <KLabel v-if="label != '' && (labelPosition == 'top' || labelPosition == undefined)" :editor-id="id" :editor-valid="valid" class="form-label">
                    {{ label }}
                </KLabel>
                <kComboBox
                    :style="{ width: '100%' }" 
                    :data-items="currentDataItemList"
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
        </div>
        <kError v-if="showValidationMessage">
            {{validationMessage}}
        </kError>
        <kHint v-else-if="showHint">{{ hint }}</kHint>
    </div>
</template>
<script setup lang="ts">
import { ComboBox as kComboBox } from "@progress/kendo-vue-dropdowns";
import { Error as kError, Hint as kHint, Label as KLabel } from "@progress/kendo-vue-labels";
import { process, filterBy, CompositeFilterDescriptor } from '@progress/kendo-data-query'

// let currentDataItemList = ref<any[]>([])

const props = 
defineProps<{
    dataItems: any[],
    id: string,
    valueField?: string | undefined,
    textField?: string | undefined,
    value?: string | number | undefined | null,
    labelPosition?: string | undefined,
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