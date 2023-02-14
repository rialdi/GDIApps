<script setup lang="ts">

import { auth, appUser, getAppUser } from "@/auth"

import { client } from "@/api"
import {UpdateAppUser} from "@/dtos"

import { showNotifSuccess } from '@/stores/commons'

import { Form as kForm } from "@progress/kendo-vue-form";
import { Upload as kUpload } from '@progress/kendo-vue-upload';
import ProfileEditForm from "./ProfileEditForm.vue";
 
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

//     const content = new MultiPartForm
//   // const filePath = ""
//   // const fileInfo = new fileInfo()
// //   const profileImg = await ProfileImageUrl.GetStreamFromUrlAsync();
// // const createContact = new MultipartFormDataContent()
// //     .AddParams(new CreateContact
// //     {
// //         FirstName = "Cody",
// //         LastName = "Fisher",
// //         Email = "cody.fisher@gmail.com",
// //         JobType = "Security",
// //         PreferredLocation = "Remote",
// //         PreferredWorkType = EmploymentType.FullTime,
// //         AvailabilityWeeks = 1,
// //         SalaryExpectation = 100_000,
// //         About = "Lead Security Associate",
// //     })
// //     .AddFile(nameof(CreateContact.ProfileUrl), "cody-fisher.png", profileImg);


//   console.log(dataItem)
  // const uploadedImage = (dataItem.profileUrl);
  // console.log(uploadedImage);
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

//     using var content = new MultipartFormDataContent()
//     .AddParam(nameof(MultipartRequest.Id), 1)
//     .AddParam(nameof(MultipartRequest.String), "foo")
//     .AddParam(nameof(MultipartRequest.Contact), 
//         new Contact { Id = 1, FirstName = "First", LastName = "Last" })
//     .AddJsonParam(nameof(MultipartRequest.PhoneScreen), 
//         new PhoneScreen { Id = 3, JobApplicationId = 1, Notes = "The Notes"})
//     .AddCsvParam(nameof(MultipartRequest.Contacts), new[] {
//         new Contact { Id = 2, FirstName = "First2", LastName = "Last2" },
//         new Contact { Id = 3, FirstName = "First3", LastName = "Last3" },
//     })
//     .AddFile(nameof(MultipartRequest.ProfileUrl), "profile.txt", file1Stream)
//     .AddFile(nameof(MultipartRequest.UploadedFiles), "uploadedFiles1.txt", file2Stream)
//     .AddFile(nameof(MultipartRequest.UploadedFiles), "uploadedFiles2.txt", file3Stream));

// var api = await client.ApiFormAsync<MultipartRequest>(typeof(MultipartRequest).ToApiUrl(), content);
// if (!api.Succeeded) api.Error.PrintDump();
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
