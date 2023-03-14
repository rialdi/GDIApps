<script setup lang="ts">
import { onMounted, ref } from "vue"
import { toLocalISOString } from '@servicestack/client'
import { Button as kButton} from '@progress/kendo-vue-buttons'
import { DateRangePicker, SelectionRange } from '@progress/kendo-vue-dateinputs';

import TimeSheetGrid from "./TimeSheetGrid.vue"
import KComboBox from "@/components/kendo/KComboBox.vue"

import { useClientProjectStore } from "@/stores/clientproject"
import moment from "moment";
const clientProjectStore = useClientProjectStore()

const mainGridtRef = ref<InstanceType<typeof TimeSheetGrid>>()


let today = moment().toDate()
let todayLastMonth = moment(today).subtract(1, "month")
let startOfMonth = moment(todayLastMonth).startOf('month').toDate()
let endOfMonth = moment(startOfMonth).add(1,"month").subtract(1,"day").toDate()
let paramDate = ref<SelectionRange>()
let defaultDate = { start : startOfMonth, end: endOfMonth}

const dateRangeOnChange = (e : any) => {
  paramDate.value = e.value
}

onMounted(async () => {
  clientProjectStore.refreshClientProjects()
  paramDate.value = defaultDate

  btnSearchOnClick()
});



const btnSearchOnClick = () => {
  const startDateString = paramDate.value?.start != undefined ? toLocalISOString(paramDate.value?.start) : undefined
  const endDateString = paramDate.value?.end != undefined ? toLocalISOString(paramDate.value?.end) : undefined
  
  mainGridtRef.value?.resetGrid(
    clientProjectStore.selectedClientId, 
    clientProjectStore.selectedProjectId, 
    startDateString, 
    endDateString
  )
}

/* END of Combobox Client */

</script>

<template>
  <!-- Hero --> 
  <BasePageHeading title="Projects Task Data" subtitle="This Project Data Edit is using External Form">
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Projects Task</li>
        </ol>
      </nav>
    </template>
  </BasePageHeading>
  <!-- END Hero -->

  <!-- Page Content -->
  <div class="content">
    <!-- Page Filter Parameter -->
    <BaseBlock title="Search Parameter" btn-option-fullscreen btn-option-content>
      
      <div class="row">
            <div class="col-6 p-1">
              <KComboBox :id="'cboClient'" :show-label="false" :label="'Client'" 
                v-model="clientProjectStore.selectedClientId"
                :data-items="clientProjectStore.clientList" 
                :valid="true"
                :value-field="'id'"
                :text-field="'name'"
                :filterable="true"
                @change="clientProjectStore.onChangeClient"/>
                <!-- <span>Project Id : {{ clientProjectStore.selectedClientId }}</span> -->
            </div>
          <div class="col-6 p-1">
            <KComboBox :id="'cboProject'" :show-label="false" :label="'Project'" 
                v-model="clientProjectStore.selectedProjectId"
                :value="clientProjectStore.selectedProjectId"
                :data-items="clientProjectStore.projectList" 
                :valid="true"
                :value-field="'id'"
                :text-field="'name'"
                :filterable="true"/>
                <!-- <span>Project Id : {{ clientProjectStore.selectedProjectId }}</span> -->
          </div>
      </div>
      <div class="row">
        <div class="col-6 p-1">
          <DateRangePicker :value="paramDate" :max="today" @change="dateRangeOnChange" :default-value="defaultDate" :format="'dd-MMM-yyyy'" />
        </div>
      </div>
    </BaseBlock>
    <!-- END Page Filter Parameter -->

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
      <template #options>
        <kButton  icon="search"  @click="btnSearchOnClick" title="Search Data" :theme-color="'primary'">Search Data</kButton>
      </template>
      <!-- Main Data Grid --> 
      <TimeSheetGrid ref="mainGridtRef"
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true" 
              :selected-client-id="clientProjectStore.selectedClientId"
              :selected-project-id="clientProjectStore.selectedProjectId" />
      <!-- End Main Data Grid -->
    </BaseBlock>
    <!-- END Result Data Grid -->
  </div>
  <!-- END Page Content -->
</template>


