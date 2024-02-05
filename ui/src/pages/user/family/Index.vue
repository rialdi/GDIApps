<script setup lang="ts">


import FamilyMemberGrid from "./MainGrid.vue"

const mainGridRef = ref<InstanceType<typeof FamilyMemberGrid>>()

onMounted(() => {
  mainGridRef.value?.updateSelectedParentParam()
})

</script>

<template>
  <!-- Page Content -->
  <div class="content">
    <!-- Result Data Grid -->
    <BaseBlock title="Family Member" btn-option-fullscreen btn-option-content> 
      <!-- Main Data Grid --> 
      <FamilyMemberGrid ref="mainGridRef"
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