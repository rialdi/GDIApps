<script setup lang="ts">
import { onMounted, ref } from "vue"
// import { Client, QueryClients } from "@/dtos"
// import { client } from "@/api"
// import { ComboBox as kComboBox} from '@progress/kendo-vue-dropdowns'
// import { process, filterBy } from '@progress/kendo-data-query'
import UserListGrid from "./UserListGrid.vue"

const mainGridtRef = ref<InstanceType<typeof UserListGrid>>()

onMounted(async () => {
});


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

    <!-- Result Data Grid -->
    <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <UserListGrid ref="mainGridtRef"
              :filterable="false" 
              :sortable="true" 
              :pageable="true" 
              :show-export-button="true"/>
      <!-- End Main Data Grid -->
    </BaseBlock>
    <!-- END Result Data Grid -->
  </div>
  <!-- END Page Content -->
</template>


