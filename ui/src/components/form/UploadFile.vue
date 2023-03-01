<template>
    <input type="file" @change="onChange($event)">
</template>

<script setup lang="ts">

// const props = 
defineProps<{
    modelValue : File | undefined
}>()

const emit = defineEmits<{
    (e:'update:modelValue', value:File): () => void
}>()

const onChange = (event : any ) => {
    // console.log(event.target.files)
    if(event.target.files.length > 0) {
        emit('update:modelValue', event.target.files[0])
    }
}

</script>