<template>
    <Grid
        :data-items="dataItem"
        :edit-field="editField"
        :sortable="sortable"
        :pageable="pageable"
        :total="dataItemTotal"
        :filterable="filterable"
        :filter="filter"
        @itemchange="onItemChange"
        @datastatechange="onDataStateChange"
        @filterchange="filterChangeHandler"
        @sortchange="sortChangeHandler"
        :columns="gridColumProperties"
        :detail="detail"
        :expand-field="expandField"
        @expandchange="onExpandChange"
    >
    <kGridToolbar>
        <kButton v-if="props.showAddButton" icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'></kButton>
        <kButton v-if="props.showExportButton" icon="excel" title="Export to Excel" :theme-color="'primary'" 
          @click='onExportToExcel'></kButton>
    </kGridToolbar>
    <template v-slot:expandGridTemplate="{props}">
        <span>{{ props }}</span>
    </template>
    <template v-slot:actionTemplate="{props}">
        <CommandCell :data-item="props.dataItem" 
                @edit="onEdit"
                @save="onSave" 
                @remove="onRemove" 
                @cancel="onCancelChanges"/>
    </template>
    <template v-slot:datePickerTemplate="{ props }">
      <td>
        <KGridDatePickerCell
          :field="props.field"
          :data-item="props.dataItem"
          :format="props.format"
        >
        </KGridDatePickerCell> 
      </td>
    </template>
    <template v-slot:timePickerTemplate="{ props }">
      <td>
        <KGridTimePickerCell
          :field="props.field"
          :data-item="props.dataItem"
          :format="props.format"
        >
        </KGridTimePickerCell> 
      </td>
    </template>

    <template v-slot:ddlTemplate1="{ props }">
      <td>
        <KGridDropDownCell 
          :data-item="props.dataItem"
          :field = "props.field"
          :item-list="lookupList1"
          :value-field="valueField1"
          :text-field="textField1"
          >
        </KGridDropDownCell>
      </td>
    </template>

    <template v-slot:ddlTemplate2="{ props }">
      <td>
        <KGridDropDownCell 
          :data-item="props.dataItem"
          :field = "props.field"
          :item-list="lookupList2"
          :value-field="valueField2"
          :text-field="textField2"
          >
        </KGridDropDownCell>
      </td>
    </template>

    <template v-slot:ddlTemplate3="{ props }">
      <td>
        <KGridDropDownCell 
          :data-item="props.dataItem"
          :field = "props.field"
          :item-list="lookupList3"
          :value-field="valueField3"
          :text-field="textField3"
          >
        </KGridDropDownCell>
      </td>
    </template>
    </Grid>
</template>

<script setup lang="ts">
import { Grid, GridToolbar as kGridToolbar, GridColumnProps, GridDataStateChangeEvent  } from '@progress/kendo-vue-grid'
import { SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'
import { saveExcel } from '@progress/kendo-vue-excel-export';

import { Button as kButton} from '@progress/kendo-vue-buttons'
import CommandCell from '@/layouts/partials/KGridCommandCell.vue'
import KGridDropDownCell from "@/components/grids/KGridDropDownCell.vue"
import KGridDatePickerCell from "@/components/grids/KGridDatePickerCell.vue"
import KGridTimePickerCell from "@/components/grids/KGridTimePickerCell.vue"

const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

interface Props {
    dataItem?: any[],
    editField?: string,
    sortable?: boolean,
    pageable?: boolean,
    dataItemTotal?: number,
    filterable?: boolean,
    gridColumProperties?: GridColumnProps[],
    detail?: any,
    expandField?: string,
    lookupList?: any[],
    lookupList1?: any[],
    valueField1?: string,
    textField1?: string,
    lookupList2?: any[],
    valueField2?: string,
    textField2?: string,
    lookupList3?: any[],
    valueField3?: string,
    textField3?: string,
    showExportButton?: boolean,
    showAddButton?: boolean,
    gridExportFileName?: string,
};

const emit = defineEmits<{
    (e:'refreshDataExpand') : ()=> void
    (e:'onCancelChangesExpand') : ()=> void
    (e:'onInsertExpand') : ()=> void
    (e:'onDataStateChangeExpand', event: GridDataStateChangeEvent) : ()=> void
    (e:'onItemChangeExpand', dataItem: object) : ()=> void
    (e:'onRemoveExpand', dataItem: object): () => void
    (e:'onEditExpand', dataItem: object): () => void
    (e:'onSaveExpand', dataItem: object): () => void
    (e:'onExpandChange', event: any): () => void
}>()

const props = withDefaults(defineProps<Props>(), {
    showAddButton: true,
    showExportButton: false
})

const onItemChange = (e: any) => {
    emit('onItemChangeExpand', e)
}
const onDataStateChange = (event: GridDataStateChangeEvent) => {
    emit('onDataStateChangeExpand', event)
}
const filterChangeHandler = (e: any) => {
  filter.value = e.filter
  refreshData()
}
const refreshData = async () => {
    emit('refreshDataExpand')
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
const onInsert = () => {
    emit('onInsertExpand')
}
const onRemove = async(e: any) => {
    emit('onRemoveExpand', e)
}
const onEdit = (e: any) => {
    emit('onEditExpand', e)
}

const onSave = async (e: any) => {
    emit('onSaveExpand', e)
}
const onCancelChanges = () => {
  emit('onCancelChangesExpand')
}
const onExportToExcel = () => {
  saveExcel({
      data: props.dataItem as any[],
      fileName: props.gridExportFileName,
      columns: props.gridColumProperties
  });
}
const onExpandChange = (event:any) =>{
  emit('onExpandChange', event)
}
</script>