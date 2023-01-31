<template>
    <p class="fs-sm text-muted">
    Coming from right with rotation animation
    </p>
    <button type="button" class="btn btn-alt-primary push" @click="showConfigm">
    Launch Modal {{ title }}
    </button>

    <!-- From Right Block Modal -->
    <div 
        class="modal fade"
        id="modal-block-fromright"
        tabindex="-1" 
        role="dialog"
        aria-labelledby="modal-block-fromright"
        aria-hidden="true"
    >
        <div id="confirmationDialog" class="modal-dialog modal-dialog-fromright modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="block block-rounded block-transparent mb-0">
                    <div class="block-header block-header-default">
                        <h3 class="block-title">{{ title }}</h3>
                        <div class="block-options">
                        <button type="button" class="btn-block-option" data-bs-dismiss="modal" aria-label="Close">
                            <i class="fa fa-fw fa-times"></i>
                        </button>
                        </div>
                    </div>
                    <div class="block-content fs-sm">
                        <p>
                        {{ questionText }}
                        </p>
                    </div>
                    <div class="block-content block-content-full text-end bg-body">
                        <button type="button" class="btn btn-sm btn-primary" @click="onConfirmYes" data-bs-dismiss="modal">Yes</button>
                        <button type="button" class="btn btn-sm btn-alt-secondary me-1" @click="onConfirmNo" data-bs-dismiss="modal">No</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END From Right Block Modal -->
</template>

<script setup lang="ts">
import { Modal } from 'bootstrap';
let title = "Delete Confirmation"
let questionText = "Are you sure to delete this object?asdsad"

// const dialogRef = ref()

interface ModalConfig {
    questionText?: string;
    title?: string;
}
// const props = defineProps({
//   questionText: {
//     type: String,
//     default: "Are You Sure to delete this object?",
//     description: "The HTML tag of the component (div, a)",
//   },
//   modelValye: {
//     type: String,
//     default: "Are You Sure to delete this object?",
//     description: "Confirmation Title",
//   }
// })

// const emit = defineEmits<{
//     (event: 'update:modelValue', payload: string): void;
// }>();

// let emit = defineEmits(['update:modelValue']);

// const emit = defineEmits<{
//     ['update:modelValue']
//     // (e:'update:modelValue', title:string): () => void
// }>()

// let config : ModalConfig = {
//     title: "Delete Confirmation",
//     questionText: "Are You Sure to delete this object"
// }

// let config = [{ title: '',  questionText: ''}];

// const closeModal = () => {
//     isOpen.value = false
// }

const emit = defineEmits<{
    (e:'onConfirmYes'): () => void
    (e:'onConfirmNo'): () => void
    // (e:'save', dataItem: object): () => void
    // (e:'cancel', dataItem: object): () => void
}>()

// const editHandler = () => emit('edit', {dataItem:props.dataItem})
// const removeHandler = () => emit('remove', {dataItem:props.dataItem})
// const addUpdateHandler = () => emit('save', {dataItem:props.dataItem})
// const cancelDiscardHandler = () => emit('cancel', {dataItem:props.dataItem})

const onConfirmYes = () => {
    // isOpen.value = false
    emit('onConfirmYes')
}

const onConfirmNo = () => {
    // isOpen.value = false
    emit('onConfirmNo')
}

const openModal = () => {
    const modalElement = document.getElementById('modal-block-fromright')
    if(modalElement != null)
    {
        const modal = new Modal(modalElement)
        modal.show();
    }
}

defineExpose({
    ShowConfirmation,
    title
})

function showConfigm(){
    ShowConfirmation({ title: "AAD", questionText: "sasda"})
    openModal()
}

function ShowConfirmation(modalConfig : ModalConfig) {
    // config.title = modalConfig
    // console.log(config);

    title = modalConfig.title as string
    questionText = modalConfig.questionText as string
    openModal()
}
</script>