<template>
    <!-- Main Data Grid -->
    <kGrid ref="grid"
        :data-items="gridData"
        :edit-field="props.editField"
        :sortable="props.sortable"
        :pageable="props.pageable"
        :total="gridDataTotal"
        :filterable="props.filterable"
        :filter="filter"
        @itemchange="onItemChange"
        @datastatechange="onDataStateChange"
        @filterchange="filterChangeHandler"
        @sortchange="sortChangeHandler"
        :columns="gridColumProperties"
        :detail="props.detail"
        :expand-field="props.expandField"
    >
    <kGridToolbar>
        <kButton v-if="props.showAddButton" icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'></kButton>
        <kButton v-if="props.showExportButton" icon="excel" title="Export to Excel" :theme-color="'primary'" 
          @click='onExportToExcel'></kButton>
    </kGridToolbar>
    <template v-slot:expandGridTemplate="{props}">
      <KGridDetailExpanded 
          :data-item="props.dataItem.gridData" 
          :edit-field="editFieldExpanded"
          :sortable="sortableExpanded"
          :pageable="pageableExpanded"
          :total="props.dataItem.total"
          :filterable="filterableExpanded"
          :grid-colum-properties="gridColumPropertiesExpanded"
          :detail="detailExpanded"
          :expand-field="expandFieldExpanded"
          :show-export-button="showExportButtonExpanded"
          @on-item-change-expand="onItemChangeExpand"
          @on-data-state-change-expand="onDataStateChangeExpand"
          @refresh-data-expand="refreshDataExpand"
          @on-expand-change="onExpandChangeExpand"
          @on-save-expand="onSaveExpand"
          @on-edit-expand="onEditExpand"
          @on-insert-expand="onInsertExpand"
          @on-remove-expand="onRemoveExpand"
          @on-cancel-changes-expand="onCancelChangesExpand"
          :grid-export-file-name="props.dataItem.tableName"
      >
      </KGridDetailExpanded>
    </template>
    <template v-slot:actionTemplate="{props}">
        <CommandCell :data-item="props.dataItem" 
                @edit="onEdit"
                @save="onSave" 
                @remove="onRemove" 
                @cancel="onCancelChanges"/>
    </template>
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

    <template v-slot:dddTemplate1="{ props }">
      <td>
        <KGridDropDownDisplay 
          :data-item="props.dataItem"
          :field = "props.field"
          :item-list="lookupList4"
          :value-field="valueField4"
          :text-field="textField4"
          >
        </KGridDropDownDisplay>
      </td>
    </template>
    
    </kGrid>
    <!-- End Main Data Grid -->
</template>
<script setup lang="ts">
import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps, GridDataStateChangeEvent } from '@progress/kendo-vue-grid'
import { saveExcel } from '@progress/kendo-vue-excel-export';
import { SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import CommandCell from '@/layouts/partials/KGridCommandCell.vue'
import KGridDropDownCell from "@/components/grids/KGridDropDownCell.vue"
import KGridDropDownDisplay from "@/components/grids/KGridDropDownDisplay.vue"
import KGridDatePickerCell from "@/components/grids/KGridDatePickerCell.vue"
import KGridTimePickerCell from "@/components/grids/KGridTimePickerCell.vue"
import KGridDetailExpanded from "@/components/grids/KGridDetailExpanded.vue"

const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

interface Props {
  responsiveColumnTitle?: string,
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
    lookupList4?: any[],
    valueField4?: string,
    textField4?: string,
    gridData: any[],
    gridDataTotal: number,
    gridColumProperties: GridColumnProps[],
    gridExportFileName: string,
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
    showAddButton?: boolean,
    editField? : string ,
    expandField?: string,
    detail?: any,
    //expanded data
    gridColumPropertiesExpanded?: GridColumnProps[],
    filterableExpanded?: boolean,
    sortableExpanded?: boolean,
    pageableExpanded?: boolean,
    showExportButtonExpanded?: boolean,
    showAddButtonExpanded?: boolean,
    editFieldExpanded? : string ,
    expandFieldExpanded?: string,
    detailExpanded?: any,
}

const props = withDefaults(defineProps<Props>(), {
    showAddButton: true
})

// const props = defineProps<{
//     responsiveColumnTitle?: string,
//     lookupList?: any[],
//     lookupList1?: any[],
//     valueField1?: string,
//     textField1?: string,
//     lookupList2?: any[],
//     valueField2?: string,
//     textField2?: string,
//     lookupList3?: any[],
//     valueField3?: string,
//     textField3?: string,
//     gridData: any[],
//     gridDataTotal: number,
//     gridColumProperties: GridColumnProps[],
//     gridExportFileName: string,
//     filterable?: boolean,
//     sortable?: boolean,
//     pageable?: boolean,
//     showExportButton?: boolean,
//     showAddButton?: boolean,
//     editField? : string | undefined
// }>()

const emit = defineEmits<{
    (e:'refreshData') : ()=> void
    (e:'onCancelChanges') : ()=> void
    (e:'onInsert') : ()=> void
    (e:'onDataStateChange', event: GridDataStateChangeEvent) : ()=> void
    (e:'onItemChange', dataItem: object) : ()=> void
    (e:'onRemove', dataItem: object): () => void
    (e:'onEdit', dataItem: object): () => void
    (e:'onSave', dataItem: object): () => void
    //expand grid
    (e:'onItemChangeExpand', dataItem: object): () => void
    (e:'onDataStateChangeExpand', event: GridDataStateChangeEvent): () => void
    (e:'onExpandChangeExpand', dataItem: object): () => void
    (e:'refreshDataExpand'): () => void
    (e:'onSaveExpand', dataItem: object): () => void
    (e:'onInsertExpand'): () => void
    (e:'onCancelChangesExpand'): () => void
    (e:'onRemoveExpand', dataItem: object): () => void
    (e:'onEditExpand', dataItem: object): () => void
}>()

function useBreakpoints() {
  
  let windowWidth = ref(window.innerWidth)
  const width = computed(() =>  windowWidth.value)

  const onWidthChange = () => windowWidth.value = window.innerWidth
  
  onMounted(() => {window.addEventListener('resize', onWidthChange);})
  onUnmounted(() => window.removeEventListener('resize', onWidthChange))

  const currWindowType = computed(() => {
    // if(props.responsiveColumnTitle === "") return null 
    if (windowWidth.value < 550) return 'xs'
    if (windowWidth.value >= 550 && windowWidth.value < 1200) return 'md'
    if (windowWidth.value >= 1200) return 'lg'
    return null;
  })
  
  return { width, currWindowType }
}

let lastWindowType = ref<string | null>("lg")

const { width, currWindowType } = useBreakpoints()

const onShowHideColumns = () => {
  if(props.editField != 'inEdit'){
    if(currWindowType.value != lastWindowType.value && width.value > 100) {
      props.gridColumProperties.map( col => {
        if(col.title != "Action") {
          if(col.title == props.responsiveColumnTitle){
            col.hidden = !col.hidden
          } else {
            col.hidden = !col.hidden
          }
        }
      })
      lastWindowType.value = currWindowType.value
      refreshData()
    }
  }
}

onMounted(async () => {
  if(props.responsiveColumnTitle) {
    window.addEventListener('resize', onShowHideColumns)
  }
});

const refreshData = async () => {
    emit('refreshData')
}

const onCancelChanges = () => {
  emit('onCancelChanges')
}

const onItemChange = (e: any) => {
    emit('onItemChange', e)
}
const onDataStateChange = (event: GridDataStateChangeEvent) => {
    emit('onDataStateChange', event)
}
const onInsert = () => {
    emit('onInsert')
}
const onRemove = async(e: any) => {
    emit('onRemove', e.dataItem)
}
const onEdit = (e: any) => {
    emit('onEdit', e)
}

const onSave = async (e: any) => {
    emit('onSave', e.dataItem)
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

const onExportToExcel = () => {
  saveExcel({
      data: props.gridData as any[],
      fileName: props.gridExportFileName,
      columns: props.gridColumProperties
  });
}

const onItemChangeExpand = (e: any) => {
    emit('onItemChangeExpand', e)
}

const onDataStateChangeExpand = (event: GridDataStateChangeEvent) => {
  emit('onDataStateChangeExpand', event)
}

const refreshDataExpand = () => {
  emit('refreshDataExpand')
}

const onExpandChangeExpand = (e:any) => {
  emit('onExpandChangeExpand', e)
}

const onSaveExpand = async (e:any) => {
  emit('onSaveExpand', e.dataItem)
}

const onInsertExpand = () => {
  emit('onInsertExpand')
}

const onRemoveExpand = async (e:any) => {
  emit('onRemoveExpand', e.dataItem)
}

const onCancelChangesExpand = () => {
  emit('onCancelChangesExpand')
}

const onEditExpand = (e:any) => {
  emit('onEditExpand', e)
}
</script>