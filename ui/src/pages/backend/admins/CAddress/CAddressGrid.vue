<template>
    <!-- Kendo Dialog for Editing Data -->
    <kDialog v-if="dataItemInEdit" @close="onCancelChanges" width="500" :title-render="'myTemplate'" >
        <template v-slot:myTemplate="{}">
            <div class="w-100">
            {{ kDialogTitle }} 
            <kButton class="float-end" icon="refresh" :fill-mode="'flat'" @click="onResetForm" ></kButton>
            <!-- <span class="k-icon k-i-reset float-end" v-on:click="onResetForm"/> -->
            </div>
        </template>
        <EditForms ref="editFormsRef" :data-item="dataItemInEdit" @save="onSave" />
        <!-- <kForm :initialValues="dataItemInEdit" @submit="onSave">
            <EditForm :client-list="props.clientList" ref="editFormRef"/>
        </kForm> -->
        <kDialogActionsBar>
        <kButton @click="onCancelChanges" :theme-color="'secondary'" ref="cancelDialog"> Cancel </kButton>
        <!-- <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" :disabled="!editFormRef?.formAllowSubmit"> Save </kButton> -->
        <kButton :theme-color="'primary'" :type="'submit'" Form="mainForm" > Save </kButton>
        </kDialogActionsBar>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
    
    <!-- Main Data Grid -->
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
    <kGridToolbar>
        <kButton icon="add" title="Add New" :theme-color="'primary'" @click='onInsert'>Add New</kButton>
        <kButton v-if="props.showExportButton" icon="excel" title="Export to Excel" :theme-color="'primary'" @click='onExportToExcel'>Export to Excel</kButton>
    </kGridToolbar>
    <template v-slot:actionTemplate="{props}">
        <CommandCell :data-item="props.dataItem" 
                @edit="onEdit"
                @save="onSave" 
                @remove="onRemove"
                @cancel="onCancelChanges"/>
    </template>
    <template v-slot:responsiveTemplate="{ props }">
        <td :colspan="1">
        <strong>Client</strong>
        <p>
        {{ props.dataItem.clientCode + ' - ' + props.dataItem.clientName }}
        </p>
        <strong>CAddress {{ (props.dataItem.isActive ? "Is Active" : "Not Active" ) }}</strong>
        <p>
        {{ props.dataItem.code + ' - ' + props.dataItem.name }}
        </p>
        </td>
    </template>
    <template v-slot:clientTemplate="{ props }">
        <td :colspan="1">
        {{ props.dataItem.clientCode + ' - ' + props.dataItem.clientName}}
        </td>
    </template>
    <template v-slot:isMainTemplate="{ props }">
        <td :colspan="1" style="text-align:center">
        <input type="checkbox" id="isMain" v-model="props.dataItem.isMain" :disabled="!props.dataItem.inEdit"/>
        </td>
    </template>
    </kGrid>
    <!-- End Main Data Grid -->
</template>
<script setup lang="ts">
import { client } from "@/api"
import { CAddressView, QueryCAddresss, CreateCAddress, UpdateCAddress, DeleteCAddress } from "@/dtos"
import { showNotifError, showNotifSuccess } from '@/stores/commons'

import { Button as kButton} from '@progress/kendo-vue-buttons'
// import { Form as kForm } from "@progress/kendo-vue-form"
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs'
import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { saveExcel } from '@progress/kendo-vue-excel-export';
import { process, SortDescriptor, CompositeFilterDescriptor, DataResult } from '@progress/kendo-data-query'

import CommandCell from '@/layouts/partials/KGridCommandCell.vue'
// import EditForm from './EditForm.vue'
import EditForms from './EditForms.vue'

const props = defineProps<{
    selectedClientId: any,
    clientList: any[],
    filterable?: boolean,
    sortable?: boolean,
    pageable?: boolean,
    showExportButton?: boolean
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

const editFormsRef = ref<InstanceType<typeof EditForms>>()

// const editFormRef = ref<InstanceType<typeof EditForm>>()
let kDialogTitle = ref<string>("Add CAddress")

const onShowHideColumns = () => {
  if(currWindowType.value != lastWindowType.value && width.value > 100) {
    gridColumProperties.map( col => {
      if(col.title != "Action") {
        if(col.title == "CAddresss"){
          col.hidden = !col.hidden
        } else {
          col.hidden = !col.hidden
        }
      }
    })
    lastWindowType.value = currWindowType.value
    refreshDatas(props.selectedClientId)
  }
}

onMounted(async () => {
  window.addEventListener('resize', onShowHideColumns)
  await refreshDatas(props.selectedClientId)
});

let CAddressData = ref<CAddressView[]>([])
let gridData = ref<DataResult>({ data: [] as any, total: 0 }).value;
let total = ref<number | undefined>(10)
let dataItemInEdit = ref<CAddressView>()
const sort = ref<SortDescriptor[] | undefined>([]);
const filter = ref<CompositeFilterDescriptor>({logic: "and", filters: []});

let gridColumProperties = [
  { cell: 'responsiveTemplate', filterable: false, title: 'CAddresss', hidden: true },
  { cell: 'clientTemplate', filterable: false, title: 'Client'},
  { field: 'addressName', title: 'Address Name', width:150 },
  { field: 'phoneNo', title: 'Phone No'},
  // { field: 'description', title: 'Description'},
  { field: 'isMain', title: 'Is Main', cell: 'isMainTemplate', width:85 },
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

const refreshDatas = async (selectedClientId?: any ) => {
  const queryCAddresss = (selectedClientId) ? 
    new QueryCAddresss({ clientId: selectedClientId }) : 
    new QueryCAddresss() 

  const api = await client.api(queryCAddresss)
  if (api.succeeded) {
    CAddressData.value = api.response!.results ?? []
    gridData.data = process(CAddressData.value, {
      sort: sort.value,
      filter: filter.value
    }).data;
    total.value = process(CAddressData.value,{}).total;
    dataItemInEdit.value = undefined
  }
}

const onInsert = () => {
  kDialogTitle.value = "Add CAddress"
  // Set Default Value
  dataItemInEdit.value = {
  }
}
const onRemove = async(e: any) => {
  if( e.dataItem !== null) {
    let index = gridData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
    gridData.data.splice(index, 1);
    const request = new DeleteCAddress({
      id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
      // showNotifSuccess('Delete CAddress', 'Successfully deleted data 🎉')
    }
  }
}
const onEdit = (e: any) => {
  kDialogTitle.value = "Edit CAddress"
  dataItemInEdit.value = e.dataItem
  // editFormRef.value?.focus
}

const onCancelChanges = () => {
  dataItemInEdit.value = undefined 
}

const onResetForm = () => {
  // editFormRef.value?.resetForm()
  editFormsRef.value?.resetForm()
}

const onSave = async (e: any) => {
  const currData = e.dataItem;
  // console.log("Country : " + currData.country)
  // return
  if( currData.id == null) {
    const request = new CreateCAddress({
      clientId: currData.clientId,
      addressName : currData.addressName,
      country : currData.country,
      province : currData.province,
      city : currData.city,
      district : currData.district,
      village : currData.village,
      address1 : currData.address1,
      address2 : currData.address2,
      postalCode : currData.postalCode,
      phoneNo : currData.phoneNo,
      isMain : currData.isMain
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas(props.selectedClientId)
        showNotifSuccess('Add CAddress', 'Successfully added new CAddress data 🎉')
    } else {
      if(api.error != null) {
        showNotifError('Add CAddress', 'Failed to add new CAddress data.<br/>' + api.error.message)
      }
      else {
        showNotifError('Add CAddress', 'Failed to add new CAddress data')
      }
    }
  }
  else{
    const request = new UpdateCAddress({
      id : currData.id,
      clientId: currData.clientId,
      addressName : currData.addressName,
      country : currData.country,
      province : currData.province,
      city : currData.city,
      district : currData.district,
      village : currData.village,
      address1 : currData.address1,
      address2 : currData.address2,
      postalCode : currData.postalCode,
      phoneNo : currData.phoneNo,
      isMain : currData.isMain
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshDatas(props.selectedClientId)
        showNotifSuccess('Update CAddress', 'Successfully updated CAddress data 🎉')
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
    refreshDatas(props.selectedClientId)
  }
}

const filterChangeHandler = (e: any) => {
  filter.value = e.filter
  refreshDatas(props.selectedClientId)
}

const onExportToExcel = () => {
  saveExcel({
      data: gridData.data as any[],
      fileName: "myFile",
      columns: gridColumProperties
  });
}

defineExpose({
    refreshDatas
})

</script>