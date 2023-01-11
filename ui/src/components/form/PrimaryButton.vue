<template>
  <!-- <RouterLink
    :to="{ name: 'home' }"
    class="fw-bold fs-lg tracking-wider text-dual me-2"
  >
    GDI
    <span class="fw-normal">Apps</span>
  </RouterLink> -->
  <RouterLink v-if="href" :to="href" v-slot="{ navigate }">
    <button :class="cls" :href="href" @click="navigate">
      <slot></slot>
    </button>
  </RouterLink>
  <button v-else :type="type" :class="cls" v-bind="$attrs">
    <slot></slot>
  </button>
</template>

<script setup lang="ts">
const props = withDefaults(defineProps<{ type?: "submit" | "button" | "reset", href?: string, color?: string }>(), {
  type: 'submit',
})

const cls = "inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
    .replace(/indigo/g, props.color ?? "indigo")
</script>