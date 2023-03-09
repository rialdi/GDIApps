<template>
    <kTabStrip :tabs="tabList" :selected="selectedtab" @select="onSelectTab" class="w-100">
        <template v-slot:tabProject class="w-100 ">
            <form @submit.prevent="onSubmit" id="mainForm" class="k-form w-100 pl-3 pr-3">
                <fieldset>
                <div class="row">
                    <div class="col-6 p-1">
                        <KComboBox 
                            :id="'cboClient'" :showLabel="true" :label="'Client'" :valueField="'id'" :textField="'name'" :required="true"
                            :dataItems="clientList" v-model="dataItemInEdit.clientId" :value="dataItemInEdit.clientId" 
                            @change="cboClientOnChange"
                            :valid="true"
                        />
                    </div>
                    <div class="col-6 p-1">
                        <PopupSearchGrid :id="'cContractId'" v-model="dataItemInEdit.cContractId"
                            :grid-source-data="cContractList" 
                            :grid-colum-properties="gridColumPropCContract" 
                            :model-value-text-field="'contractNo'"
                            :show-label="true" :label="'Contract No'"
                            :filterable="true" :pageable="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KTextInput id="ProjectCode" v-model="dataItemInEdit.code" :validator="nameValidator"
                            :showLabel="true" :label="'Project Code'" :type="'string'" />
                    </div>
                    <div class="col-6 p-1">
                        <KTextInput id="ProjectName" v-model="dataItemInEdit.name" :validator="nameValidator"
                            :showLabel="true" :label="'Project Name'" :type="'string'" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KTextInput id="ProjectOwner" v-model="dataItemInEdit.projectOwner"
                            :showLabel="true" :label="'Project Owner'" :type="'string'" />
                    </div>
                    <div class="col-6 p-1">
                        <KTextInput id="ProjectManager" v-model="dataItemInEdit.projectManager"
                            :showLabel="true" :label="'Project Manager'" :type="'string'" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KTextInput id="Description" v-model="dataItemInEdit.description" 
                            :showLabel="true" :label="'Description'" :type="'string'" />
                    </div>
                    <div class="col-6 p-1">
                        <KNumericTextBox id="DurationDays" v-model="dataItemInEdit.durationDays" 
                            :showLabel="true" :label="'Duration Days'" :format="'n0'"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KDatePicker :id="'estimatedStartDate'"  v-model="projectEstimatedStartDate" 
                            :showLabel="true" :label="'Estimated Start Date'"/>
                    </div>
                    <div class="col-6 p-1">
                        <KDatePicker :id="'estimatedEndDate'"  v-model="projectEstimatedEndDate" 
                            :showLabel="true" :label="'Estimated End Date'"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 p-1">
                        <KDatePicker :id="'actualStartDate'"  v-model="projectActualStartDate" 
                            :showLabel="true" :label="'Actual Start Date'"/>
                    </div>
                    <div class="col-6 p-1">
                        <KDatePicker :id="'actualEndDate'"  v-model="projectActualEndDate" 
                            :showLabel="true" :label="'Actual End Date'"/>
                    </div>
                </div>
                </fieldset>
            </form>
        </template>
        <template v-slot:tabProjectDoc>
            <kGrid
            :edit-field="'inEdit'"
                :data-items="projectDocList"
                :columns="gridColProjectDocs"
                @itemchange="itemChangeProjectDoc"
            >
            <kGridToolbar class="k-form w-100">
                <kButton icon="add" title="Add New" :theme-color="'primary'" @click='addProjectDoc'>Add New</kButton>
            </kGridToolbar>
            <template v-slot:attFileTemplate="{ props }">
                <td :colspan="1" style="text-align:center">
                    <a :href="props.dataItem.attachmentUrl">
                        <span class="k-icon k-i-attachment-45"/>
                    </a>
                </td>
            </template>
            <template v-slot:actionTemplate="{props}">
                <CommandCell :data-item="props.dataItem" 
                        @edit="onEditProjectDoc"
                        @save="onSaveProjectDoc" 
                        @remove="onRemoveProjectDoc"
                        @cancel="onCancelChangesProjectDoc"/>
            </template>
            </kGrid>
        </template>
        <template v-slot:tabProjectTeam>
            <ProjectTeamGrid :selected-project-id="dataItemInEdit.id" />
        </template>
    </kTabStrip>

     <!-- Kendo Dialog for Editing Data -->
     <kDialog v-if="showUploadProjectDoc" width="60%" :title="'Add New Project Attachment'" @close="closeUploadNewProjectDoc" :title-render="'titleTemplate'">
        <template v-slot:titleTemplate="{}">
            <div class="w-100">
              Add New Project Attachment
              <kButton class="float-end" :theme-color="'primary'" icon="upload"  @click="onUploadProjectDoc" title="Upload">Upload</kButton>
            </div>
        </template>
        <UploadFile v-model="ProjectDocFile" @change="ProjectDocFileChange"/>
        <div class="w-100 mt-2">
            <kInput :style="{ width: '100%' }"  v-model="newProjectDocFileName" :label="'File Name'"></kInput>
        </div>
    </kDialog>
    <!-- END Kendo Dialog for Editing Data -->
</template>
    
<script setup lang="ts">
import { toDate, toLocalISOString } from '@servicestack/client'
import { client } from "@/api"

import KComboBox from "@/components/kendo/KComboBox.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
import KDatePicker from "@/components/kendo/KDatePicker.vue"
import KNumericTextBox from "@/components/kendo/KNumericTextBox.vue"
import PopupSearchGrid from "@/components/grids/PopupSearchGrid.vue"
import UploadFile from '@/components/form/UploadFile.vue'
import ProjectTeamGrid from './ProjectTeamGrid.vue'
// import UploadFile from '@/components/form/UploadFile.vue'

import { ProjectView, 
    ProjectDoc, QueryProjectDocs, CreateProjectDoc, UpdateProjectDoc, DeleteProjectDoc,
    CContract, QueryCContracts
} from "@/dtos"
import { nameValidator } from "@/stores/validators"

import { Grid as kGrid, GridToolbar as kGridToolbar, GridColumnProps } from '@progress/kendo-vue-grid'
import { TabStrip as kTabStrip} from '@progress/kendo-vue-layout'
import { Button as kButton} from '@progress/kendo-vue-buttons'
// import { Upload as kUpload } from '@progress/kendo-vue-upload'
import { Dialog as kDialog} from '@progress/kendo-vue-dialogs'
import { Input as kInput } from '@progress/kendo-vue-inputs'
import { process } from '@progress/kendo-data-query'

import CommandCell from '@/layouts/partials/KGridCommandCell.vue'

let dataItemInEdit = ref<ProjectView>(new ProjectView())
let projectEstimatedStartDate = ref<Date | undefined>()
let projectEstimatedEndDate = ref<Date | undefined>()
let projectActualStartDate = ref<Date | undefined>()
let projectActualEndDate= ref<Date | undefined>()

let showFormSubmitButton = ref<Boolean>(true)

let props = defineProps<{
    dataItem: any,
    clientList: any[]
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    resetForm()
});

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as ProjectView)
    projectEstimatedStartDate.value = toDate(dataItemInEdit.value.estimatedStartDate)
    projectEstimatedEndDate.value = toDate(dataItemInEdit.value.estimatedEndDate)
    projectActualStartDate.value = toDate(dataItemInEdit.value.actualtartDate)
    projectActualEndDate.value = toDate(dataItemInEdit.value.actualEndDate)
    refreshProjectDocs()
}   

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.estimatedStartDate = projectEstimatedStartDate.value == undefined ? undefined : toLocalISOString(projectEstimatedStartDate.value)
    dataItemInEdit.value.estimatedEndDate = projectEstimatedEndDate.value == undefined ? undefined : toLocalISOString(projectEstimatedEndDate.value)
    dataItemInEdit.value.actualtartDate = projectActualStartDate.value == undefined ? undefined : toLocalISOString(projectActualStartDate.value)
    dataItemInEdit.value.actualEndDate = projectActualEndDate.value == undefined ? undefined : toLocalISOString(projectActualEndDate.value)
    emit('save', { dataItem : dataItemInEdit.value })
}

/* Combobox Client */
const cboClientOnChange = (e: any) => {
    getCContractList()
}
/* End Comboboc Client */

/* Popup Search */
/* CContract */
const gridColumPropCContract = [
  { field: 'contractNo', title: 'Contract No', width:150 },
  { field: 'description', title: 'description'},
  { field: 'currency', title: 'Currency'},
  { field: 'totalAmount', title: 'Total Amount', format:"{0:n0}"},
  { field: 'isActive', title: 'Is Active', cell: 'isActiveTemplate', width:85 }
] as GridColumnProps[];

const sourceCContractList = ref<CContract[]>([])
let cContractList = ref<any[]>([])
const getCContractList = async() => {
  const api = await client.api(new QueryCContracts({ clientId: dataItemInEdit.value.clientId }))
  if (api.succeeded) {
    sourceCContractList.value = api.response!.results ?? []
    cContractList.value = process(sourceCContractList.value, {}).data as any[]
  }
}
/* END CContract */


const selectedtab = ref<number>(0)
const tabList = [
    { title: "Project", content: "tabProject"},
    { title: "Docs", content: "tabProjectDoc" },
    { title: "Team", content: "tabProjectTeam" }
]

const onSelectTab = (e: any) => {
    selectedtab.value = e.selected

    // Show Hide Parent Submit button
    showFormSubmitButton.value = (e.selected == 0)
}

const closeUploadNewProjectDoc = () => {
    showUploadProjectDoc.value = false
}

let projectDocList = ref<ProjectDoc[]>([])
const refreshProjectDocs = async() => {
    const api = await client.api(new QueryProjectDocs({ projectId: dataItemInEdit.value.id }))
    if (api.succeeded) {
        const data  = api.response!.results ?? []
        projectDocList.value = process(data, {}).data as ProjectDoc[]
    }
    showUploadProjectDoc.value = false
}

const gridColProjectDocs = [
  { field: 'fileName', title: 'File Name'},
  { cell: 'attFileTemplate', title: 'File', width:50},
  { cell: 'actionTemplate', filterable: false, title: 'Action', className:"center" , width:95 }
] as GridColumnProps[];

let showUploadProjectDoc = ref<boolean>()
let newProjectDocFileName = ref<string>()


const addProjectDoc = (e : any) => {
    showUploadProjectDoc.value = true
}

// const onBeforeUploadProjectDoc = (event: any) => {
//   const attachmentUrl = "/uploads/ProjectAttachments/[" + dataItemInEdit.value.id + "]-" + event.files[0].name
//   event.additionalData.ProjectId = dataItemInEdit.value.id 
//   event.additionalData.fileName = newProjectDocFileName.value
//   event.additionalData.attachmentUrl = attachmentUrl
// }

// const onAddProjectAttachment = (event: any) => {
//     newProjectDocFileName.value = event.affectedFiles[0].name
// }

// const onStatusChangeProjectDoc = (event: any) => {
//   if (event.response) {
//     refreshProjectDoc()
//   }
// }

/* Upload Project Attachment File */
let ProjectDocFile = ref<File |undefined> ()
const ProjectDocFileChange = (e: any) => {
    newProjectDocFileName.value = ProjectDocFile.value?.name
}
const onUploadProjectDoc = async () => {
    const formData = new FormData()
    formData.set("ProjectId", dataItemInEdit.value.id?.toString() as string)
    formData.set("fileName", newProjectDocFileName.value as string)
    formData.set("AttachmentUrl", ProjectDocFile.value as Blob)

    let api = await client.apiForm(new CreateProjectDoc(), formData)
    if(api.succeeded) {
        refreshProjectDocs()
    }
    else {
        console.log(api)
    }
}
/* END Upload Project Attachment File */

const onRemoveProjectDoc = async(e: any) => {
    const request = new DeleteProjectDoc({
        id: e.dataItem.id
    });
    const api = await client.api(request)
    if (api.succeeded) {
        refreshProjectDocs()
    }
}

const itemChangeProjectDoc = (e : any) => {
    if (e.dataItem.id) {
        let index = projectDocList.value.findIndex(p => p.id === e.dataItem.id);
        let updated = Object.assign({},projectDocList.value[index], {[e.field]:e.value});
        projectDocList.value.splice(index, 1, updated);
    } else {
        e.dataItem[e.field] = e.value;
    }
}

const onEditProjectDoc = (e: any) => {
    let index = projectDocList.value.findIndex(p => p.id === e.dataItem.id);
    let updated = Object.assign({},projectDocList.value[index], {inEdit:true});
    projectDocList.value.splice(index, 1, updated);
}

const onCancelChangesProjectDoc = () => {
    refreshProjectDocs()
}

const onSaveProjectDoc = async (e: any) => {
    const currData = e.dataItem;
    const request = new UpdateProjectDoc({
        id : currData.id,
        fileName: currData.fileName
    })
    const api = await client.api(request)
    if (api.succeeded) {
        refreshProjectDocs()
    } 
}

defineExpose({
    showFormSubmitButton,
    resetForm
})
</script>

<style>
    .swal2-container {
        z-index: 10005;
    }
    
</style>