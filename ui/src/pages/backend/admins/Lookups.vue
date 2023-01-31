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
// import DropDownCell from '../../../components/grids/DropDownCell.vue';
// import { process, DataResult, SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'
import Swal from "sweetalert2";

// Set default properties
let toast = Swal.mixin({
  buttonsStyling: false,
  target: "#page-container",
  customClass: {
    confirmButton: "btn btn-success m-1",
    cancelButton: "btn btn-danger m-1",
    input: "form-control",
  },
});

const lookupTypeList = Object.keys(LOOKUPTYPE)
lookupTypeList.push("ALL");
let selectedLookupType = ref<LOOKUPTYPE>();

// Object.assign(lookupTypeList, "ALL");

// console.log(lookupTypeList);

// let editID = ref<number|undefined>()
// let editID = null;
// const defaultItems = { code: null, text: "ALL" };
// let gridData = []
let lookupData = ref<Lookup[]>([]);
const pageable = ref(true);
// const sortable = ref(true);
const skip = ref<number | undefined>(0);
const take = ref<number | undefined>(10);
let total = ref<number | undefined>(10);

// let hideLookupType = ref(true);

const sort = ref<SortDescriptor[] | undefined>([]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const columns = [
  { field: 'lookupType', title: 'Lookup Type' },
  { field: 'lookupValue', title: 'Lookup Value' },
  { field: 'lookupText', title: 'lookupText' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'myTemplate', filterable: false, title: 'Action', className:"center" }
] as GridColumnProps[];

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;

const refreshDatas = async ( ) => {
  const api = await client.api(new QueryLookups({ lookuptype: selectedLookupType.value as LOOKUPTYPE}))
  if (api.succeeded) {
    lookupData.value = api.response!.results ?? []

    gridData.data = process(lookupData.value, {
      skip: skip.value,
      take: take.value,
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

onMounted(async () => await refreshDatas());

const hasItemsInEdit =  computed(() => 
  gridData.data.filter(p => p.inEdit).length > 0
);

const handleDropDownChange = (e: DropDownListChangeEvent) => {
  selectedLookupType.value = (e.value == "ALL" ? null : e.value) ;

  if(e.value !== "ALL")
  {
    if(columns[0].field === "lookupType")
      columns.splice(0,1);
  }
  else
  {
    if(columns[0].field !== "lookupType")
      columns.splice(0, 0, { field: 'lookupType', title: 'Lookup Type' })
  }
  
  refreshDatas();
};

const createAppState = (dataState: State) => {
  take.value = dataState.take;
  skip.value = dataState.skip;
  sort.value = dataState.sort;
  refreshDatas();
};

const dataStateChange = (event: GridDataStateChangeEvent) => {
  createAppState(event.data);
}


// const ddChange = (e: any, dataItem: any) => {
//   const currData = dataItem;
//   currData.isActive = e.target.value;
//   let index = gridData.value.data.findIndex(p => p.id === currData.id);
//   gridData.value.data.splice(index, 1, currData);
// }

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
        toast.fire(
              "Deleted!",
              "Lookup data has been deleted.",
              "success"
            )
      
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
  // console.log('OnSave')
  // console.log(currData)
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
    <!-- Partial Table -->
    <BaseBlock>
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
          :pageable="pageable"
          :take="take"
          :skip="skip"
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
