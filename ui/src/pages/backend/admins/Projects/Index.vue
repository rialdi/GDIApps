<script setup lang="ts">
import { ref } from "vue"
// import { formatDate, formatCurrency } from "@/utils"
import { Client, QueryClients, ProjectView, QueryProjects, CreateProject, UpdateProject, DeleteProject } from "@/dtos"
import { client } from "@/api"

import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid';
import { Button as kButton} from '@progress/kendo-vue-buttons'
import { ComboBox as kComboBox} from '@progress/kendo-vue-dropdowns';
import { Form as kForm } from "@progress/kendo-vue-form";
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs';
import { process, filterBy, SortDescriptor, DataResult } from '@progress/kendo-data-query'

import { saveExcel } from '@progress/kendo-vue-excel-export';

import CommandCell from '@/layouts/partials/KGridCommandCell.vue';
import { showNotifError, showNotifSuccess } from '@/stores/commons'
import FormContent from './FormContent.vue';

const formContentRef = ref<InstanceType<typeof FormContent>>()
let kDialogTitle = ref<string>("Add Project")

onMounted(async () => {
  await getClientList()
  await refreshDatas()
});

/* Combobox Client */
const sourceClientList = ref<Client[]>([])
let clientList = ref<any[]>([])

const getClientList = async() => {
  const api = await client.api(new QueryClients({ isActive: true }))
  if (api.succeeded) {
    sourceClientList.value = api.response!.results ?? []
    clientList.value = process(sourceClientList.value, {}).data as any[]
  }
}

const cboClientOnChange = (e: any) => {
  if(e.value) {
    refreshDatas(e.value.id)
  } else {
    refreshDatas()
  }
}

const onCBOClientFilter = (e : any) => {
  const data = process(sourceClientList.value, {}).data as any[]
  clientList.value = filterBy(data, e.filter)
}
/* END of Combobox Client */

/* Code for DataGrid */
let ProjectData = ref<ProjectView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>(10)
let dataItemInEdit = ref<ProjectView>()
const sort = ref<SortDescriptor[] | undefined>([]);
// const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

const gridColumProperties = [
  // { field: 'clientId', title: 'Client', template:'{{ clientCode  | clientName }}' },
  // { field: 'clientCode', title: 'Client Name',  },
  { cell: 'clientTemplate', filterable: false, title: 'Client', width:300 },
  { field: 'code', title: 'Code' },
  { field: 'name', title: 'Name' },
  { field: 'description', title: 'Description' },
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 },
  { cell: 'myTemplate', filterable: false, title: 'Action', className:"center" , width:200 }
] as GridColumnProps[];

const refreshDatas = async (selectedClientId?: any ) => {
  // Create QueryObjecs based on selectedClientId
  const queryProjects = (selectedClientId) ? 
    new QueryProjects({ clientId: selectedClientId }) : 
    new QueryProjects() 

  const api = await client.api(queryProjects)
  if (api.succeeded) {
    ProjectData.value = api.response!.results ?? []
    gridData.data = process(ProjectData.value, {
      sort: sort.value,
      // filter: filter.
    }).data;
    total.value = process(ProjectData.value,{}).total;
    dataItemInEdit.value = undefined
  }
}
const onInsert = () => {
  kDialogTitle.value = "Add Project"
  // Set Default Value
  dataItemInEdit.value = {
    // code: '', name: "Project Name", 
    isActive: true
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteProject({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete Project', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit Project"
  dataItemInEdit.value = e.dataItem
  formContentRef.value?.focus
  // alert(JSON.stringify(dataItemInEdit.value, null, 2));
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}

const onResetForm = () => {
  formContentRef.value?.resetForm()
}

const onSave = async (e: any) => {
  const currData = e;
  // console.log(currData)
  if( currData.id == null) {
    const request = new CreateProject({
      clientId: currData.clientId,
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
      clientId: currData.clientId,
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

const onExportToExcel = () => {
  saveExcel({
      data: gridData.data as any[],
      fileName: "myFile",
      columns: gridColumProperties
  });
}

/* END Code for DataGrid */
</script>

<template>
  <!-- Hero --> 
  <BasePageHeading title="Projects Data" subtitle="This Project Data Edit is using External Form">
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

  <!-- Kendo Dialog for Editing Data -->
  <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="500" :title-render="'myTemplate'" >
    <template v-slot:myTemplate="{}">
        <div class="w-100">
          {{ kDialogTitle }} 
          <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" ></kButton>
          <!-- <span class="k-icon k-i-reset float-end" v-on:click="onResetForm"/> -->
        </div>
    </template>
    <kForm :initialValues="dataItemInEdit" @submit="onSave">
        <FormContent :client-list="clientList" ref="formContentRef"/>
    </kForm>
    <kDialogActionsBar>
      <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
      <!-- <kButton @click="onResetForm" :theme-color="'warning'"> Reset </kButton> -->
      <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!formContentRef?.formAllowSubmit"> Save </kButton>
    </kDialogActionsBar>
  </kDialog>
  <!-- END Kendo Dialog for Editing Data -->

  <!-- Page Content -->
  <div class="content">
    <!-- Page Filter Parameter -->
    <BaseBlock title="Search Parameter" btn-option-fullscreen btn-option-content>
      <div class="row pb-2">
        <div class="col-sm-2 text-end">
          <label class="mr-3 mt-1">Client</label>
        </div>
        <div class="col-sm-4">
          <kComboBox
              :style="{ width: '230px' }"
              :data-items="clientList"
              :value-field="'id'"
              :text-field="'name'"
              :filterable="true"
              @change="cboClientOnChange"
              @filterchange="onCBOClientFilter"
          ></kComboBox>
        </div>
      </div>
    </BaseBlock>
    <!-- END Page Filter Parameter -->

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content>  
      <!-- Main Data Grid -->
      <kGrid ref="grid"
          :style="{height: '440px'}"
          :data-items="gridData"
          :sortable="true"
          :pageable="true"
          :total="total"
          @sortchange="sortChangeHandler"
          :columns="gridColumProperties"
      >
        <kGridToolbar>
          <kButton title="Add new" :theme-color="'primary'" @click='onInsert'>
              Add new
          </kButton>
          <kButton title="Export to Excel" :theme-color="'primary'" @click='onExportToExcel'>
            Export to Excel
          </kButton>
        </kGridToolbar>
        <template v-slot:myTemplate="{props}">
            <CommandCell :data-item="props.dataItem" 
                    @edit="onEdit"
                    @save="onSave" 
                    @remove="onRemove"
                    @cancel="onCancelChanges"/>
        </template>
        <template v-slot:clientTemplate="{ props }">
          <td :colspan="1">
            {{ props.dataItem.clientCode + ' - ' + props.dataItem.clientName}}
          </td>
        </template>
        <template v-slot:isActiveTemplate="{ props }">
          <td :colspan="1" style="text-align:center">
            <input type="checkbox" id="isActive" v-model="props.dataItem.isActive" :disabled="!props.dataItem.inEdit"/>
          </td>
        </template>
      </kGrid>
      <!-- End Main Data Grid -->
    </BaseBlock>
    <!-- END Result Data Grid -->
  </div>
  <!-- END Page Content -->
</template>
