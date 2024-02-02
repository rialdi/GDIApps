<script setup lang="ts">
import { client } from "@/api"
import { auth, appUser, getAppUser } from "@/auth"
import { UpdateAppUser, UploadUserProfile } from "@/dtos"
import { showNotifSuccess, showNotifError } from '@/stores/commons'

// import SecondaryButton from "@/components/form/SecondaryButton.vue"

import { Form as KForm } from "@progress/kendo-vue-form";

import ProfileEditForm from "./ProfileEditForm.vue";
import ChangePassword from "./ChangePassword.vue";
import UploadFile from "@/components/form/UploadFile.vue";

import { Button as KButton} from '@progress/kendo-vue-buttons'

const roles = auth.value?.roles ?? []

const isAdmin = computed(() => {
  return roles.includes("Admin")
})

const currProfileImageUrl = computed(() => {
  return ((import.meta.env.DEV) ? "https://localhost:5005/" : "") + appUser.value.profileUrl
})

let profileImage = ref<File |undefined> ()


const roleInfoClass = ref<string>('mb-0 fs-sm fw-medium items-center px-3 py-0.5 rounded-full text-xs font-medium leading-5 bg-indigo-100 text-indigo-800')

onMounted(() => {
  getAppUser(auth.value?.userName)
  console.log(auth.value)
})

const onUpdateUserProfile = async (dataItem: any) => {
  const request = new UpdateAppUser({
    email: dataItem.email,
    fullName: dataItem.fullName,
    phoneNumber: dataItem.phoneNumber,
  })
  const api = await client.api(request)
  if (api.succeeded) {
    showNotifSuccess('Update Profile', 'Successfully updated Profile data 🎉')
    await getAppUser(auth.value?.userName)
  } 
}

const onUploadProfileImg = async () => {
  const formData = new FormData()
  formData.set("id", appUser.value.id?.toString() ?? '')
  formData.set("email", appUser.value.email ?? '')
  formData.set("ProfileUrl", profileImage.value as Blob)

  let api = await client.apiForm(new UploadUserProfile(), formData)
  if(api.succeeded)
  {
    showNotifSuccess('Update Profile Image', 'Successfully updated Profile Image 🎉')
    getAppUser(auth.value?.userName)
  }
  else
  {
    showNotifError("Update Profile Image", api.errorMessage)
  }
}
</script>

<template>
  <!-- Hero -->
  <BaseBackground image="/assets/media/photos/photo12@2x.jpg" inner-class="bg-black-50">
    <div class="content content-full text-center">
      <div class="my-3">
        <img class="img-avatar img-avatar-thumb" alt="Avatar" :src="currProfileImageUrl"/>
      </div>
      <h1 class="h2 text-white mb-0">{{appUser?.phoneNumber}}</h1>
      <span v-if="!isAdmin" v-for="role in roles" :key="role" :class="roleInfoClass">
        {{ role }}
      </span>
      <span v-if="isAdmin" :class="roleInfoClass">
        Admin
      </span>
      <!-- <div class="text-end">
        <SecondaryButton class="m-0" href="profile/edit" icon-class="fa fa-pen-to-square text-primary p-0">
          Edit
        </SecondaryButton>
      </div> -->
    </div>
  </BaseBackground>
  <!-- END Hero -->

  
  <!-- Page Content -->
  <div class="content content-boxed mt-3">
    <BaseBlock>
      <template #content>
          <ul class="nav nav-tabs nav-tabs-block" role="tablist">
            <li class="nav-item">
              <button id="btabs-static-tab-profile" data-bs-target="#btabs-static-profile"
                  class="nav-link active" data-bs-toggle="tab" role="tab"
                  aria-controls="btabs-static-profile" aria-selected="true" type="button">
                My Profile
              </button>
            </li>
            <li class="nav-item">
              <button id="btabs-static-tab-security" data-bs-target="#btabs-static-security"
                  class="nav-link" data-bs-toggle="tab" role="tab"
                  aria-controls="btabs-static-security" aria-selected="false" type="button">
                Security
              </button>
            </li>
            <li class="nav-item">
              <button id="btabs-static-tab-family" data-bs-target="#btabs-static-family"
                  class="nav-link" data-bs-toggle="tab" role="tab"
                  aria-controls="btabs-static-family" aria-selected="false" type="button">
                Family Member
              </button>
            </li>
            <li class="nav-item">
              <button id="btabs-static-tab-resume" data-bs-target="#btabs-static-resume"
                  class="nav-link" data-bs-toggle="tab" role="tab"
                  aria-controls="btabs-static-resume" aria-selected="false" type="button">
                Resume
              </button>
            </li>
          </ul>
          <div class="block-content tab-content">
            <div id="btabs-static-profile" aria-labelledby="btabs-static-tab-profile" tabindex="0" class="tab-pane active" role="tabpanel" >
              <KForm :initialValues="appUser" @submit="onUpdateUserProfile">
                <ProfileEditForm />
              </KForm>
              <BaseBlock title="Profile Image">
                <template #options>
                    <KButton id="btnUploadProfileImg" @click="onUploadProfileImg" :theme-color="'primary'" >Upload</KButton>
                </template>
                <UploadFile v-model="profileImage" />
              </BaseBlock>
            </div>
            <div id="btabs-static-security" aria-labelledby="btabs-static-tab-security" tabindex="1" class="tab-pane" role="tabpanel">
              <ChangePassword />
            </div>
            <div id="btabs-static-family" aria-labelledby="btabs-static-tab-family" tabindex="2" class="tab-pane" role="tabpanel">
              <h1>Family</h1>
            </div>
            <div id="btabs-static-resume" aria-labelledby="btabs-static-tab-resume" tabindex="3" class="tab-pane" role="tabpanel">
              <h1>Resume</h1>
            </div>
          </div>
      </template>
    </BaseBlock>
  </div>
  <!-- END Page Content -->

  
</template>
