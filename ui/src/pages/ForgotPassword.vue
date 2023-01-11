<template>
  <!-- Page Content -->
  <BaseBackground image="/assets/media/photos/photo28@2x.jpg">
    <div class="row g-0 bg-primary-dark-op">
      <AuthMetaInfoSection/>
      <!-- Main Section -->
      <div class="hero-static col-lg-8 d-flex flex-column align-items-center bg-body-extra-light">
        <div class="p-4 w-100 flex-grow-1 d-flex align-items-center">
          <div class="w-100">
            <!-- Header -->
            <div class="text-center mb-2">
              <p class="mb-3">
                <i class="fa fa-2x fa-circle-notch text-primary-light"></i>
              </p>
              <h1 class="fw-bold mb-2">Forgot Password</h1>
              <p class="fw-medium text-muted">
                Please provide your account’s email or username and we will send you your password.
              </p>
            </div>
            <!-- END Header -->

            <!-- Main Form -->
            <div class="row g-0 justify-content-center">
              <div class="col-sm-8 col-xl-6">
                <form @submit.prevent="onSubmit">
                  <div class="shadow overflow-hidden sm:rounded-md">
                    <ErrorSummary except="userName"/>
                    <div class="px-4 py-4 bg-white space-y-6 sm:p-6">
                      <div class="flex flex-col gap-y-2">
                        <TextInput id="userName" v-model="username" 
                            :showLabel="false" placeholder="Email" />
                      </div>
                    </div>
                    <div class="pt-3 px-4 py-3 bg-gray-50 text-right sm:px-6">
                      <div class="flex justify-content-center">
                        <FormLoading class="flex-1"/>
                        <PrimaryButton class="ml-3">Send Email</PrimaryButton>
                      </div>
                    </div>
                  </div>
                </form>
              </div>
            </div>
            <!-- END Main Form -->
          </div>
        </div>
      </div>
      <!-- END Main Section -->
    </div>
  </BaseBackground>
  <!-- END Page Content -->
</template>

<script setup lang="ts">
// import AppPage from "@/components/AppPage.vue"
import ErrorSummary from "@/components/form/ErrorSummary.vue"
import TextInput from "@/components/form/TextInput.vue"
import FormLoading from "@/components/form/FormLoading.vue"
import PrimaryButton from "@/components/form/PrimaryButton.vue"
import AuthMetaInfoSection from "@/components/AuthMetaInfoSection.vue"

import { ref, watchEffect, nextTick } from "vue"
import { useRouter } from "vue-router"
import { serializeToObject } from "@servicestack/client"
import { useClient } from "@/api"
import { Register } from "@/dtos"
import { auth, revalidate } from "@/auth"
import { getRedirect } from "@/routing"

const client = useClient()
const username = ref("")
const router = useRouter()

let stop = watchEffect(() => {
  if (auth.value) {
    router.push(getRedirect(router) ?? '/')
    nextTick(stop)
  }
})

const onSubmit = async (e: Event) => {
  const {
    userName
  } = serializeToObject(e.currentTarget as HTMLFormElement)
  
  const displayName = ""
  const password = ""
  const confirmPassword = ""
  const autoLogin = false
  const api = await client.api(new Register({ displayName, email: userName, password, confirmPassword, autoLogin }))
  if (api.succeeded) {
    await revalidate()
    await router.push("auth/signin")
  }
}
</script>
