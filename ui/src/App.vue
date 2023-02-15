<script setup lang="ts">
import { useApp } from "@/api"
import { Tooltip as kTooltip } from '@progress/kendo-vue-tooltip';
// import '@progress/kendo-theme-default/dist/all.css';
// import "@/styles/gdi.css"

// https://github.com/vueuse/head
// you can use this to manipulate the document head in any components,
// they will be rendered correctly in the html results with vite-ssg
useHead({
  title: 'GDIApps',
  meta: [
    { name: 'description', content: 'Vite SSG Starter Template' },
  ],
})

let app = useApp()
app.load()
</script>

<template>
  <kTooltip :anchor-еlement="'target'"  :open-delay="100" :paren-title="true">
    <router-view />
  </kTooltip>
  <!-- <RouterView /> -->
</template>

<style lang="scss">
// Main Stylesheet
@import "@/assets/scss/main";

// All color themes are included and available by default
// Feel free to comment out any of them if you won't use them in your project
@import "@/assets/scss/oneui/themes/amethyst";
@import "@/assets/scss/oneui/themes/city";
@import "@/assets/scss/oneui/themes/flat";
@import "@/assets/scss/oneui/themes/modern";
@import "@/assets/scss/oneui/themes/smooth";
</style>
