<template>
  <fieldwrapper>
    <kComboBox
        :style="{ width: '230px' }"
        :data-items="provinceList"
        :value="selectedProvince?.value"
        :text-field="'name'"
        :filterable="true"
        @change="onChangeProvince"
        @filterchange="onFilterProvice"
    ></kComboBox>
    <kComboBox
        :style="{ width: '230px' }"
        :disabled="!selectedProvince"
        :data-items="cityList"
        :value="selectedCity ? undefined : selectedCity?.value"
        :text-field="'name'"
        :filterable="true"
        @change="onChangeCity"
        @filterchange="onFilterCity"
    ></kComboBox>
    <kComboBox
        :style="{ width: '230px' }"
        :disabled="!selectedCity"
        :data-items="districtList"
        :value="selectedDistrict ? undefined : selectedDistrict?.value"
        :value-field="'id'"
        :text-field="'name'"
        :filterable="true"
        @change="onChangeDistrict"
        @filterchange="onFilterDistrict"
    ></kComboBox>
    <kComboBox
        :style="{ width: '230px' }"
        :disabled="!selectedDistrict"
        :data-items="villageList"
        :value="selectedVillage?.value"
        :text-field="'name'"
        :filterable="true"
        @change="onChangeVillage"
        @filterchange="onFilterVillage"
    ></kComboBox>
    <span>
      {{ selectedVillage?.postal }}
    </span>
  </fieldwrapper>
</template>

<script setup lang="ts">
import { Province, City, District, Village, QueryProvinces, QueryCities, QueryDistricts, QueryVillages } from "@/dtos"
import { client } from "@/api"

import { process, filterBy } from '@progress/kendo-data-query'
import { FieldWrapper as fieldwrapper } from "@progress/kendo-vue-form";
// import { Error, Hint, Label } from "@progress/kendo-vue-labels";
import { ComboBox as kComboBox} from '@progress/kendo-vue-dropdowns'

// const props = defineProps<{
//   selectedProvince: any,
//   selectedCity: any,
//   selectedDistrict: any,
//   selectedVillage: any,
// }>()

const sourceProvinceList = ref<Province[]>([])
let provinceList = ref<Province[]>([])
let selectedProvince = ref<any | null>(null)

let sourceCityList = ref<City[]>([])
let cityList = ref<City[]>([])
let selectedCity = ref<any | null>(null)

let sourceDistrictList = ref<District[]>([])
let districtList = ref<District[]>([])
let selectedDistrict = ref<any | undefined>(undefined)

let sourceVillageList = ref<Village[]>([])
let villageList = ref<Village[]>([])
let selectedVillage = ref<any | null>(null)

const getProvinceList = async(selectedId?: any) => {
  const api = await client.api(new QueryProvinces({ countryId: selectedId }))
  if (api.succeeded) {
    sourceProvinceList.value = api.response!.results ?? []
    provinceList.value = process(sourceProvinceList.value, {}).data as any[]
  }
}

const getCityList = async() => {
  const api = await client.api(new QueryCities({ provinceId: selectedProvince.value}))
  if (api.succeeded) {
    sourceCityList.value = api.response!.results ?? []
    cityList.value = sourceCityList.value
  }
}

const getDistrictList = async() => {
  const api = await client.api(new QueryDistricts({ cityId: selectedCity.value}))
  if (api.succeeded) {
    sourceDistrictList.value = api.response!.results ?? []
    districtList.value = sourceDistrictList.value
  }
}

const getVillageList = async() => {
  const api = await client.api(new QueryVillages({ districtId: selectedDistrict.value}))
  if (api.succeeded) {
    sourceVillageList.value = api.response!.results ?? []
    villageList.value = sourceVillageList.value
  }
}

onMounted(async () => {
  
  await getProvinceList(62)
  // selectedProvince.value = props.selectedProvince
  // await getCityList(selectedCity.value)
  // selectedCity.value = props.selectedCity
  // selectedDistrict.value = props.selectedDistrict
  // await getCityList(selectedCity.value)
  // se.value = props.selectedCity
  // await getCityList(selectedCity.value)

});

const onChangeProvince = (e: any) => {
    selectedProvince.value = e.target.value?.id
    getCityList()
    selectedCity.value.id = null
    selectedDistrict.value = undefined
    selectedVillage.value = null
}

const onChangeCity = (e: any) => {
    selectedCity.value = e.value?.id
    // console.log(e)
    getDistrictList()
    selectedDistrict.value = undefined
    selectedVillage.value = null
}

const onChangeDistrict = (e: any) => {
    selectedDistrict.value = e.value?.id
    getVillageList()
    selectedVillage.value = null
}

const onChangeVillage = (e: any) => {
    selectedVillage.value = e.target.value
}

const onFilterProvice = (e : any) => {
  const data = process(sourceProvinceList.value, {}).data as any[]
  provinceList.value = filterBy(data, e.filter)
}

const onFilterCity = (e : any) => {
  const data = process(sourceCityList.value, {}).data as any[]
  cityList.value = filterBy(data, e.filter)
}

const onFilterDistrict = (e : any) => {
  const data = process(sourceDistrictList.value, {}).data as any[]
  districtList.value = filterBy(data, e.filter)
}

const onFilterVillage = (e : any) => {
  const data = process(sourceVillageList.value, {}).data as any[]
  villageList.value = filterBy(data, e.filter)
}

</script>