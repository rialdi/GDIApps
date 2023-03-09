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
        @filterchange="filterChangeHandler"
        @sortchange="sortChangeHandler"
        :columns="gridColumProperties"
    >
    <kGridToolbar>
        <kButton icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'></kButton>
        <kButton v-if="props.showExportButton" icon="excel" title="Export to Excel" :theme-color="'primary'" 
          @click='onExportToExcel'></kButton>
    </kGridToolbar>
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
    
    </kGrid>
    <!-- End Main Data Grid -->
</template>
<script setup lang="ts">
import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { saveExcel } from '@progress/kendo-vue-excel-export';
import { SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'

import { Button as kButton} from '@progress/kendo-vue-buttons'
import CommandCell from '@/layouts/partials/KGridCommandCell.vue'

const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const props = defineProps<{
    responsiveColumnTitle?: string,
    gridData: any[],
    gridDataTotal: number,
    gridColumProperties: GridColumnProps[],
    gridExportFileName: string,
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean,
    editField? : string | undefined
}>()

const emit = defineEmits<{
    (e:'refreshData') : ()=> void
    (e:'onCancelChanges') : ()=> void
    (e:'onInsert') : ()=> void
    (e:'onRemove', dataItem: object): () => void
    (e:'onEdit', dataItem: object): () => void
    (e:'onSave', dataItem: object): () => void
}>()

function useBreakpoints() {
  let windowWidth = ref(window.innerWidth)

  const onWidthChange = () => windowWidth.value = window.innerWidth
  
  onMounted(() => window.addEventListener('resize', onWidthChange))
  onUnmounted(() => window.removeEventListener('resize', onWidthChange))

  const currWindowType = computed(() => {
    if (windowWidth.value < 550) return 'xs'
    if (windowWidth.value >= 550 && windowWidth.value < 1200) return 'md'
    if (windowWidth.value >= 1200) return 'lg'
    return null;
  })

  const width = computed(() =>  windowWidth.value)
  return { width, currWindowType }
}

let lastWindowType = ref<string | null>("lg")

const { width, currWindowType } = useBreakpoints()

const onShowHideColumns = () => {
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

</script>