<script setup lang="ts">
import { ref } from "vue"
// import { formatDate, formatCurrency } from "@/utils"
import { Client, QueryClients, CreateClient, UpdateClient, DeleteClient } from "@/dtos"
import { client } from "@/api"
import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps } from '@progress/kendo-vue-grid';
import { Button as kbutton} from '@progress/kendo-vue-buttons'
import { process, State, SortDescriptor, DataResult } from '@progress/kendo-data-query'
import CommandCell from '../../../layouts/partials/KGridCommandCell.vue';
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import ProjectGrid from './Projects/ProjectGrid.vue'


// const ClientTypeList = Object.keys(ClientTYPE)
// // ClientTypeList.push("ALL");
// let selectedClientType = ref<ClientTYPE>();
let ClientData = ref<Client[]>([]);
let total = ref<number | undefined>(10);

const sort = ref<SortDescriptor[] | undefined>([]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const columns = [
  { field: 'code', title: 'Code' },
  { field: 'name', title: 'Name' },
  { field: 'description', title: 'Description' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'myTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;

const refreshDatas = async ( ) => {
  const api = await client.api(new QueryClients({ }))
  if (api.succeeded) {
    ClientData.value = api.response!.results ?? []

    gridData.data = process(ClientData.value, {
      sort: sort.value,
      // filter: filter.
    }).data;

    // console.log(gridData);

    total.value = process( 
      ClientData.value,{
          // filter: filter.value
      }).total;
  }
}


onMounted(async () => {
  await refreshDatas()
});

// unMounted
onUnmounted(() => {
  
});

const hasItemsInEdit =  computed(() => 
  gridData.data.filter(p => p.inEdit).length > 0
);

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
    const request = new DeleteClient({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      showNotifSuccess('Delete Client', 'Successfully deleted data 🎉')
    }
  }
}

const onEdit = (e: any) => {
  let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
  let updated = Object.assign({},gridData.data[index], {inEdit:true});
  gridData.data.splice(index, 1, updated);
}

const onInsert = () => {
  // Set Default Value
  const dataItem = { inEdit: true, isActive: true };
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
    const request = new CreateClient({
      code: currData.code,
      name : currData.name,
      description : currData.description,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Add Client', 'Successfully added new Client data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Client', 'Failed to add new Client data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Client', 'Failed to add new Client data')
      }
    }
  }
  else{
    const request = new UpdateClient({
      id : currData.id,
      code: currData.code,
      name : currData.name,
      description : currData.description,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Update Client', 'Successfully updated Client data 🎉')
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

const gridExpandChange = (e: any) => {
  e.dataItem[e.target.$props.expandField] = e.value;
}
</script>

<template>
  <!-- Hero --> 
  <BasePageHeading
    title="Clients Data"
    subtitle="Mobile friendly tables that work across all screen sizes."
  >
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Clients</li>
        </ol>
      </nav>
    </template>
  </BasePageHeading>
  <!-- END Hero -->

  <!-- Page Content -->
  <div class="content">
    <BaseBlock title="Client data">
      <kGrid ref="grid"
        :style="{height: '440px'}"
        :data-items="gridData"
        :edit-field="'inEdit'"
        :sortable="true"
        :pageable="true"
        :total="total"
        @itemchange="itemChange"
        @datastatechange="dataStateChange"
        @sortchange="sortChangeHandler"
        :columns="columns"
        :detail="'gridDetailTemplate'"
        @expandchange="gridExpandChange"
        :expand-field="'expanded'"
      >
        <kGridToolbar>
          <kbutton title="Add new" :theme-color="'primary'" @click='onInsert'>
              Add new
          </kbutton>
          <kbutton v-if="hasItemsInEdit"
              :theme-color="'info'"
                  title="Cancel current changes"
                  @click="onCancelChanges">
                  Cancel current changes
          </kbutton>
        </kGridToolbar>
        <template v-slot:gridDetailTemplate="{props}">
            <ProjectGrid 
              :selected-client-id="props.dataItem.id" 
              :client-list="gridData.data" 
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true"/>
        </template>
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
