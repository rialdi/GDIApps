<script setup lang="ts">
import { onMounted, ref } from "vue"
import { Client, QueryClients } from "@/dtos"
import { client } from "@/api"
import KComboBox  from "@/components/kendo/KComboBox.vue" 
import { process, filterBy } from '@progress/kendo-data-query'
import ProjectGrid from "./MainGrid.vue"

const mainGridtRef = ref<InstanceType<typeof ProjectGrid>>()

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
  selectedClientId.value = e.value ?? undefined
  mainGridtRef.value?.updateSelectedClientId(selectedClientId.value)
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
      <div class="row">
          <div class="col-6">
            <!-- :style="{ width: '50%' }" -->
            <KComboBox :id="'clientParam'"
                :data-items="clientList"
                :value-field="'id'"
                :text-field="'name'"
                :filterable="true"
                :label="'Client'"
                :label-position="'left'"
                :valid="true"
                @change="cboClientOnChange"
                @filterchange="onCBOClientFilter"
            ></KComboBox>
        </div>
      </div>
    </BaseBlock>
    <!-- END Page Filter Parameter -->

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <ProjectGrid ref="mainGridtRef"
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


