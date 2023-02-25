<template>
    <kDialog v-if="showPopup" @close="onCancel" width="60%" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
              {{ popupSearchTitle }} 
              <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetGrid" title="Reset Grid"></kButton>
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
    
</template>

<script setup lang="ts">
import { Grid as kGrid, GridColumnProps } from '@progress/kendo-vue-grid'
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Button as kButton} from '@progress/kendo-vue-buttons'

const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

interface Props {
    popupSearchTitle?: string,
    currValueId: number | null | undefined,
    gridSourceData: any[],
    gridColumProperties: GridColumnProps[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
}

const props = withDefaults(defineProps<Props>(), {
    currValueId: 0,
    popupSearchTitle: 'Search Value',
    filterable: true,
    sortable: true,
    pageable: true
})

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>()

let showPopup = ref<boolean>(false)

onMounted( () => {
    showPopup.value = false
})

const refreshData = () => {
    gridData.data = process(props.gridSourceData, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(props.gridSourceData,{}).total;
}

const showPopupSearchDialog = () => {
    showPopup.value = true
    onResetGrid()
}
const onResetGrid = () => {
    sort.value = undefined,
    filter.value = {logic: "and", filters: []}
    refreshData()
}

const onCancel = () => {
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