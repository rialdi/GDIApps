<template>
    <!-- Meta Info Section -->
    <div class="hero-static col-lg-4 d-none d-lg-flex flex-column justify-content-center">
        <div class="p-4 p-xl-5 flex-grow-1 d-flex align-items-center">
          <div class="w-100">
            <RouterLink :to="{ name: 'home' }" lass="link-fx fw-semibold fs-2 text-white">
              GDI <span class="fw-normal">Apps</span>
            </RouterLink>
            <p class="text-white-75 me-xl-8 mt-2">
              Welcome to your amazing app. Feel free to login and start managing
              your projects and clients.
            </p>
          </div>
        </div>
        <div class="p-4 p-xl-5 d-xl-flex justify-content-between align-items-center fs-sm">
          <p class="fw-medium text-white-50 mb-0">
            <strong>{{ store.app.name + " " + store.app.version }}</strong>
            &copy; {{ store.app.copyright }}
          </p>
          <ul class="list list-inline mb-0 py-2">
            <li class="list-inline-item">
              <a class="text-white-75 fw-medium" href="javascript:void(0)">Legal</a>
            </li>
            <li class="list-inline-item">
              <a class="text-white-75 fw-medium" href="javascript:void(0)"
                >Contact</a
              >
            </li>
            <li class="list-inline-item">
              <a class="text-white-75 fw-medium" href="javascript:void(0)"
                >Terms</a
              >
            </li>
          </ul>
        </div>
      </div>
      <!-- END Meta Info Section -->
</template>

<script setup lang="ts">
import { useTemplateStore } from "@/stores/template";
const store = useTemplateStore();
</script>