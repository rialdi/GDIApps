<script setup lang="ts">
import { onMounted, ref } from "vue"
// import { EVENT_STATUS } from "@/dtos"
// import { client } from "@/api"
// import KCombobox  from "@/components/kendo/KComboBox.vue" 
// import KTextInput  from "@/components/kendo/KTextInput.vue"
import { Button as KButton} from '@progress/kendo-vue-buttons'
// import { process, filterBy } from '@progress/kendo-data-query'
import MainGrid from "./MainGrid.vue"

// import { nameValidator } from "@/stores/validators"

const mainGridRef = ref<InstanceType<typeof MainGrid>>()
// const router = useRouter()

// const isAdmin = ref<boolean>(true)
// const showEventList = ref<boolean>(true)

// const paramCode = ref<string>()
// const paramName = ref<string>()

onMounted(async () => {
    refreshData()
});

const refreshData = () => {
    // console.log("selectedEventStatus.value")
    // console.log(selectedEventStatus.value)
    // console.log("paramCode.value")
    // console.log(paramCode.value)
    // console.log("paramName.value")
    // console.log(paramName.value)
    mainGridRef.value?.updateSelectedParentParam()
}

const btnSearchOnClick = () => {
    refreshData()
}

/* Combobox UEve

/* Combobox Gender */
// const eventStatusList = Object.values(EVENT_STATUS)
// let selectedEventStatus = ref<string | undefined>()

// const cboEventStatusOnChange = (e: any) => {
//   refreshData()
// }

/* END of Combobox Gender */

// /* Combobox Gender */
// const checkinStatusList = Object.values(CHECKIN_STATUS)
// let selectedCheckinStatus = ref<string | undefined>()

// const cboCheckinStatusOnChange = (e: any) => {
//   refreshData()
// }

// /* END of Combobox Gender */

// const onSubmitCheckin = async (e : any) => {
//   console.log('onSubmitCheckin')
//   console.log(checkinConfirmationCode.value)

//   const api = await client.api(new CheckInParticipant({
//     confirmationCode: checkinConfirmationCode.value
//    }))
//   if (api.succeeded) {
//     showNotifSuccess('Checkin', 'Successfully 🎉')
//   }

//   // selectedGender.value = (selectedGender.value == "MALE")? "FEMALE" : "MALE"
//   // refreshData()

//   // showNotifSuccess('Delete CBank', 'Successfully deleted data 🎉')
// }

</script>

<template>
    <!-- Hero --> 
    <BasePageHeading title="Daily Scrum" subtitle="Daily Scrum Meeting">
      <template #extra>
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb breadcrumb-alt">
            <li class="breadcrumb-item" aria-current="page">Daily Scrum</li>
          </ol>
        </nav>
      </template>
    </BasePageHeading>
    <!-- END Hero -->
  
    <!-- Page Content -->
    <div class="content">
      <!-- Page Filter Parameter -->
      <BaseBlock title="Search Parameter" btn-option-fullscreen btn-option-content>
        <template #options>
            <KButton id="btnSearch" @click="btnSearchOnClick" :theme-color="'primary'" style="width:100px">Search</KButton>
        </template>
        <!-- <div class="row pb-2">
            <div class="col">
            <KCombobox
                :id="'cboEventStatus'"
                :label="'Status'"
                :label-position="'top'"
                :valid="true"
                :data-items="eventStatusList"
                v-model="selectedEventStatus"
                :value="selectedEventStatus"
                :filterable="false"
            ></KCombobox>
          </div>
            <div class="col">
                <KTextInput id="paramCode" v-model="paramCode"  :label-position="'top'"
                    :showLabel="true" :label="'Code'" 
                    :type="'string'" :valid="true" />
            </div>
            <div class="col">
                <KTextInput id="paramName" v-model="paramName"  :label-position="'top'"
                    :showLabel="true" :label="'Name'" 
                    :type="'string'" :valid="true" />
            </div>
        </div> -->
      </BaseBlock>
      <!-- END Page Filter Parameter -->
      
      <!-- Result Data Grid -->
      <BaseBlock title="Result data" btn-option-fullscreen btn-option-content> 
        <!-- Main Data Grid --> 
        <MainGrid ref="mainGridRef" 
            :filterable="false" 
            :sortable="true" 
            :pageable="true" 
            :show-export-button="true"
        />
        <!-- End Main Data Grid -->
      </BaseBlock>
      <!-- END Result Data Grid -->
    </div>
    <!-- END Page Content -->
</template>
  
  