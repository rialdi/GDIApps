<template>
    <KLabel :editor-id="id" :editor-valid="isValid" :disabled="disabled" :optional="optional" class="form-label">
          {{label}}
    </KLabel>
    <kDialog v-if="showPopup" @close="onCancel" width="60%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ popupSearchTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetGrids" title="Reset Grid"></kButton>
            </div>
        </template>
        <kGrid ref="grid"
                :data-items="gridData"
                :sortable="props.sortable"
                :pageable="props.pageable"
                :total="total"
                :filterable="props.filterable"
                :filter="filter"
                @filterchange="filterChangeHandler"
                @sortchange="sortChangeHandler"
                @rowdblclick="onRowDblClick"
                :columns="gridColumProperties"
            >
          <template v-slot:isMainTemplate="{ props }">
              <td :colspan="1" style="text-align:center">
              <input type="checkbox" id="isMain" v-model="props.dataItem.isMain" :disabled="!props.dataItem.inEdit"/>
              </td>
          </template>
          <template v-slot:isActiveTemplate="{ props }">
              <td :colspan="1" style="text-align:center">
              <input type="checkbox" id="isActive" v-model="props.dataItem.isActive" :disabled="!props.dataItem.inEdit"/>
              </td>
          </template>
        </kGrid>
        <kDialogActionsBar>
        <!-- <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton> -->
        <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" title="Save"> Save </kButton> -->
        </kDialogActionsBar>
    </kDialog>
    <div class="k-form-field-wrap">
        <KInput :placeholder="'Search'" :icon-name="'search'" @focus="onSearch" :value="selectedItemText"/>
        <KError v-if="!isValid">
            {{ valMessage }}
        </KError>
        <KHint v-else>{{hint}}</KHint>
    </div>
</template>

<script setup lang="ts">
import { Grid as kGrid, GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Button as kButton} from '@progress/kendo-vue-buttons'
import { Input as KInput } from '@progress/kendo-vue-inputs';
import { Error as KError, Hint as KHint, Label as KLabel } from '@progress/kendo-vue-labels';

const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

interface Props {
    id: string,
    disabled?: boolean | false,
    popupSearchTitle?: string,
    optional?: boolean|true,
    showLabel?: boolean|true,
    label?: string,
    hint?: string ,
    required?: boolean | false,
    validator?: Function,
    validationMessage?: string,
    modelValueTextField : string,
    modelValue?: string | number|  string[] | undefined,
    gridSourceData: any[],
    gridColumProperties: GridColumnProps[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
}

const props = withDefaults(defineProps<Props>(), {
    currValueId: 0,
    currValueText: "Empty",
    popupSearchTitle: 'Search Value',
    filterable: false,
    sortable: true,
    pageable: false
})

const emit = defineEmits<{
    (e:'update:modelValue', value:any): () => void
    (e:'change', value:any): () => void
    (e:'filterchange', value:any): () => void
    (e:'blur', value:any): () => void
    (e:'focus', value:any): () => void
}>()

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>()
let showPopup = ref<boolean>(false)

let valMessage = ref<string | undefined>()
const isValid = computed( () => getIsValid(props.modelValue))
const getIsValid = (currValue : any) => {
    if (props.validator) {
        valMessage.value = props.validator(currValue) 
        return !valMessage.value
    }
    else if(props.required) {
        valMessage.value = props.validationMessage
        return !(!currValue) 
    }
    else {
        return true
    }
}

onMounted( () => {
    showPopup.value = false
})

const selectedItemText = computed (() => {
  const selectedItem = props.gridSourceData.find((x) => x.id === props.modelValue)
  // console.log(selectedItem)
  return selectedItem === undefined ? "" : selectedItem[props.modelValueTextField]
})

const onSearch = (e : Event) => {
  // console.log('onInput')
  refreshData()
  showPopup.value = true
  
}
const refreshData = () => {
    gridData.data = process(props.gridSourceData, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(props.gridSourceData,{}).total;
}

const showPopupSearchDialog = () => {
    showPopup.value = true
    // onResetGrids()
}
const onResetGrids = (e? : any) => {
  // console.log('onResetGrids')
  e.preventDefault()
    filter.value = {logic: "and", filters: []}
    // showPopup.value = true
    refreshData()
}

const onCancel = () => {
    showPopup.value = false
}

// const onRowClick = (e: any) => {
//   emit('update:modelValue', e.dataItem.id)
// }

const onRowDblClick = (e: any) => {
  emit('update:modelValue', e.dataItem.id)
  showPopup.value = false
}

const sortChangeHandler = (e: any) => {
  if(e.sort.length > 0)
  {
    let existingSortItem = sort.value
    if(existingSortItem != undefined && existingSortItem?.length > 0)
    {
      existingSortItem[0].dir = (existingSortItem[0].dir === "asc") ? "desc" : "asc" 
      sort.value = existingSortItem
    }
    else{
      e.sort[0].dir = (e.sort[0].dir === "asc") ? "desc" : "asc" 
      sort.value = e.sort;
    }
    refreshData()
  }
}

const filterChangeHandler = (e: any) => {
  filter.value = e.filter
  refreshData()
}

defineExpose({
    showPopupSearchDialog,
    refreshData
})
</script>