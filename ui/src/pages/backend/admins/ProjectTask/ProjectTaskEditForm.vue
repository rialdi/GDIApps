<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { client } from "@/api"
import { ProjectTeamView, QueryProjectTeams} from "@/dtos"

import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"
import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"
// import UploadFile from '@/components/form/UploadFile.vue'

import { ProjectTaskView, TASK_STATUS } from "@/dtos"
import { nameValidator } from "@/stores/validators"

import {  GridColumnProps } from '@progress/kendo-vue-grid'
// import { TabStrip as kTabStrip} from '@progress/kendo-vue-layout'
// import { Button as kButton} from '@progress/kendo-vue-buttons'
// import { Upload as kUpload } from '@progress/kendo-vue-upload'
// import { Dialog as kDialog} from '@progress/kendo-vue-dialogs'
// import { Input as kInput } from '@progress/kendo-vue-inputs'
import { process } from '@progress/kendo-data-query'

// import CommandCell from '@/layouts/partials/KGridCommandCell.vue'

const gridColumProjectTeamList = [
  { field: 'userName', title: 'User Name'},
  { field: 'projectTeamRole', title: 'Role'}
] as GridColumnProps[];

const sourceProjectTeamList = ref<ProjectTeamView[]>()
let projectTeamList = ref<any[]>([])
const getProjectTeamList = async () => {
    const api = await client.api(new QueryProjectTeams({ projectId: props.dataItem.projectId }))
    if (api.succeeded) {
        sourceProjectTeamList.value = api.response!.results ?? []
        projectTeamList.value = process(sourceProjectTeamList.value, {}).data as any[]
    }
}

// const sourceCContractList = ref<CContract[]>([])
// let cContractList = ref<any[]>([])
// const getCContractList = async() => {
//   const api = await client.api(new QueryCContracts({ clientId: dataItemInEdit.value.clientId }))
//   if (api.succeeded) {
//     sourceCContractList.value = api.response!.results ?? []
//     cContractList.value = process(sourceCContractList.value, {}).data as any[]
//   }
// }


const taskStatusList = Object.values(TASK_STATUS)
let dataItemInEdit = ref<ProjectTaskView>(new ProjectTaskView())
let planStart = ref<Date | undefined>()
let planEnd = ref<Date | undefined>()
let actualStart = ref<Date | undefined>()
let actualEnd = ref<Date | undefined>()
let taskStatus = ref<string>()

let showFormSubmitButton = ref<Boolean>(true)

let props = defineProps<{
    dataItem: any
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    resetForm()
});

const resetForm = async () => {
    getProjectTeamList()
    dataItemInEdit.value = Object.assign({}, props.dataItem as ProjectTaskView)
    planStart.value = toDate(dataItemInEdit.value?.planStart)
    planEnd.value = toDate(dataItemInEdit.value?.planEnd)
    actualStart.value = toDate(dataItemInEdit.value?.actualStart)
    actualEnd.value = toDate(dataItemInEdit.value?.actualEnd)
    taskStatus.value = dataItemInEdit.value?.status
}   

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.planStart = planStart.value == undefined ? undefined : toLocalISOString(planStart.value)
    dataItemInEdit.value.planEnd = planEnd.value == undefined ? undefined : toLocalISOString(planEnd.value)
    dataItemInEdit.value.actualStart = actualStart.value == undefined ? undefined : toLocalISOString(actualStart.value)
    dataItemInEdit.value.actualEnd = actualEnd.value == undefined ? undefined : toLocalISOString(actualEnd.value)
    dataItemInEdit.value.status = taskStatus.value as TASK_STATUS
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
                <KNumericTextBox id="taskNo" v-model="dataItemInEdit.no" 
                    :showLabel="true" :label="'No'" :format="'n0'"/>
            </div>
            <div class="col-6 p-1">
                <KNumericTextBox id="DurationDays" v-model="dataItemInEdit.durationDays" 
                    :showLabel="true" :label="'Duration Days'" :format="'n0'"/>
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <KTextInput id="ProjectName" v-model="dataItemInEdit.taskName" :validator="nameValidator"
                    :showLabel="true" :label="'Project Name'" :type="'string'" />
                
            </div>
            <div class="col-6 p-1">
                <KTextInput id="Description" v-model="dataItemInEdit.description" 
                    :showLabel="true" :label="'Description'" :type="'string'" />
                
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <KComboBox id="status" :data-items="taskStatusList" v-model="taskStatus" :value="taskStatus"
                    :showLabel="true" :label="'Status'" :valid="true" />
                
            </div>
            <div class="col-6 p-1">
                <KNumericTextBox id="completed" v-model="dataItemInEdit.completed" 
                    :showLabel="true" :label="'Completed'" :format="'n2'"/>
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <KDatePicker :id="'estimatedStartDate'"  v-model="planStart" 
                    :showLabel="true" :label="'Estimated Start Date'"/>
            </div>
            <div class="col-6 p-1">
                <KDatePicker :id="'estimatedEndDate'"  v-model="planEnd" 
                    :showLabel="true" :label="'Estimated End Date'"/>
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <KDatePicker :id="'actualStartDate'"  v-model="actualStart" 
                    :showLabel="true" :label="'Actual Start Date'"/>
            </div>
            <div class="col-6 p-1">
                <KDatePicker :id="'actualEndDate'"  v-model="actualEnd" 
                    :showLabel="true" :label="'Actual End Date'"/>
            </div>
        </div>
        <div class="row">
            <div class="col-6 p-1">
                <PopupSearchGrid :id="'assignTo'"  v-model="dataItemInEdit.projectTeamId"
                    :model-value-text-field="'userName'"
                    :grid-source-data="projectTeamList" :grid-colum-properties="gridColumProjectTeamList" 
                    :show-label="true" :label="'Assign To'" />
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