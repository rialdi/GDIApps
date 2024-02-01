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
            <div class="text-center mb-3">
              <p class="mb-3">
                <i class="fa fa-2x fa-circle-notch text-primary-light"></i>
              </p>
              <h1 class="fw-bold mb-2">Sign In</h1>
              <p class="fw-medium text-muted">
                Welcome, please login or
                <RouterLink :to="{ name: 'auth-signup' }">sign up</RouterLink>
                for a new account.
              </p>
              <p class="fw-medium text-muted">
                --- sign in with ---
              </p>
              <div class="flex justify-center">
                <div class="btn-group" role="group" aria-label="Horizontal Secondary">
                        <!-- <a class="btn btn-outline-primary" href="/auth/facebook?continue=/backend/dashboard" v-click-ripple>
                          <i class="fab fa-facebook-f"></i>
                        </a> -->
                        <a class="btn btn-outline-primary" href="/auth/google?continue=/backend/dashboard" v-click-ripple>
                          <i class="fab fa-google"></i>
                        </a>
                        <!-- <a class="btn btn-outline-primary" href="/auth/microsoft?continue=/backend/dashboard" v-click-ripple>
                          <i class="fab fa-microsoft"></i>
                        </a> -->
                      </div>
                    </div>
            </div>
            <!-- END Header -->

            <!-- Sign In Form -->
            <div class="row g-0 justify-content-center">
              <div class="col-sm-8 col-xl-6">
                <form @submit.prevent="onSubmit">
                  <div class="shadow overflow-hidden sm:rounded-md">
                    <ErrorSummary except="userName,password,rememberMe"/>
                    <div class="px-4 py-4 bg-white space-y-6 sm:p-6">
                      <div class="flex flex-col gap-y-2">
                        <TextInput id="userName"  v-model="username"  
                            :showLabel="true" 
                            label="User Name or Email" 
                            placeholder="User Name or Email"/>
                        <TextInput id="password" v-model="password"
                            :showLabel="true" 
                            type="password" />
                        <CheckBox id="rememberMe" />
                      </div>
                    </div>
                    <div class="pt-3 px-4 py-3 bg-gray-50 text-right sm:px-6">
                      <div class="flex justify-end">
                        <FormLoading class="flex-1"/>
                        <SecondaryButton href="forgotpassword">Forgot Password</SecondaryButton>
                        <PrimaryButton class="ml-3">Sign In</PrimaryButton>
                      </div>
                    </div>
                    
                  </div>
                </form>
              </div>
              
            </div>
            <!-- END Sign In Form -->

            
        <!-- <div class="row justify-content-end mt-5" style="max-width:300px">
            <nav-button-group class="nav-button-group" 
                :items="store.nav.navItemsMap.auth" :attributes="store.userAttributes" :baseHref="store.nav.baseUrl" block lg />
        </div> -->
          </div>
        </div>
        <div class="px-4 py-3 w-100 d-lg-none d-flex flex-column flex-sm-row justify-content-between fs-sm text-center text-sm-start">
          <p class="fw-medium text-black-50 py-2 mb-0">
            <strong>{{ store.app.name + " " + store.app.version }}</strong>
            &copy; {{ store.app.copyright }}
          </p>
          <ul class="list list-inline py-2 mb-0">
            <li class="list-inline-item">
              <a class="text-muted fw-medium" href="javascript:void(0)"
                >Legal</a
              >
            </li>
            <li class="list-inline-item">
              <a class="text-muted fw-medium" href="javascript:void(0)"
                >Contact</a
              >
            </li>
            <li class="list-inline-item">
              <a class="text-muted fw-medium" href="javascript:void(0)"
                >Terms</a
              >
            </li>
          </ul>
        </div>
      </div>
      <!-- END Main Section -->
    </div>
  </BaseBackground>
  <!-- END Page Content -->

  <!-- <BaseBlock title="Sign In" class="max-w-xl"> 
    <div class="flex mt-8">
      <h3 class="hidden xs:block mr-4 leading-8 text-gray-500">Quick Links</h3>
      <span class="relative z-0 inline-flex shadow-sm rounded-md">
        <button type="button" @click="setUser('admin@email.com')"
                class="relative inline-flex items-center px-4 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:z-10 focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500">
            admin@email.com
        </button>
        <button type="button" @click="setUser('manager@email.com')"
                class="-ml-px relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:z-10 focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500">
            manager@email.com
        </button>
        <button type="button" @click="setUser('employee@email.com')"
                class="-ml-px relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:z-10 focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500">
            employee@email.com
        </button>
        <button type="button" @click="setUser('new@user.com')"
                class="-ml-px relative inline-flex items-center px-4 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:z-10 focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500">
            new@user.com
        </button>
      </span>
    </div>
  </BaseBlock> -->
</template>

<script setup lang="ts">
// import AppPage from "@/components/AppPage.vue"
import ErrorSummary from "@/components/form/ErrorSummary.vue"
import TextInput from "@/components/form/TextInput.vue"
import CheckBox from "@/components/form/Checkbox.vue"
import FormLoading from "@/components/form/FormLoading.vue"
import PrimaryButton from "@/components/form/PrimaryButton.vue"
// import SecondaryButton from "@/components/form/SecondaryButton.vue"
import AuthMetaInfoSection from "@/components/AuthMetaInfoSection.vue"

import { ref, watchEffect, nextTick } from "vue"
import { useRouter } from "vue-router"
import { serializeToObject } from "@servicestack/client"
import { useClient } from "@/api"
import { Authenticate } from "@/dtos"
import { auth, revalidate } from "@/auth"
import { getRedirect } from "@/routing"

import { useTemplateStore } from "@/stores/template";
const store = useTemplateStore();

const client = useClient() 
// const username = ref('')
// const password = ref('')
const username = ref('rialdi@ptgdi.com')
const password = ref('Formula01')
const router = useRouter()

let stop = watchEffect(() => {
  if (auth.value) {
    router.push(getRedirect(router) ?? '/')
    nextTick(() => stop())
  }
})

onMounted(() => {
  if(import.meta.env.DEV) {
    username.value = 'rialdi@ptgdi.com'
    password.value = 'Formula01'
  }
})

// const setUser = (email: string) => {
//   username.value = email
//   password.value = "p@55wOrd"
// }

const onSubmit = async (e: Event) => {
  const { userName, password, rememberMe } = serializeToObject(e.currentTarget as HTMLFormElement)
  const api = await client.api(new Authenticate({ provider: 'credentials', userName, password, rememberMe }))
  if (api.succeeded)
  {
    await revalidate()
    router.push({ name: "backend-dashboard" });
  }
}
</script>
