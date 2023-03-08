<script setup lang="ts">
import { onMounted, ref } from "vue"
import { Client, QueryClients } from "@/dtos"
import { client } from "@/api"
import { ComboBox as kComboBox} from '@progress/kendo-vue-dropdowns'
import { process, filterBy } from '@progress/kendo-data-query'
// import ProjectGrid from "./ProjectGrid.vue"

import MainGrid from "./MainGrid.vue"


// import provinces from "@/data/regions/provinces"
// import cities from "@/data/regions/cities"

const mainGridtRef = ref<InstanceType<typeof MainGrid>>()

// const projectGridtRef = ref<InstanceType<typeof ProjectGrid>>()

onMounted(async () => {
  await getClientList()
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
  // console.log(clientList.value)
}

const cboClientOnChange = (e: any) => {
  selectedClientId.value = e.value ? e.value.id : undefined
  mainGridtRef.value?.updateSelectedClientId(selectedClientId.value)
  // mainGridtRef.value?.refreshDatas(selectedClientId.value)
}

const onCBOClientFilter = (e : any) => {
  const data = process(sourceClientList.value, {}).data as any[]
  clientList.value = filterBy(data, e.filter)
}

let selectedClientId = ref<number | undefined>()
/* END of Combobox Client */

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

  <!-- Page Content -->
  <div class="content">
    <!-- Page Filter Parameter -->
    <BaseBlock title="Search Parameter" btn-option-fullscreen btn-option-content>
      <div class="row pb-2">
        <div class="col-sm-2 text-end">
          <label class="mr-3 mt-1">Client</label>
        </div>
        <div class="col-sm-4">
          <kComboBox :id="'clientParam'"
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
      <MainGrid ref="mainGridtRef"
              :selected-client-id="selectedClientId" 
              :client-list="clientList" 
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true"/>

      <!-- <ProjectGrid ref="projectGridtRef"
              :selected-client-id="selectedClientId" 
              :client-list="clientList" 
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true"/> -->
      <!-- End Main Data Grid -->
    </BaseBlock>
    <!-- END Result Data Grid -->
  </div>
  <!-- END Page Content -->
</template>


