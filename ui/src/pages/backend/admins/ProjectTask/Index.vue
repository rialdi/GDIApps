<script setup lang="ts">
import { onMounted, ref } from "vue"
import { Client, QueryClients } from "@/dtos"
import { client } from "@/api"
// import { ComboBox as kComboBox} from '@progress/kendo-vue-dropdowns'
import { process } from '@progress/kendo-data-query'
import ProjectTaskGrid from "./ProjectTaskGrid.vue"
import KComboBox from "@/components/kendo/KComboBox.vue"

const mainGridtRef = ref<InstanceType<typeof ProjectTaskGrid>>()

onMounted(async () => {
  await getClientList()
});

/* Combobox Client */
const sourceClientList = ref<Client[]>([])
let clientList = ref<any[]>([])
let projectList = ref<any[]>([])
const getClientList = async() => {
  const api = await client.api(new QueryClients({ isActive: true }))
  if (api.succeeded) {
    sourceClientList.value = api.response!.results ?? []
    clientList.value = process(sourceClientList.value, {}).data as any[]
  }
}

const cboProjectOnChange = (e: any) => {
  // console.log(e.value)
  mainGridtRef.value?.updateselectedProjectId(e.value)
}

const cboClientOnChange = (e: any) => {
  let selectedData = clientList.value.find(p => p.id === e.value);
  if(selectedData?.projectList)
  {
    projectList.value = process(selectedData?.projectList, {}).data as any[] 
  }
  else
  {
    selectedProjectId.value = undefined
    mainGridtRef.value?.updateselectedProjectId(selectedProjectId.value)
    projectList.value = []
  }
  
}


let selectedClientId = ref<number | undefined>()
let selectedProjectId = ref<number | undefined>(0)
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
              <KComboBox :id="'cboClient'" :show-label="true" :label="'Client'" v-model="selectedClientId"
                :data-items="clientList" 
                :valid="true"
                :value-field="'id'"
                :text-field="'name'"
                :filterable="true"
                @change="cboClientOnChange"/>
            </div>
          <div class="col-6 p-1">
            <KComboBox :id="'cboProject'" :show-label="true" :label="'Project'" v-model="selectedProjectId"
                :data-items="projectList" :value="selectedProjectId" 
                :valid="true"
                :value-field="'id'"
                :text-field="'name'"
                :filterable="true"
                @change="cboProjectOnChange"/>
          </div>
      </div>
    </BaseBlock>
    <!-- END Page Filter Parameter -->

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <ProjectTaskGrid ref="mainGridtRef"
              :client-list="clientList" 
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true" 
              :selected-project-id="selectedProjectId" :value="selectedProjectId"/>

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


