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
<script setup lang="ts">
import { Button as kbutton } from '@progress/kendo-vue-buttons';
    
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
const removeHandler = () => emit('remove', {dataItem:props.dataItem})
const addUpdateHandler = () => emit('save', {dataItem:props.dataItem})
const cancelDiscardHandler = () => emit('cancel', {dataItem:props.dataItem})
</script>