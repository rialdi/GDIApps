<script setup lang="ts">
import { ref } from "vue"
// import { formatDate, formatCurrency } from "@/utils"
import { Project, QueryProjects, CreateProject, UpdateProject, DeleteProject } from "@/dtos"
import { client } from "@/api"
import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid';
import { Button as kbutton} from '@progress/kendo-vue-buttons'
import { process, SortDescriptor, DataResult } from '@progress/kendo-data-query'
import CommandCell from '@/layouts/partials/KGridCommandCell.vue';
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import EditDialog from './EditDialog.vue';

let ProjectData = ref<Project[]>([])
let total = ref<number | undefined>(10)
let dataItemInEdit = ref<Project>()

const sort = ref<SortDescriptor[] | undefined>([]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const columns = [
  { field: 'clientId', title: 'Code' },
  { field: 'code', title: 'Code' },
  { field: 'name', title: 'Name' },
  { field: 'description', title: 'Description' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'myTemplate', filterable: false, title: 'Action', className:"center" , width:200 }
] as GridColumnProps[];

let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;

const refreshDatas = async ( ) => {
  const api = await client.api(new QueryProjects({ }))
  if (api.succeeded) {
    ProjectData.value = api.response!.results ?? []

    gridData.data = process(ProjectData.value, {
      sort: sort.value,
      // filter: filter.
    }).data;

    // console.log(gridData);

    total.value = process( 
      ProjectData.value,{
          // filter: filter.value
      }).total;
    
      dataItemInEdit.value = undefined
  }
}

onMounted(async () => {
  await refreshDatas()
});

const hasItemsInEdit =  computed(() => 
  gridData.data.filter(p => p.inEdit).length > 0
);

const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteProject({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
    }
  }
}

const onEdit = (e: any) => {
  dataItemInEdit.value = e.dataItem
}

const onInsert = () => {
  // Set Default Value
  dataItemInEdit.value = {code: 'New Code', name: "Project Name", isActive: true}
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
  if( currData.id == null) {
    const request = new CreateProject({
      code: currData.code,
      name : currData.name,
      description : currData.description,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Add Project', 'Successfully added new Project data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add Project', 'Failed to add new Project data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add Project', 'Failed to add new Project data')
      }
    }
  }
  else{
    const request = new UpdateProject({
      id : currData.id,
      code: currData.code,
      name : currData.name,
      description : currData.description,
      isActive : currData.isActive
    })
    const api = await client.api(request)
    if (api.succeeded) {
      refreshDatas();
      showNotifSuccess('Update Project', 'Successfully updated Project data 🎉')
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
    title="Projects Data"
    subtitle="This Project Data Edit is using External Form"
  >
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Projects</li>
        </ol>
      </nav>
    </template>
  </BasePageHeading>
  <!-- END Hero -->

  <!-- Page Content -->
  <div class="content">
    <EditDialog v-if="dataItemInEdit" :data-item="dataItemInEdit" @save="onSave" @cancel="onCancelChanges">
    </EditDialog>
    <BaseBlock title="Project data">
      <!-- <Create v-if="isNewData" class="max-w-screen-sm" @done="onSave" />
      <Edit v-else-if="editDataId" :id="editDataId" class="max-w-screen-sm" @done="onSave" />
      <OutlineButton v-else @click="() => reset({isNewData:true})">
        New Booking
      </OutlineButton> -->
      
      <kGrid ref="grid"
        ::style="{height: '440px'}"
            :data-items="gridData"
            :sortable="true"
            :pageable="true"
            :total="total"
            @sortchange="sortChangeHandler"
            :columns="columns"
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
