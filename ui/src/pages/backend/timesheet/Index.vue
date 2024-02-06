<script setup lang="ts">


import TimeSheetGrid from "./MainGrid.vue"

const mainGridRef = ref<InstanceType<typeof TimeSheetGrid>>()

onMounted(() => {
  mainGridRef.value?.updateSelectedParentParam()
})

</script>

<template>
  <!-- Page Content -->
  <div class="content">
    <!-- Result Data Grid -->
    <BaseBlock title="Time Sheet" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <TimeSheetGrid ref="mainGridRef"
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