<template>
  <!-- Hero -->
  <BasePageHeading
    title="Lookups Data"
    subtitle="Mobile friendly tables that work across all screen sizes."
  >
    <template #extra>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-alt">
          <li class="breadcrumb-item">
            <a class="link-fx" href="javascript:void(0)">Admins</a>
          </li>
          <li class="breadcrumb-item" aria-current="page">Lookups</li>
        </ol>
      </nav>
    </template>
  </BasePageHeading>
  <!-- END Hero -->

  <div class="content">
    <Create v-if="newBooking" class="max-w-screen-sm" @done="onDone" />
    <Edit v-else-if="editBookingId" :id="editBookingId" class="max-w-screen-sm" @done="onDone" />
    <OutlineButton v-else @click="() => reset({newBooking:true})">
      New Booking
    </OutlineButton>
    <!-- Partial Table -->
    <BaseBlock title="Partial Table">
      <template #options>
        <button type="button" class="btn-block-option">
          <i class="si si-settings"></i>
        </button>
      </template>

      <p class="fs-sm text-muted">
        The second way is to use responsive utility CSS classes for hiding
        columns in various screen resolutions. This way you can hide less
        important columns and keep the most valuable on smaller screens. At the
        following example the <strong>Access</strong> column isn't visible on
        small and extra small screens and <strong>Email</strong> column isn't
        visible on extra small screens.
      </p>
      <table class="table table-bordered table-striped table-vcenter">
        <thead>
          <tr>
            <!-- <th class="text-center" style="width: 100px">
              <i class="far fa-user"></i>
            </th> -->
            <th>Lookup Type</th>
            <th class="d-none d-md-table-cell" style="width: 30%">Lookup Value</th>
            <th class="d-none d-sm-table-cell" style="width: 15%">Lookup Text</th>
            <th class="text-center" style="width: 100px">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="currData in bookings" :key="currData.id">
            <!-- <td class="text-center">
              <img
                class="img-avatar img-avatar48"
                :src="`/assets/media/avatars/${user.avatar}.jpg`"
                alt="Avatar"
              />
            </td> -->
            <td class="fw-semibold fs-sm">
              {{ currData.name }}
            </td>
            <td class="d-none d-md-table-cell fs-sm">
              {{ currData.roomType }}
            </td>
            <td class="d-none d-sm-table-cell">
              {{ currData.roomNumber }}
            </td>
            <td class="text-center">
              <div class="btn-group">
                <button type="button" class="btn btn-sm btn-alt-secondary" @click="() => reset({newBooking:true})">
                  <i class="fa fa-fw fa-pencil-alt"></i>
                </button>
                <button type="button" class="btn btn-sm btn-alt-secondary">
                  <i class="fa fa-fw fa-times"></i>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </BaseBlock>
    <div v-if="bookings.length > 0" class="mt-4 flex flex-col">
      <div class="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div class="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
          <div class="shadow overflow-hidden border-b border-gray-200 sm:rounded-lg">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
              <tr class="select-none">
                <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  <FluentAddCircle24Regular class="w-6 h-6 cursor-pointer" title="New Booking" @click="() => reset({newBooking:true})" />
                </th>
                <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Id
                </th>
                <th class="hidden sm:table-cell px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Name
                </th>
                <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  <span class="hidden sm:inline">Room </span>Type
                </th>
                <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  <span class="hidden sm:inline">Room </span>No
                </th>
                <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Cost
                </th>
                <th class="hidden sm:table-cell px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Start Date
                </th>
                <th class="hidden sm:table-cell px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Created By
                </th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="(booking, index) in bookings" :key="booking.id" :class="index % 2 === 0 ? 'bg-white' : 'bg-gray-50'">
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  <MdiSquareEditOutline class="w-6 h-6 cursor-pointer" title="Edit Booking" @click="() => reset({editBookingId:booking.id})" />
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ booking.id }}
                </td>
                <td class="hidden sm:table-cell px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ booking.name }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ booking.roomType }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ booking.roomNumber }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ formatCurrency(booking.cost) }}
                </td>
                <td class="hidden sm:table-cell px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ formatDate(booking.bookingStartDate) }}
                </td>
                <td class="hidden sm:table-cell px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ booking.createdBy }}
                </td>
              </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Create from "@/pages/bookings-crud/Create.vue"
import Edit from "@/pages/bookings-crud/Edit.vue"
import OutlineButton from "@/components/form/OutlineButton.vue"
// import AppPage from "@/components/AppPage.vue"
// import SrcLink from "@/components/SrcLink.vue"

import { ref } from "vue"
import { formatDate, formatCurrency } from "@/utils"
import { Booking, QueryBookings } from "@/dtos"
import { client } from "@/api"

const newBooking = ref<boolean>(false)
const editBookingId = ref<number|undefined>()

// const expandAbout = ref<boolean>(false)

const bookings = ref<Booking[]>([])

const refreshBookings = async () => {
  const api = await client.api(new QueryBookings())
  if (api.succeeded) {
    bookings.value = api.response!.results ?? []
  }
}

onMounted(async () => await refreshBookings())

const reset = (args:{ newBooking?: boolean, editBookingId?:number } = {}) => {
  newBooking.value = args.newBooking ?? false
  editBookingId.value = args.editBookingId ?? undefined
}

const onDone = () => {
  reset()
  refreshBookings()
}

// const toggleAbout = () => expandAbout.value = !expandAbout.value

</script>
