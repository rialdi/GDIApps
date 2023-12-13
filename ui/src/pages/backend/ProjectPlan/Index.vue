<script setup lang="ts">
import { onMounted, ref } from "vue"
import { Client, QueryClients, Project, QueryProjects } from "@/dtos"
import { client } from "@/api"
import KComboBox  from "@/components/kendo/KComboBox.vue" 
import { process, filterBy } from '@progress/kendo-data-query'

import ProjectPlanGrid from "./ProjectPlanGrid.vue"

const mainGridRef = ref<InstanceType<typeof ProjectPlanGrid>>()

onMounted(async () => {
  await getClientList()
});

/* Combobox Project */
const sourceProjectList = ref<Project[]>([])
let projectList = ref<any[]>([])

const getProjectList = async() => {
    const api = await client.api(new QueryProjects({ 
        clientId: selectedClientId.value,
        isActive: true 
    }))

    if (api.succeeded) {
        sourceProjectList.value = api.response!.results ?? []
        projectList.value = process(sourceProjectList.value, {}).data as any[]
    }
}

const cboProjectOnChange = (e: any) => {
  selectedProjectId.value = e.value ?? undefined
  mainGridRef.value?.updateSelectedProjectId(selectedProjectId.value)
}

const cboProjectOnFilter = (e : any) => {
  const data = process(sourceProjectList.value, {}).data as any[]
  projectList.value = filterBy(data, e.filter)
}

let selectedProjectId = ref<number | undefined>()
    
/* END of Combobox Client */

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
  getProjectList()
//   mainGridRef.value?.updateSelectedClientId(selectedClientId.value)
}

const onCBOClientFilter = (e : any) => {
  const data = process(sourceClientList.value, {}).data as any[]
  clientList.value = filterBy(data, e.filter)
}

let selectedClientId = ref<number | undefined>()
    
/* END of Combobox Client */


let selectedVersionNo = ref<number | undefined>()


</script>

<template>
    <!-- Hero --> 
  <BasePageHeading title="Project Plans Data" subtitle="This Project Plan Data">
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Project Plans</li>
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
            <div class="col-6">
                <!-- :style="{ width: '50%' }" -->
                <KComboBox :id="'cboProject'"
                    :data-items="projectList"
                    :value-field="'id'"
                    :text-field="'name'"
                    :filterable="true"
                    :label="'Project'"
                    :label-position="'left'"
                    :valid="true"
                    @change="cboProjectOnChange"
                    @filterchange="cboProjectOnFilter"
                ></KComboBox>
            </div>
        </div>
    </BaseBlock>
    <!-- END Page Filter Parameter -->

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <ProjectPlanGrid ref="mainGridRef"
              :selected-project-id="selectedProjectId" 
              :selected-version-no="selectedVersionNo"
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :groupable="false"
              :show-export-button="false"/>
    </BaseBlock>
    <!-- END Result Data Grid -->
  </div>
  <!-- END Page Content -->
</template>