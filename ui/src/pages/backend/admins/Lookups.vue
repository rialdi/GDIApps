<script setup lang="ts">
import { ref } from "vue"
// import { formatDate, formatCurrency } from "@/utils"
import { Lookup, QueryLookups, LOOKUPTYPE, CreateLookup, UpdateLookup, DeleteLookup } from "@/dtos"
import { client } from "@/api"
import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps } from '@progress/kendo-vue-grid';
import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
import { Button as kbutton} from '@progress/kendo-vue-buttons'
import { process, State, DataResult, SortDescriptor } from '@progress/kendo-data-query'
import CommandCell from '../../../components/grids/CommandCell.vue';
// import DropDownCell from '../../../components/grids/DropDownCell.vue';
// import { process, DataResult, SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query'


const lookupTypeList = Object.keys(LOOKUPTYPE)
lookupTypeList.push("ALL");
let selectedLookupType = ref<LOOKUPTYPE>();

// Object.assign(lookupTypeList, "ALL");

// console.log(lookupTypeList);

// let editID = ref<number|undefined>()
// let editID = null;
// const defaultItems = { code: null, text: "ALL" };
// let gridData = []
let lookupData = ref<Lookup[]>([]).value;
const pageable = ref(true);
const sortable = ref(true);
const skip = ref<number | undefined>(0);
const take = ref<number | undefined>(10);
let total = ref<number | undefined>(10);

const sort = ref<SortDescriptor[] | undefined>([
  { field: "lookupValue", dir: "asc" }
]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const columns = [
  { field: 'lookupType', title: 'Lookup Type' },
  { field: 'lookupValue', title: 'Lookup Value' },
  { field: 'lookupText', title: 'lookupText' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate' },
  { cell: 'myTemplate', filterable: false, title: 'Action' }
] as GridColumnProps[];

let gridData = ref<DataResult>({ data: [] as any, total: 0 });
  defineExpose({
    gridData
  })

const refreshDatas = async ( ) => {
  const api = await client.api(new QueryLookups({ lookuptype: selectedLookupType.value as LOOKUPTYPE}))
  if (api.succeeded) {
    lookupData = api.response!.results ?? []

    gridData.value.data = process(lookupData, {
      skip: skip.value,
      take: take.value,
      sort: sort.value,
      // filter: filter.
    }).data;

    // console.log(gridData);

    total.value = process( 
      lookupData,{
          // filter: filter.value
      }).total;
  }
}

onMounted(async () => await refreshDatas());

const hasItemsInEdit =  computed(() => 
  gridData.value.data.filter(p => p.inEdit).length > 0
);

const handleDropDownChange = (e: DropDownListChangeEvent) => {
  selectedLookupType.value = (e.value == "ALL" ? null : e.value) ;
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

// const isActiveChange = (dataItem: any) => {
//   console.log('isActiveChange')
//   console.log(dataItem);
// }

// const ddChange = (e: any, dataItem: any) => {
//   const currData = dataItem;
//   currData.isActive = e.target.value;
//   let index = gridData.value.data.findIndex(p => p.id === currData.id);
//   gridData.value.data.splice(index, 1, currData);
// }

// const closeEdit = (e: GridEditEvent) => {
//   // if (e.target === e.currentTarget) {
//   //     // editID = null;
//   // }
// }
const itemChange = (e: any) => {
  if (e.dataItem.id) {
    let index = gridData.value.data.findIndex(p => p.id === e.dataItem.id);
    let updated = Object.assign({},gridData.value.data[index], {[e.field]:e.value});
    gridData.value.data.splice(index, 1, updated);
  } else {
    e.dataItem[e.field] = e.value;
  }
}

const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.value.data.findIndex(p => p.id === e.dataItem.id);
    gridData.value.data.splice(index, 1);
    const request = new DeleteLookup({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
    }
  }
}

const onEdit = (e: any) => {
  let index = gridData.value.data.findIndex(p => p.id === e.dataItem.id);
  let updated = Object.assign({},gridData.value.data[index], {inEdit:true});
  gridData.value.data.splice(index, 1, updated);
}

const onInsert = () => {
  const selectedLookupTypeVal = selectedLookupType.value ?? LOOKUPTYPE.PRIORITY
  const dataItem = { inEdit: true, lookupType: selectedLookupTypeVal, isActive: true };
  gridData.value.data.splice(0, 0, dataItem)
}

const onCancelChanges = () => {
  let editedItems = gridData.value.data.filter(p => p.inEdit === true);
    if(editedItems.length){
      editedItems.forEach(
          item => {
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

</script>

<template>
  <!-- Hero --> 
  <BasePageHeading
    title="Lookups Data"
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
      ></DropDownList>&nbsp; Selected category ID:
      <!-- <strong>{{ dropdownlistLookupType }}</strong> -->
    </p>

    <kGrid ref="grid"
      ::style="{height: '440px'}"
          :data-items="gridData"
          :edit-field="'inEdit'"
          :sortable="sortable"
          :pageable="pageable"
          :take="take"
          :skip="skip"
          :total="total"
          @itemchange="itemChange"
          @datastatechange="dataStateChange"
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
        <td :colspan="1" >
          <input type="checkbox" id="isActive" v-model="props.dataItem.isActive" :disabled="!props.dataItem.inEdit"/>
        </td>
      </template>
    </kGrid>
    </BaseBlock>
    <!-- END Partial Table -->
  </div>
  <!-- END Page Content -->
</template>
