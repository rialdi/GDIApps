<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { client } from "@/api"
import { TimeSheetView, ProjectView, QueryProjects, AppUser, GetUserList} from "@/dtos"

// import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"
import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"
// import UploadFile from '@/components/form/UploadFile.vue'

// import { ProjectTaskView, TASK_STATUS } from "@/dtos"
// import { nameValidator } from "@/stores/validators"

import {  GridColumnProps } from '@progress/kendo-vue-grid'
// import { TabStrip as kTabStrip} from '@progress/kendo-vue-layout'
// import { Button as kButton} from '@progress/kendo-vue-buttons'
// import { Upload as kUpload } from '@progress/kendo-vue-upload'
// import { Dialog as kDialog} from '@progress/kendo-vue-dialogs'
// import { Input as kInput } from '@progress/kendo-vue-inputs'
import { process } from '@progress/kendo-data-query'

// import CommandCell from '@/layouts/partials/KGridCommandCell.vue'

const gridColumProjectList = [
  { field: 'clientCode', title: 'Client Code'},
  { field: 'clientName', title: 'Client Name'},
  { field: 'code', title: 'Code'},
  { field: 'Name', title: 'Name'},
] as GridColumnProps[];

const sourceProjectList = ref<ProjectView[]>()
let projectList = ref<any[]>([])
const getProjecList = async () => {
    const api = await client.api(new QueryProjects({}))
    if (api.succeeded) {
        sourceProjectList.value = api.response!.results ?? []
        projectList.value = process(sourceProjectList.value, {}).data as any[]
    }
}

let userList = ref<AppUser[]>([])
let gridColUserList = [
  { field: 'userName', title: 'User Name' },
  { field: 'email', title: 'Email'},
] as GridColumnProps[];

const getUserList = async() => {
  const api = await client.api(new GetUserList())
  if (api.succeeded) {
    userList.value = api.response! as any[]?? []
  }
}


let dataItemInEdit = ref<TimeSheetView>(new TimeSheetView())
let timeSheetDate = ref<Date | undefined>()

let showFormSubmitButton = ref<Boolean>(true)

let props = defineProps<{
    dataItem: any
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted( () => {
    // getUserList()
    resetForm()
});

const resetForm = () => {
    getUserList()
    getProjecList()
    dataItemInEdit.value = Object.assign({}, props.dataItem as TimeSheetView)
    timeSheetDate.value = toDate(dataItemInEdit.value?.tsDate)
}   

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.tsDate = timeSheetDate.value == undefined ? undefined : toLocalISOString(timeSheetDate.value)
    emit('save', { dataItem : dataItemInEdit.value })
}

defineExpose({
    showFormSubmitButton,
    resetForm
})
</script>

<template>
    <form @submit.prevent="onSubmit" id="mainForm" class="k-form w-100 pl-3 pr-3">
        <fieldset>
        <div class="row">
            <div class="col-6 p-1">
                <div class="col-6 p-1">
                    <KDatePicker :id="'tsDate'"  v-model="timeSheetDate" 
                        :showLabel="true" :label="'Date'"/>
                </div>
            </div>
            <div class="col-6 p-1">
                <PopupSearchGrid :id="'user'"  v-model="dataItemInEdit.appUserId" :value="dataItemInEdit.appUserUserName"
                    :model-value-text-field="'userName'"
                    :grid-source-data="userList" :grid-colum-properties="gridColUserList" 
                    :show-label="true" :label="'User'" :filterable="true" />
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <PopupSearchGrid :id="'project'"  v-model="dataItemInEdit.projectId"
                    :model-value-text-field="'code'"
                    :grid-source-data="projectList" :grid-colum-properties="gridColumProjectList" 
                    :show-label="true" :label="'Project'" :filterable="true" />
            </div>
            <div class="col-6 p-1">
                <KNumericTextBox id="taskNo" v-model="dataItemInEdit.no" 
                    :showLabel="true" :label="'No'" :format="'n0'"/>
            </div>
        </div>
        <div class="row">
            <div class="col-12 p-1">
                <KTextInput :id="'taskName'" :type="'text'" v-model="dataItemInEdit.taskName" :show-label="true" :label="'TaskName'"  />
            </div>
        </div>
       
        </fieldset>
    </form>
</template>
<style>
    .swal2-container {
        z-index: 10005;
    }
    
</style>