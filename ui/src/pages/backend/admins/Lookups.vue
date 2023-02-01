<script setup lang="ts">
import { ref } from "vue"
// import { formatDate, formatCurrency } from "@/utils"
import { Lookup, QueryLookups, LOOKUPTYPE, CreateLookup, UpdateLookup, DeleteLookup } from "@/dtos"
import { client } from "@/api"
import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps } from '@progress/kendo-vue-grid';
import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
import { Button as kbutton} from '@progress/kendo-vue-buttons'
import { process, State, SortDescriptor, DataResult } from '@progress/kendo-data-query'
import CommandCell from '../../../layouts/partials/KGridCommandCell.vue';
import { showNotifError, showNotifSuccess } from '@/stores/commons'

// import DropDownCell from '../../../components/grids/DropDownCell.vue';
// const ddChange = (e: any, dataItem: any) => {
//   const currData = dataItem;
//   currData.isActive = e.target.value;
//   let index = gridData.value.data.findIndex(p => p.id === currData.id);
//   gridData.value.data.splice(index, 1, currData);
// }

// import { process, DataResult, SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'
// import { useNotification } from "@kyvg/vue3-notification";

// const notification = useNotification()
// const root = getCurrentInstance(); 

const lookupTypeList = Object.keys(LOOKUPTYPE)
lookupTypeList.push("ALL");
let selectedLookupType = ref<LOOKUPTYPE>();
let lookupData = ref<Lookup[]>([]);
let total = ref<number | undefined>(10);

const sort = ref<SortDescriptor[] | undefined>([]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const columns = [
  { field: 'lookupType', title: 'Lookup Type' },
  { field: 'lookupValue', title: 'Lookup Value' },
  { field: 'lookupText', title: 'Lookup Text' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'myTemplate', filterable: false, title: 'Action', className:"center" , width:200 }
] as GridColumnProps[];

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;

const refreshDatas = async ( ) => {
  const api = await client.api(new QueryLookups({ lookuptype: selectedLookupType.value as LOOKUPTYPE}))
  if (api.succeeded) {
    lookupData.value = api.response!.results ?? []

    gridData.data = process(lookupData.value, {
      sort: sort.value,
      // filter: filter.
    }).data;

    // console.log(gridData);

    total.value = process( 
      lookupData.value,{
          // filter: filter.value
      }).total;
  }
}

// Helper variables
let tooltipTriggerList = ref<any>([]);
let tooltipList = ref<any>([]);

onMounted(async () => {
  await refreshDatas()
  // Grab all tooltip containers..
  tooltipTriggerList = [].slice.call(
    document.querySelectorAll('[data-bs-toggle="tooltip"]')
  );

  // ..and init them
  tooltipList = tooltipTriggerList.map((tooltipTriggerEl:any) => {
    return new window.bootstrap.Tooltip(tooltipTriggerEl, {
      container: tooltipTriggerEl.dataset.bsContainer || "#page-container",
      animation:
        tooltipTriggerEl.dataset.bsAnimation &&
        tooltipTriggerEl.dataset.bsAnimation.toLowerCase() == "true"
          ? true
          : false,
    });
  });
});

// Dispose tooltips on unMounted
onUnmounted(() => {
  tooltipList.forEach((tooltip:any) => tooltip.dispose());
});

const hasItemsInEdit =  computed(() => 
  gridData.data.filter(p => p.inEdit).length > 0
);

const handleDropDownChange = (e: DropDownListChangeEvent) => {
  selectedLookupType.value = (e.value == "ALL" ? null : e.value) ;

  if(e.value !== "ALL"){
    if(columns[0].field === "lookupType")
      columns.splice(0,1);
  } else {
    if(columns[0].field !== "lookupType")
      columns.splice(0, 0, { field: 'lookupType', title: 'Lookup Type' })
  }

  refreshDatas();
};

const createAppState = (dataState: State) => {
  sort.value = dataState.sort;
  refreshDatas();
};

const dataStateChange = (event: GridDataStateChangeEvent) => {
  createAppState(event.data);
}

const itemChange = (e: any) => {
  if (e.dataItem.id) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    let updated = Object.assign({},gridData.data[index], {[e.field]:e.value});
    gridData.data.splice(index, 1, updated);
  } else {
    e.dataItem[e.field] = e.value;
  }
}

const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteLookup({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      showNotifSuccess('Delete Lookup', 'Successfully deleted data 🎉')
    }
  }
}

const onEdit = (e: any) => {
  let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
  let updated = Object.assign({},gridData.data[index], {inEdit:true});
  gridData.data.splice(index, 1, updated);
}

const onInsert = () => {
  const selectedLookupTypeVal = selectedLookupType.value ?? LOOKUPTYPE.PRIORITY
  const dataItem = { inEdit: true, lookupType: selectedLookupTypeVal, isActive: true };
  gridData.data.splice(0, 0, dataItem)
}

const onCancelChanges = () => {
  let editedItems = gridData.data.filter((p: { inEdit: boolean; }) => p.inEdit === true);
    if(editedItems.length){
      editedItems.forEach(
(          item: { inEdit: undefined; }) => {
              item.inEdit = undefined;
            });
    }
  refreshDatas();
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
  if( currData.id == null) {
    const request = new CreateLookup({
      lookupType : currData.lookupType,
      lookupValue : currData.lookupValue,
      lookupText : currData.lookupText,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Add Lookup', 'Successfully added new Lookup data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Lookup', 'Failed to add new Lookup data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Lookup', 'Failed to add new Lookup data')
      }
    }
  }
  else{
    const request = new UpdateLookup({
      id : currData.id,
      lookupType : currData.lookupType ,
      lookupValue : currData.lookupValue,
      lookupText : currData.lookupText,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Update Lookup', 'Successfully updated Lookup data 🎉')
    } 
  }
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
    refreshDatas()
  }
}

</script>

<template>
  <!-- Hero --> 
  <BasePageHeading
    title="Lookups Datass"
    subtitle="Mobile friendly tables that work across all screen sizes."
  >
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Lookups</li>
        </ol>
      </nav>
    </template>
  </BasePageHeading>
  <!-- END Hero -->

  <!-- Page Content -->
  <div class="content">
    <button
            type="button"
            class="btn btn-primary w-100"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="Top Tooltip"
          >
            Top
          </button>
          <button type="button" class="btn btn-sm btn-alt-danger" data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="Top Tooltip"> 
            <i class="fa fa-fw fa-times"></i>
            </button>
    <!-- Partial Table -->
    <!-- <notifications position="bottom left" classes="alert alert-info d-flex align-items-center" /> -->
    <BaseBlock title="Default Table">
      
      <p>
        <DropDownList
          :data-items="lookupTypeList"
          @change="handleDropDownChange"
          :default-value="'ALL'"
        ></DropDownList>&nbsp; Selected category ID:
        <!-- <strong>{{ dropdownlistLookupType }}</strong> -->
      </p>

      <kGrid ref="grid"
        ::style="{height: '440px'}"
            :data-items="gridData"
            :edit-field="'inEdit'"
            :sortable="true"
            :pageable="true"
            :total="total"
            @itemchange="itemChange"
            @datastatechange="dataStateChange"
            @sortchange="sortChangeHandler"
            :columns="columns"
      >
        <kGridToolbar>
          <kbutton title="Add new" :theme-color="'primary'" @click='onInsert'>
              Add new
          </kbutton>
          <kbutton v-if="hasItemsInEdit"
                  title="Cancel current changes"
                  @click="onCancelChanges">
                  Cancel current changes
          </kbutton>
        </kGridToolbar>
        <template v-slot:myTemplate="{props}">
            <CommandCell :data-item="props.dataItem" 
                    @edit="onEdit"
                    @save="onSave" 
                    @remove="onRemove"
                    @cancel="onCancelChanges"/>
        </template>
        <template v-slot:isActiveTemplate="{ props }">
          <td :colspan="1" style="text-align:center">
            <input type="checkbox" id="isActive" v-model="props.dataItem.isActive" :disabled="!props.dataItem.inEdit"/>
          </td>
        </template>
      </kGrid>
    </BaseBlock>
    <!-- END Partial Table -->
  </div>
  <!-- END Page Content -->
</template>
