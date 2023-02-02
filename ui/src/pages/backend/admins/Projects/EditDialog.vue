<template>
    <kDialog @close="onCancel" width="500">
      <div class="k-form k-form-horizontal">
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
      </div>
      <kDialogActionsBar>
        <kButton @click="onCancel"> Cancel </kButton>
        <kButton :theme-color="'primary'" @click="onSave"> Save </kButton>
      </kDialogActionsBar>
    </kDialog>
</template>
<script setup lang="ts">
import { Dialog as kDialog, DialogActionsBar as kDialogActionsBar } from '@progress/kendo-vue-dialogs';
import { Input as kInput, Checkbox as kCheckbox} from '@progress/kendo-vue-inputs';
import { Label as klabel} from '@progress/kendo-vue-labels';
import { Button as kButton} from '@progress/kendo-vue-buttons';
import { Project } from "@/dtos"

const props = defineProps<{
    dataItem: Object
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
    (e:'cancel', dataItem: object): () => void
}>()

let dataItemInEdit = ref<Project>({
    code: "",
    isActive: true
})

onMounted(async () => {
    dataItemInEdit.value = props.dataItem
});

// onUpdated(async () => {
//     dataItemInEdit.value = props.dataItem
// });

const onCancel = async (e: any) => {
    emit('cancel', {dataItem:props.dataItem} )
}

const onSave = async (e: any) => {
    emit('save', {dataItem:props.dataItem} )
}

</script>