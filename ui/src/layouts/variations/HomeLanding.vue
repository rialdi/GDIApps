<script setup>
import NavItem from "@/components/NavItem.vue"
import NavLink from "@/components/NavLink.vue"
import SecondaryButton from "@/components/form/SecondaryButton.vue"
import PrimaryButton from "@/components/form/PrimaryButton.vue"

import { signout } from "@/auth"

import { useTemplateStore } from "@/stores/template";

import BaseLayout from "@/layouts/BaseLayout.vue";

// Main store
const store = useTemplateStore();

// Set default elements for this layout
store.setLayout({
  header: true,
  sidebar: false,
  sideOverlay: false,
  footer: false,
});

// Set various template options for this layout variation
store.headerStyle({ mode: "light" });
store.mainContent({ mode: "boxed" });
</script>

<template>
  <BaseLayout>
    <!-- Header Content Left -->
    <!-- Using the available v-slot, we can override the default Side Overlay content from layouts/partials/Header.vue -->
    <template #header-content-left>
      <div class="d-flex align-items-center">
        <!-- Logo -->
        <RouterLink
          :to="{ name: 'home' }"
          class="fw-bold fs-lg tracking-wider text-dual me-2"
        >
          GDI
          <span class="fw-normal">Apps</span>
        </RouterLink>
        <!-- END Logo -->

        <!-- Version -->
        <div class="fs-xs fw-medium py-1 px-3 rounded-pill bg-body-dark text-dark">
          {{ store.app.version }}
        </div>
      </div>
    </template>
    <!-- END Header Content Left -->

    <!-- Header Content Right -->
    <!-- Using the available v-slot, we can override the default Side Overlay content from layouts/partials/Header.vue -->
    <template #header-content-right>
      <NavItem show="auth">
        <SecondaryButton class="m-2" @click="signout($router)">Sign Out</SecondaryButton>
      </NavItem>
      <NavItem hide="auth">
        <SecondaryButton class="m-2" href="auth/signin">Sign In</SecondaryButton>
      </NavItem>
      <NavItem hide="auth">
        <PrimaryButton class="m-2" href="auth/signup">Register</PrimaryButton>
      </NavItem>
    </template>
    <!-- END Header Content Right -->
  </BaseLayout>
</template>
