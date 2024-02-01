<script setup lang="ts">
import { auth, appUser, getAppUser } from "@/auth"
import { client } from "@/api"
import { UpdateAppUser, UploadUserProfile } from "@/dtos"
import { showNotifSuccess, showNotifError } from '@/stores/commons'
import { Form as kForm } from "@progress/kendo-vue-form";
// import { Upload as kUpload } from '@progress/kendo-vue-upload';
import { Button as KButton} from '@progress/kendo-vue-buttons'
import ProfileEditForm from "./ProfileEditForm.vue";
import ChangePassword from "./ChangePassword.vue";
import UploadFile from "@/components/form/UploadFile.vue";

let profileImage = ref<File |undefined> ()

const currProfileImageUrl = computed(() => {
  return ((import.meta.env.DEV) ? "https://localhost:5005/" : "") + appUser.value.profileUrl
})

onMounted(async () => {
  await getAppUser(auth.value?.userName)
})

// const onBeforeUpload = (event: any) => {
//   const profileUrl = client.baseUrl + "/assets/media/users/" + appUser.value.id + "/profile/" + event.files[0].name
//   event.additionalData.email = appUser.value.email;
//   event.additionalData.profileUrl = profileUrl;
// }

// const onStatusChange = (event: any) => {
//   if (event.response) {
//     getAppUser(auth.value?.userName)
//   }
// }

// const newPassword = ref<string>('')
// const newPasswordConfirm = ref<string>('')
// const oldPassword = ref<string>('')

// const onChangePassword = () => {

// }

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
  <BaseBackground image="/assets/media/photos/photo10@2x.jpg">
    <div class="bg-primary-dark-op">
      <div class="content content-full text-center">
        <div class="my-3">
          <img class="img-avatar img-avatar-thumb" :src="currProfileImageUrl" alt=""/>
        </div>
        <h1 class="h2 text-white mb-0">Edit Account</h1>
        <h2 class="h4 fw-normal text-white-75">{{ appUser.fullName }}</h2>
        <RouterLink :to="{ name: 'user-profile' }" class="btn btn-alt-secondary">
          <i class="fa fa-fw fa-arrow-left text-danger"></i> Back to Profile
        </RouterLink>
      </div>
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
              <button id="btabs-static-tab-eventsharing" data-bs-target="#btabs-static-eventsharing"
                  class="nav-link" data-bs-toggle="tab" role="tab"
                  aria-controls="btabs-static-eventsharing" aria-selected="false" type="button">
                Event Sharing
              </button>
            </li>
          </ul>
          <div class="block-content tab-content">
            <div id="btabs-static-profile" aria-labelledby="btabs-static-tab-profile" tabindex="0" class="tab-pane active" role="tabpanel" >
              <kForm :initialValues="appUser" @submit="onUpdateUserProfile">
                <ProfileEditForm />
              </kForm>
              <BaseBlock title="Update User Profile" btn-option-fullscreen btn-option-content>
                <template #options>
                    <KButton id="btnSearchData" @click="onUploadProfileImg" :theme-color="'primary'" style="width:100px">Upload</KButton>
                </template>
                <UploadFile v-model="profileImage" />
              </BaseBlock>
            </div>
            <div id="btabs-static-security" aria-labelledby="btabs-static-tab-security" tabindex="1" class="tab-pane" role="tabpanel">
              <ChangePassword />
              <!-- <BaseBlock title="Password" btn-option-fullscreen btn-option-content>
                <template #options>
                    <KButton id="btnChangePassword" @click="onChangePassword" :theme-color="'primary'" style="width:100px">Change Password</KButton>
                </template>
              </BaseBlock> -->
            </div>
            <div id="btabs-static-eventsharing" aria-labelledby="btabs-static-tab-eventsharing" tabindex="2" class="tab-pane" role="tabpanel">
              <h1>Event Sharing</h1>
            </div>
          </div>
      </template>
    </BaseBlock>
    <!-- Update User Info -->
    <!-- <kForm :initialValues="appUser" @submit="onUpdateUserProfile">
      <ProfileEditForm />
    </kForm> -->
    <!-- END Update User Info -->
    <!-- <BaseBlock title="Update User Profile" btn-option-fullscreen btn-option-content>
      <template #options>
          <KButton id="btnSearchData" @click="onUploadProfileImg" :theme-color="'primary'" style="width:100px">Upload</KButton>
      </template>
      <UploadFile v-model="profileImage" />
      <kButton id="uploadProfileImg" @click="onUploadProfileImg" :theme-color="'secondary'" > Upload</kButton>
    </BaseBlock> -->
    <!-- Update User Profile -->
    <!-- <BaseBlock title="Update User Profile" btn-option-fullscreen btn-option-content>
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
    </BaseBlock> -->
    <!-- END Update User Profile -->
  </div>
  <!-- END Page Content -->
</template>
