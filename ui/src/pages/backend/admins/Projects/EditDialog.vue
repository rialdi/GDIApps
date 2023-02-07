<template>
    <kDialog @close="onCancel" width="500" class="p-0">
      <k-form :initialValues="props.dataItem" @submit="onSave">
      <FormContent />
   </k-form>
      <!-- <kForm :initialValues="dataItemInEdit" @submit="onSave" class="m-0">
        <kFormElement >
          <fieldset class="k-form-fieldset">
            <kField :id="'code'" :name="'code'" :label="'Code'" :component="'inputTemplate'"
                  :validator="requiredValidator" >
              <template v-slot:inputTemplate="{props}">
                <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus">
                </FormInput>
              </template>
            </kField>
            <kField :id="'name'" :name="'name'" :label="'Name'" :component="'inputTemplate'"
                  :validator="requiredValidator" >
              <template v-slot:inputTemplate="{props}">
                <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus">
                </FormInput>
              </template>
            </kField>
            <kField :id="'isActive'" :name="'isActive'" :label="'Is Active'" :component="'inputTemplate'">
              <template v-slot:inputTemplate="{props}">
                <FormCheckbox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus">
                </FormCheckbox>
              </template>
            </kField>
            
            <kDialogActionsBar>
              <kButton @click="onCancel"> Cancel </kButton>
              <kButton :theme-color="'primary'" :type="'submit'" :disabled="!kendoForm?.allowSubmit"> Save </kButton>
            </kDialogActionsBar>
          </fieldset>
        </kFormElement>
      </kForm> -->
      <!-- <div class="k-form k-form-horizontal">
        <div class="k-form-field">
          <label for="client" class="k-form-label"> Client</label>
          <div class="k-form-field-wrap">
            <kComboBox
                :style="{ width: '230px' }"
                :data-items="clientList"
                :value-field="'id'"
                :text-field="'name'"
                :value-primitive="true"
                v-model="dataItemInEdit.clientId"
            ></kComboBox>
          </div>
        </div>
        <div class="k-form-field">
          <label for="code" class="k-form-label"> Code</label>
          <div class="k-form-field-wrap">
            <kInput
              :style="{ width: '230px' }"
              type="text"
              :name="'name'"
              v-model="dataItemInEdit.code"
            ></kInput>
          </div>
        </div>
        <div class="k-form-field">
          <label for="Name" class="k-form-label"> Name</label>
          <div class="k-form-field-wrap">
            <kInput
              :style="{ width: '230px' }"
              type="text"
              :name="'name'"
              v-model="dataItemInEdit.name"
            ></kInput>
          </div>
        </div>
        <div class="k-form-field">
          <label for="Description" class="k-form-label"> Description</label>
          <div class="k-form-field-wrap">
            <kInput
              :style="{ width: '230px' }"
              type="text"
              :name="'description'"
              v-model="dataItemInEdit.description"
            ></kInput>
          </div>
        </div>
        <div class="k-form-field">
          <klabel :editor-id="'isActive'"> Is Active </klabel>
          <div class="k-form-field-wrap">
            <span>
              <kCheckbox
                :name="'isActive'"
                :id="'isActive'"
                v-model="dataItemInEdit.isActive"
              />
            </span>
          </div>
        </div>
      </div> -->
      <!-- <kDialogActionsBar>
        <kButton @click="onCancel"> Cancel </kButton>
        <kButton :theme-color="'primary'" :type="'submit'" > Save </kButton>
      </kDialogActionsBar> -->
    </kDialog>
</template>
<script setup lang="ts">
import { Dialog as kDialog } from '@progress/kendo-vue-dialogs';
import { Project } from "@/dtos"
import FormContent from './FormContent.vue';

const props = defineProps<{
    dataItem: object,
    clientList: any[]
}>()

// const kendoForm = inject<any>('kendoForm', {})

// const emit = defineEmits<{
//     (e:'save', dataItem: object): () => void
//     (e:'cancel', dataItem: object): () => void
// }>()

let dataItemInEdit = ref<Project>(props.dataItem)

onMounted(async () => {
    dataItemInEdit.value = props.dataItem
}); 

// onUpdated(async () => {
//     dataItemInEdit.value = props.dataItem
// });

const onCancel = async (e: any) => {
    // console.log(kendoForm);
    // emit('cancel', {dataItem:dataItemInEdit} )
}

const onSave = async (e: any) => {
    console.log(e)
    //emit('save', {dataItem: props.dataItem} )
}

// const handleSubmit = async (e: any) => {
//     console.log(e)
//     //emit('save', {dataItem: props.dataItem} )
// }

</script>