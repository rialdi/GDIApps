<script setup lang="ts">
import { client } from "@/api"
import { 
    TimeSheet, Client, 
    Project, ProjectTask,
    QueryClients, 
    QueryProjects, QueryProjectTasks
} from "@/dtos"
import { requiredValidator } from "@/stores/validators"
import { toDate } from "@servicestack/client"

import { Field as KField, FormElement as KFormElement } from "@progress/kendo-vue-form"
import FormDatePicker from "@/components/kform/FormDatePicker.vue"
import FormTextArea from "@/components/kform/FormTextArea.vue"
import FormComboBox from "@/components/kform/FormComboBox.vue"
// import FormNumericTextBox from "@/components/kform/FormNumericTextBox.vue"
// import FormDatePicker from "@/components/kform/FormDatePicker.vue"

let dataItemInEdit = ref<TimeSheet>(new TimeSheet())


let props = 
defineProps<{
    dataItem: any
}>()

// const emit = 
defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    getListClient()
    resetForm()
});

const onSave = (e: any) => {
    console.log('save')
}

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as TimeSheet)
    // birthDate.value = toDate(dataItemInEdit.value.birthDate)
    // dataItemInEdit.value.profileUrl = "/"
    // endDateInEdit.value = toDate(dataItemInEdit.value.endDate)
}

const clientList = ref<Client[]> ([])
const projectList = ref<Project[]> ([])
const projectTaskList = ref<ProjectTask[]> ([])

const getListClient = async () => {
  const api = await client.api(new QueryClients({}))
  if (api.succeeded) {
    clientList.value = api.response!.results ?? []
  }
}


const clientOnChange = (e : any) => {
    console.log(dataItemInEdit.value.clientId)
    console.log(e.value)
    dataItemInEdit.value.clientId = e.value
    console.log(e)
    getListProject()
}

const getListProject = async () => {
  const api = await client.api(new QueryProjects({clientId : dataItemInEdit.value.clientId}))
  if (api.succeeded) {
    projectList.value = api.response!.results ?? []
  }
}


const getListProjectTask = async () => {
  const api = await client.api(new QueryProjectTasks({projectId : dataItemInEdit.value.projectId}))
  if (api.succeeded) {
    projectList.value = api.response!.results ?? []
  }
}

defineExpose({
    resetForm,
    onSave
    // AllowSubmit
})

</script>

<template>
    <KFormElement id="memberForm1">
        <fieldset class="k-form-fieldset">
            <div class="row">
                <KField class="col-md p-1" :name="'tsDate'" :label="'Timesheet Date'" :component="'myTemplate'" 
                    :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormDatePicker v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :value="toDate(props.value)"/>
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'clientId'" :label="'Client'" :component="'myTemplate'" 
                    :data-items="clientList" :valueField="'id'" :textField="'name'" :value-primitive="true" :data-item-key="'id'" @change="clientOnChange">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'projectId'" :label="'Project'" :component="'myTemplate'" 
                    :data-items="projectList" :valueField="'id'" :textField="'name'" :value-primitive="true" :data-item-key="'id'" @change="getListProjectTask">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
                <KField class="col-md p-1" :name="'projectTaskId'" :label="'Project Task'" :component="'myTemplate'" 
                    :data-items="projectTaskList" :valueField="'id'" :textField="'name'"  :value-primitive="true" :data-item-key="'id'">
                    <template v-slot:myTemplate="{props}">
                        <FormComboBox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" />
                    </template>
                </KField>
            </div>
            <div class="row">
                <KField class="col-md p-1" :name="'notes'" :label="'Notes'" :component="'myTemplate'" :validator="requiredValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormTextArea v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"></FormTextArea>
                    </template>
                </KField>
            </div>
        </fieldset>
    </KFormElement>
</template>