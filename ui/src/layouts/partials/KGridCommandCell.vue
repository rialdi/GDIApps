<template>
    <!-- <div class="content"> -->
    <td v-if="!(dataItem  as any)['inEdit']" class="k-command-cell">
        <!-- <div class="btn-group">
        <button type="button" class="btn btn-sm btn-alt-primary"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="Edit"
            @click="editHandler">
        <i class="fa fa-fw fa-pencil-alt"></i>
        </button>
        <button type="button" class="btn btn-sm btn-alt-danger"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="Delete"
            @click="removeHandler">
        <i class="fa fa-fw fa-times"></i>
        </button>
    </div> -->
        <kbutton icon="edit"
                 title="edit"
                 :theme-color="'primary'"
                 class="k-grid-edit-command"
                 @click="editHandler">
        </kbutton>
        <kbutton icon="delete"
                 title="delete"
                 :theme-color="'error'"
                 class="k-grid-remove-command"
                 @click="removeHandler">
        </kbutton>
      
    </td>
    <td v-else>
            <kbutton
                icon="save"
                :title="(dataItem as any).id? 'Update' : 'Add'"
                :theme-color="'warning'"
                style="width:30px; height:30px; margin-right:8px; color: white;"
                class="k-grid-save-command"
                @click="addUpdateHandler">
                <!-- {{(dataItem as any).id? 'Update' : 'Add'}} -->
            </kbutton>
            <kbutton
                icon="undo"
                :title="(dataItem as any).id? 'Cancel' : 'Discard'"
                :theme-color="'info'"
                style="width:30px; height:30px;"
                class="k-grid-cancel-command"
                @click="cancelDiscardHandler">
                <!-- {{(dataItem as any).id? 'Cancel' : 'Discard'}} -->
            </kbutton>
        </td>
    <!-- </div> -->
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
        },
        html: false as any,
        preConfirm: () => {
        return new Promise((resolve: any) => {
          setTimeout(() => {
            resolve();
          }, 50);
        });
      },
    }).then((result) => {
        // console.log(result)
        if (result.value) {
            emit('remove', {dataItem:props.dataItem})
        }
        else if (result.dismiss?.toString() === "cancel") {
            // result.dismiss can be 'overlay', 'cancel', 'close', 'esc', 'timer'
            // toast.fire("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });

}
const addUpdateHandler = () => emit('save', {dataItem:props.dataItem})
const cancelDiscardHandler = () => emit('cancel', {dataItem:props.dataItem})
</script>