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
                <kDropDownList
                    :class="'mb-0 block w-full sm:text-sm rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 border-gray-300'"
                    :data-items="dataItems"
                    :id="id"
                    :name="id"
                    :value-field="valueField"
                    :text-field="textField"
                    :value-primitive="true"
                    :value="value"
                    :valid="valid"
                    :filterable="filterable"
                    :disabled="disable"
                    @filterchange="handleFilterChange"
                    @change="handleChange"
                    @blur="handleBlur"
                    @focus="handleFocus"
                />
            </div>
        </div>         
        <!-- </div> -->
        <kError v-if="showValidationMessage">
            {{validationMessage}}
        </kError>
        <kHint v-else-if="showHint">{{ hint }}</kHint>
    </div>
</template>
<script setup lang="ts">
// import { humanize, ResponseStatus, toPascalCase } from "@servicestack/client"
// import { computed } from "vue"
// import { ApiState } from "@/api"
import { DropDownList as kDropDownList } from "@progress/kendo-vue-dropdowns";
import { Error as kError, Hint as kHint, Label as KLabel} from "@progress/kendo-vue-labels";
// import { process, filterBy } from '@progress/kendo-data-query'

const props = 
defineProps<{
    dataItems: any[],
    id: string,
    valueField?: string | undefined,
    textField?: string | undefined,
    value?: string | number | undefined,
    labelPosition?: string | undefined,
    label?: string,
    valid?: boolean | true,
    validationMessage?: string,
    touched?: boolean | false,
    hint?: string ,
    disable?: boolean | false,
    filterable?: boolean | false
    // status?: ResponseStatus|null
 }>()

const emit = defineEmits<{
    (e:'update:modelValue', value:any): () => void
    (e:'change', value:any): () => void
    (e:'filterchange', value:any): () => void
    (e:'blur', value:any): () => void
    (e:'focus', value:any): () => void
}>()
// onCreated( () => {
//     currValue.value = props.value
// });

// const useLabel = computed(() => props.label ?? humanize(toPascalCase(props.id)))
const showValidationMessage = computed( () => props.touched && props.validationMessage )
const showHint = computed( () => !showValidationMessage && props.hint )
// const hintId = computed( () => showHint ? `${props.id}_hint` : "" )
// const errorId = computed( () => showValidationMessage ? `${props.id}_error` : "" )

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