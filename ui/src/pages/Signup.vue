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
              <h1 class="fw-bold mb-2">Create Account</h1>
              <p class="fw-medium text-muted">
                Already have an account, please
                <RouterLink :to="{ name: 'auth-signin' }">sign in</RouterLink>.
              </p>
            </div>
            <!-- END Header -->

            <!-- Sign In Form -->
            <div class="row g-0 justify-content-center">
              <div class="col-sm-8 col-xl-6">
                <form @submit.prevent="onSubmit">
                  <div class="shadow overflow-hidden sm:rounded-md">
                    <ErrorSummary except="displayName,userName,password,confirmPassword,autoLogin"/>
                    <div class="px-4 py-4 bg-white space-y-6 sm:p-6">
                      <div class="flex flex-col gap-y-2">
                        <TextInput id="displayName" v-model="displayName" 
                            :showLabel="true" placeholder="Your first and last name" />
                        <TextInput id="userName" v-model="username" 
                            :showLabel="true" placeholder="Email" />
                        <TextInput id="password" v-model="password" type="password" 
                            :showLabel="true" placeholder="6 characters or more" />
                        <TextInput id="confirmPassword" v-model="confirmPassword" type="password"
                          :showLabel="true" />
                        <CheckBox id="autoLogin" />
                      </div>
                    </div>
                    <div class="pt-3 px-4 py-3 bg-gray-50 text-right sm:px-6">
                      <div class="flex justify-end">
                        <FormLoading class="flex-1"/>
                        <PrimaryButton class="ml-3">Sign Up</PrimaryButton>
                      </div>
                    </div>
                  </div>
                </form>
              </div>
            </div>
            <!-- END Sign In Form -->
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
import CheckBox from "@/components/form/Checkbox.vue"
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
const displayName = ref("")
const username = ref("")
const password = ref("")
const confirmPassword = ref("")
const router = useRouter()

let stop = watchEffect(() => {
  if (auth.value) {
    router.push(getRedirect(router) ?? '/')
    nextTick(stop)
  }
})

const onSubmit = async (e: Event) => {
  const {
    displayName,
    userName,
    password,
    confirmPassword,
    autoLogin
  } = serializeToObject(e.currentTarget as HTMLFormElement)
  if (password !== confirmPassword) {
    client.setError({ fieldName:'confirmPassword', message:'Passwords do not match' })
    return
  }

  const api = await client.api(new Register({ displayName, email: userName, password, confirmPassword, autoLogin }))
  if (api.succeeded) {
    await revalidate()
    await router.push("auth/signin")
  }
}
</script>
