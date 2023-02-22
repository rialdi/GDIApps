import { acceptHMRUpdate, defineStore } from "pinia"
import { client } from "@/api"
import { Country, Province, City, District, Village, QueryCountries, QueryProvinces, QueryCities, QueryDistricts, QueryVillages } from "@/dtos"
import { process, filterBy } from '@progress/kendo-data-query'

interface SelectedRegion {
    Country: string | null | undefined,
    Province: string | null | undefined,
    City: string | null | undefined,
    District: string | null | undefined,
    Village: string | null | undefined,
}
export const useIndonesiaRegionStore = defineStore('indonesiaregions', () => {

    let currSelectedRegion = ref<SelectedRegion | undefined>()

    let availablePostalCodes = ref<string | undefined>()

    // State
    const sourceCountryList = ref<Country[]>([])
    let countryList = ref<Country[]>([])
    let selectedCountry = ref<Country | undefined>()

    const sourceProvinceList = ref<Province[]>([])
    let provinceList = ref<Province[]>([])
    let selectedProvince = ref<Province | undefined>()

    let sourceCityList = ref<City[]>([])
    let cityList = ref<City[]>([])
    let selectedCity = ref<City | undefined>()

    let sourceDistrictList = ref<District[]>([])
    let districtList = ref<District[]>([])
    let selectedDistrict = ref<District | undefined>()

    let sourceVillageList = ref<Village[]>([])
    let villageList = ref<Village[]>([])
    let selectedVillage = ref<Village | undefined>()

    // const emit = defineEmits<{
    //     (e:'updatePostalCode', value: any): () => void
    // }>()

    // Actions
    const refreshIndonesiaRegions = async (slectedRegion? : any) => {
        selectedCountry.value = undefined
        selectedProvince.value = undefined
        selectedCity.value = undefined
        selectedDistrict.value = undefined
        selectedVillage.value = undefined
        currSelectedRegion.value = slectedRegion
        getCountryList()
    }
    const getCountryList = async() => {
        const api = await client.api(new QueryCountries({ }))
        if (api.succeeded) {
            sourceCountryList.value = api.response!.results ?? []
            countryList.value = process(sourceCountryList.value, {}).data as any[]
            
            if(currSelectedRegion.value?.Country) {
                selectedCountry.value = countryList.value.find((p) => p.name === currSelectedRegion.value?.Country)
                getProvinceList()
            }
        }
    }

    const getProvinceList = async() => {
        const api = await client.api(new QueryProvinces({ countryId: selectedCountry.value?.id }))
        if (api.succeeded) {
            sourceProvinceList.value = api.response!.results ?? []
            provinceList.value = process(sourceProvinceList.value, {}).data as any[]

            if(currSelectedRegion.value?.Province) {
                selectedProvince.value = provinceList.value.find((p) => p.name === currSelectedRegion.value?.Province)
                getCityList()
            }
        }
    }

    const getCityList = async() => {
        const api = await client.api(new QueryCities({ provinceId: selectedProvince.value?.id}))
        if (api.succeeded) {
            sourceCityList.value = api.response!.results ?? []
            cityList.value = sourceCityList.value

            if(currSelectedRegion.value?.City) {
                selectedCity.value = cityList.value.find((p) => p.name === currSelectedRegion.value?.City)
                getDistrictList()
            }
        }
    }

    const getDistrictList = async() => {
        const api = await client.api(new QueryDistricts({ cityId: selectedCity.value?.id}))
        if (api.succeeded) {
            sourceDistrictList.value = api.response!.results ?? []
            districtList.value = sourceDistrictList.value

            if(currSelectedRegion.value?.District) {
                selectedDistrict.value = districtList.value.find((p) => p.name === currSelectedRegion.value?.District)
                // console.log(selectedCountry)
                getVillageList()
            }
        }
    }

    const getVillageList = async() => {
        const api = await client.api(new QueryVillages({ districtId: selectedDistrict.value?.id}))
        if (api.succeeded) {
            sourceVillageList.value = api.response!.results ?? []
            villageList.value = sourceVillageList.value
            if(currSelectedRegion.value?.Village) {
                selectedVillage.value = villageList.value.find((p) => p.name === currSelectedRegion.value?.Village)
            }
        }
    }

    const onChangeCountry = (e: any) => {
        // console.log("Change Country")
        selectedProvince.value = undefined
        selectedCity.value = undefined
        selectedDistrict.value = undefined
        selectedVillage.value = undefined
        selectedCountry.value = e.component.dataItems[e.component.index]
        getProvinceList()
    }
  
    const onChangeProvince = (e: any) => {
        selectedProvince.value = e.component.dataItems[e.component.index]
        getCityList()
        selectedCity.value = undefined
        selectedDistrict.value = undefined
        selectedVillage.value = undefined
    }
    
    const onChangeCity = (e: any) => {
        selectedCity.value = e.component.dataItems[e.component.index]
        // console.log(e)
        getDistrictList()
        selectedDistrict.value = undefined
        selectedVillage.value = undefined
    }
    
    const onChangeDistrict = (e: any) => {
        selectedDistrict.value = e.component.dataItems[e.component.index]
        getVillageList()
        selectedVillage.value = undefined
    }
    
    const onChangeVillage = (e: any) => {
        selectedVillage.value = e.component.dataItems[e.component.index]
        // emit('updatePostalCode', selectedVillage.value?.postal)
        availablePostalCodes.value = selectedVillage.value?.postal
        // console.log(selectedVillage.value)
    }
    
    const onFilterCountry = (e : any) => {
        const data = process(sourceCountryList.value, {}).data as any[]
        countryList.value = filterBy(data, e.filter)
    }
    const onFilterProvince = (e : any) => {
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

    return {
        availablePostalCodes,
        currSelectedRegion,
        countryList,
        selectedCountry,
        provinceList,
        selectedProvince,
        cityList,
        selectedCity,
        districtList,
        selectedDistrict,
        villageList,
        selectedVillage,
        // emit,
        // defineEmits,
        refreshIndonesiaRegions,
        onChangeCountry,
        onChangeProvince,
        onChangeCity,
        onChangeDistrict,
        onChangeVillage,
        onFilterCountry,
        onFilterProvince,
        onFilterCity,
        onFilterDistrict,
        onFilterVillage
    }

})

if (import.meta.hot)
    import.meta.hot.accept(acceptHMRUpdate(useIndonesiaRegionStore, import.meta.hot))