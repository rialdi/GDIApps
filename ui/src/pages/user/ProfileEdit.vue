<script setup lang="ts">

import { auth, appUser, getAppUser } from "@/auth"

import { client } from "@/api"
import {UpdateAppUser, UploadUserProfile} from "@/dtos"

import { showNotifSuccess } from '@/stores/commons'

import { Form as kForm } from "@progress/kendo-vue-form";
import { Upload as kUpload } from '@progress/kendo-vue-upload';
import ProfileEditForm from "./ProfileEditForm.vue";

import { Button as kButton} from '@progress/kendo-vue-buttons'

import UploadFile from "@/components/form/UploadFile.vue";

let profileImage = ref<File |undefined> ()

onMounted(async () => await getAppUser(auth.value?.userName))

const onBeforeUpload = (event: any) => {
  const profileUrl = client.baseUrl + "/assets/media/users/" + appUser.value.id + "/profile/" + event.files[0].name
  event.additionalData.email = appUser.value.email;
  event.additionalData.profileUrl = profileUrl;
}

const onStatusChange = (event: any) => {
  if (event.response) {
    getAppUser(auth.value?.userName)
  }
}

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
  formData.set("email", "rialdi@ptgdi.com")
  formData.set("ProfileUrl", profileImage.value as Blob)

  let api = await client.apiForm(new UploadUserProfile(), formData)
  if(api.succeeded)
  {
    console.log("Success")
    getAppUser(auth.value?.userName)
  }
  else
  {
    console.log(api)
  }
  // formData.append("email", "rialdi@ptgdi.com")
  // formData.append("ProfileUrl", profileImage.value)
}
</script>

<template>
  <!-- Hero -->
  <BaseBackground image="/assets/media/photos/photo10@2x.jpg">
    <div class="bg-primary-dark-op">
      <div class="content content-full text-center">
        <div class="my-3">
          <img
            class="img-avatar img-avatar-thumb"
            :src="appUser.profileUrl"
            alt=""
          />
        </div>
        <h1 class="h2 text-white mb-0">Edit Account</h1>
        <h2 class="h4 fw-normal text-white-75">{{ appUser.fullName }}</h2>
        <RouterLink
          :to="{ name: 'user-profile' }"
          class="btn btn-alt-secondary"
        >
          <i class="fa fa-fw fa-arrow-left text-danger"></i> Back to Profile
        </RouterLink>
      </div>
    </div>
  </BaseBackground>
  <!-- END Hero -->

  <!-- Page Content -->
  <div class="content content-boxed">
    <!-- Update User Info -->
    <kForm :initialValues="appUser" @submit="onUpdateUserProfile">
      <ProfileEditForm />
    </kForm>
    <!-- END Update User Info -->
    <BaseBlock title="Update User Profile" btn-option-fullscreen btn-option-content>
      <UploadFile v-model="profileImage" />
      <kButton id="uploadProfileImg" @click="onUploadProfileImg" :theme-color="'secondary'" > Upload</kButton>
    </BaseBlock>
    <!-- Update User Profile -->
    <BaseBlock title="Update User Profile" btn-option-fullscreen btn-option-content>
      <kUpload class="mb-3"
            :default-files="[]"
            :list="'myTemplate'"
            :batch="false"
            :multiple="false"
            :with-credentials="false"
            @beforeupload="onBeforeUpload"
            @statuschange="onStatusChange" 
            :save-url="'/api/UploadUserProfile'"
        >
        <template v-slot:myTemplate="{props}">
        <ul>
            <li v-for="file in props.files" :key="file.name">
              <img class="h-16 w-16 object-cover rounded-full" 
                    :src="'/assets/media/users/' + appUser.id + '/profile/' + file.name " 
                    alt="Current Profile Url"/>
            </li>
        </ul>
        </template>
      </kUpload>
    </BaseBlock>
    <!-- END Update User Profile -->
  </div>
  <!-- END Page Content -->
</template>
