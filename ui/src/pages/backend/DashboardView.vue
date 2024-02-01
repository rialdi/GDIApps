<script setup lang="ts">
// import { client } from "@/api"
import { auth, selectedRole } from "@/auth"

const routerLinkOverViewClass = ref<string>('block-content block-content-full block-content-sm fs-sm fw-medium d-flex align-items-center justify-content-between')
</script>

<template>
    <!-- Hero -->
    <div class="content">
        <div class="d-flex flex-column flex-md-row justify-content-md-between align-items-md-center py-2 text-center text-md-start">
            <div class="flex-grow-1 mb-1 mb-md-0">
                <h1 class="h3 fw-bold mb-2">Dashboard</h1>
                <h2 class="h6 fw-medium fw-medium text-muted mb-0">
                Welcome
                <RouterLink
                    :to="{ name: 'user-profile' }"
                    class="fw-semibold"
                    >{{ auth?.displayName }}
                </RouterLink>, everything looks great.
                </h2>
            </div>
        </div>
    </div>
    <!-- END Hero -->

    <!-- Page Content -->
    <div class="content">
        <!-- Overview -->
    <div class="row items-push">
        <div v-if="selectedRole == 'Admin'" class="col-sm-6 col-xxl-3">
            <!-- Users -->
            <BaseBlock class="d-flex flex-column h-100 mb-0">
            <template #content>
                <div class="block-content block-content-full flex-grow-1 d-flex justify-content-between align-items-center">
                <dl class="mb-0">
                    <dt class="fs-3 fw-bold">124</dt>
                    <dd class="fs-sm fw-medium fs-sm fw-medium text-muted mb-0">
                    Users
                    </dd>
                </dl>
                <div class="item item-rounded-lg bg-body-light">
                    <i class="far fa-user-circle fs-3 text-primary"></i>
                </div>
                </div>
                <div class="bg-body-light rounded-bottom">
                    <RouterLink :to="{ name: 'user-profile' }" :class="routerLinkOverViewClass" >
                        View all Users <i class="fa fa-arrow-alt-circle-right ms-1 opacity-25 fs-base"></i>
                    </RouterLink>
                </div>
            </template>
            </BaseBlock>
            <!-- END Users -->
        </div>
      <div class="col-sm-6 col-xxl-3">
        <!-- Pending Events -->
        <BaseBlock class="d-flex flex-column h-100 mb-0">
          <template #content>
            <div class="block-content block-content-full flex-grow-1 d-flex justify-content-between align-items-center">
              <dl class="mb-0">
                <dt class="fs-3 fw-bold">32</dt>
                <dd class="fs-sm fw-medium fs-sm fw-medium text-muted mb-0">
                    Outstanding Tasks
                </dd>
              </dl>
              <div class="item item-rounded-lg bg-body-light">
                <i class="far fa-gem fs-3 text-primary"></i>
              </div>
            </div>
            <div class="bg-body-light rounded-bottom">
                <RouterLink :to="{ name: 'user-profile' }" :class="routerLinkOverViewClass" >
                    View all my tasks <i class="fa fa-arrow-alt-circle-right ms-1 opacity-25 fs-base"></i>
                </RouterLink>
            </div>
          </template>
        </BaseBlock>
        <!-- END Events -->
      </div>
      
      <div class="col-sm-6 col-xxl-3">
        <!-- Messages -->
        <BaseBlock class="d-flex flex-column h-100 mb-0">
          <template #content>
            <div class="block-content block-content-full flex-grow-1 d-flex justify-content-between align-items-center">
              <dl class="mb-0">
                <dt class="fs-3 fw-bold">45</dt>
                <dd class="fs-sm fw-medium fs-sm fw-medium text-muted mb-0">
                  Messages
                </dd>
              </dl>
              <div class="item item-rounded-lg bg-body-light">
                <i class="far fa-paper-plane fs-3 text-primary"></i>
              </div>
            </div>
            <div class="bg-body-light rounded-bottom">
                <RouterLink :to="{ name: 'user-profile' }" :class="routerLinkOverViewClass" >
                    View all messages <i class="fa fa-arrow-alt-circle-right ms-1 opacity-25 fs-base"></i>
                </RouterLink>
            </div>
          </template>
        </BaseBlock>
        <!-- END Messages -->
      </div>
      <div class="col-sm-6 col-xxl-3">
        <!-- Conversion Rate -->
        <BaseBlock class="d-flex flex-column h-100 mb-0">
          <template #content>
            <div
              class="block-content block-content-full flex-grow-1 d-flex justify-content-between align-items-center"
            >
              <dl class="mb-0">
                <dt class="fs-3 fw-bold">4.5%</dt>
                <dd class="fs-sm fw-medium fs-sm fw-medium text-muted mb-0">
                  Conversion Rate
                </dd>
              </dl>
              <div class="item item-rounded-lg bg-body-light">
                <i class="fa fa-chart-bar fs-3 text-primary"></i>
              </div>
            </div>
            <div class="bg-body-light rounded-bottom">
              <a
                class="block-content block-content-full block-content-sm fs-sm fw-medium d-flex align-items-center justify-content-between"
                href="javascript:void(0)"
              >
                <span>View statistics</span>
                <i
                  class="fa fa-arrow-alt-circle-right ms-1 opacity-25 fs-base"
                ></i>
              </a>
            </div>
          </template>
        </BaseBlock>
        <!-- END Conversion Rate-->
      </div>
    </div>
    <!-- END Overview -->
    </div>
    <!-- END Page Content -->
</template>