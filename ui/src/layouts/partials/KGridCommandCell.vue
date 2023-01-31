<template>
    <td v-if="!(dataItem  as any)['inEdit']" class="k-command-cell">
        <kbutton
            :theme-color="'primary'"
            class="k-grid-edit-command"
            @click="editHandler">
            Edit
        </kbutton>
        <kbutton
            class="k-grid-remove-command"
            @click="removeHandler">
            Remove
        </kbutton>
    </td>
    <td v-else>
            <kbutton
                class="k-grid-save-command"
                @click="addUpdateHandler">
                {{(dataItem as any).id? 'Update' : 'Add'}}
            </kbutton>
            <kbutton
                class="k-grid-cancel-command"
                @click="cancelDiscardHandler">
                {{(dataItem as any).id? 'Cancel' : 'Discard'}}
            </kbutton>
        </td>
    </template>
<style lang="scss">
// SweetAlert2
@import "sweetalert2/dist/sweetalert2.min.css";
</style>
<script setup lang="ts">
import { Button as kbutton } from '@progress/kendo-vue-buttons';
// import { Title } from 'chart.js';
import Swal from "sweetalert2";

// Set default properties
let toast = Swal.mixin({
  buttonsStyling: false,
  target: "#page-container",
  customClass: {
    confirmButton: "btn btn-success m-1",
    cancelButton: "btn btn-danger m-1",
    input: "form-control",
  },
});
    
const props = defineProps<{
    // id: Number,
    // field: { type: String, required: false },
    dataItem: Object,
    // format: String,
    // className: String,
    // columnIndex: Number,
    // columnsCount: Number,
    // rowType: String,
    // level: Number,
    // expanded: Boolean,
    // editor: String,
    // inEdit: Boolean,
}>()

const emit = defineEmits<{
    (e:'edit', dataItem: object): () => void
    (e:'remove', dataItem: object): () => void
    (e:'save', dataItem: object): () => void
    (e:'cancel', dataItem: object): () => void
}>()

const editHandler = () => emit('edit', {dataItem:props.dataItem})
const removeHandler = () => {
    toast.fire({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        customClass: {
            confirmButton: "btn btn-danger m-1",
            cancelButton: "btn btn-secondary m-1",
        }
    }).then((result) => {
        // console.log(result)
        if (result.value) {
            emit('remove', {dataItem:props.dataItem})
            // toast.fire(
            //   "Deleted!",
            //   "Your imaginary file has been deleted.",
            //   "success"
            // );
        }
        else if (result.dismiss?.toString() === "cancel") {
            // toast.fire("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });

    emit('remove', {dataItem:props.dataItem})
}
const addUpdateHandler = () => emit('save', {dataItem:props.dataItem})
const cancelDiscardHandler = () => emit('cancel', {dataItem:props.dataItem})
</script>