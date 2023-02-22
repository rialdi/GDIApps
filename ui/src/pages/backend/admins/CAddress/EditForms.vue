<template>
<form @submit.prevent="onSubmit" id="mainForm" class="k-form">
    <fieldset>
    <div class="row">
        <div class="col-6 p-1">
            <KComboBox 
                :id="'client'" :label="'Client'" :valueField="'id'" :textField="'name'"
                :dataItems="clientList" v-model="dataItemInEdit.clientId" :value="dataItemInEdit.clientId" :valid="true" :showLabel="true"
            />
        </div>
        <div class="col-6 p-1">
            <KTextInput id="Address Name" v-model="dataItemInEdit.addressName" :validator="nameValidator"
                :showLabel="true" :label="'Address Name'" :type="'string'" :required="true"/>
        </div>
    </div>
    <!-- <div class="row mb-1">
        <div class="col-6 p-2">
            <KComboBox 
                :name="'country'"
                :label="'Country'"
                :data-items="indonesiRegionStore.countryList"
                :value-primitive="true"
                :valueField="'name'" 
                :textField="'name'"
                :filterable="true"
                @filterchange="indonesiRegionStore.onFilterCountry"
                @change="indonesiRegionStore.onChangeCountry"
                v-model="dataItemInEdit.country"
                :required="true"
            />
        </div>
        <div class="col-6 p-2">
            <KComboBox 
                :style="{ width: '100%' }"
                :label="'Province'"
                v-model="dataItemInEdit.province"
                @change="indonesiRegionStore.onChangeProvince"
                :name="'province'"
                :data-items="indonesiRegionStore.provinceList"
                :value-primitive="true"
                :valueField="'name'" :textField="'name'"
            />
        </div>
    </div> -->
    <div class="row">
        <div class="col-6 p-2">
            <KComboBox 
                :id="'Country'" :label="'Country'" :valueField="'name'" :textField="'name'"
                :dataItems="indonesiRegionStore.countryList" v-model="dataItemInEdit.country" :value="dataItemInEdit.country" :valid="true" :showLabel="true"
                @change="indonesiRegionStore.onChangeCountry" @filterchange="indonesiRegionStore.onFilterCountry"
                :hint="'Country'" :touched="false" :validationMessage="'Ok'" 
            />
        </div>
        <div class="col-6 p-2">
            <KComboBox 
                :id="'Province'" :label="'Province'" :valueField="'name'" :textField="'name'"
                :dataItems="indonesiRegionStore.provinceList" v-model="dataItemInEdit.province" :value="dataItemInEdit.province" :valid="true" :showLabel="true"
                @change="indonesiRegionStore.onChangeProvince" @filterchange="indonesiRegionStore.onFilterProvince" :disable="!indonesiRegionStore.selectedCountry"
            />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-2">
            <KComboBox 
                :id="'City'" :label="'City'" :valueField="'name'" :textField="'name'"
                :dataItems="indonesiRegionStore.cityList" v-model="dataItemInEdit.city" :value="dataItemInEdit.city" :valid="true" :showLabel="true"
                :disable="!indonesiRegionStore.selectedProvince"
                @change="indonesiRegionStore.onChangeCity" @filterchange="indonesiRegionStore.onFilterCity"
            />
        </div>
        <div class="col-6 p-2">
            <KComboBox 
                :id="'District'" :label="'District'" :valueField="'name'" :textField="'name'"
                :dataItems="indonesiRegionStore.districtList" v-model="dataItemInEdit.district" :value="dataItemInEdit.district" :valid="true" :showLabel="true"
                :disable="!indonesiRegionStore.selectedCity"
                @change="indonesiRegionStore.onChangeDistrict" @filterchange="indonesiRegionStore.onFilterDistrict"
            />
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-1">
            <KComboBox 
                :id="'Village'" :label="'Village'" :valueField="'name'" :textField="'name'"
                :dataItems="indonesiRegionStore.villageList" v-model="dataItemInEdit.village" :value="dataItemInEdit.village" :valid="true" :showLabel="true"
                :disable="!indonesiRegionStore.selectedDistrict"
                @change="indonesiRegionStore.onChangeVillage" @filterchange="indonesiRegionStore.onFilterVillage"
            />
        </div>
        <div class="col-6 p-1">
            <KTextInput id="postalCode"  v-model="indonesiRegionStore.availablePostalCodes" 
                :showLabel="true" :label="'Postal Code'" :type="'string'" :required="false"/>
        </div>
    </div>
    <div class="row">
        <div class="col-12 p-1">
            <KTextArea id="address1" :showLabel="true" :label="'Address 1'" v-model="dataItemInEdit.address1" :rows="3" :max="'150'"/>
        </div>
    </div>
    <div class="row">
        <div class="col-12 p-1">
            <KTextArea id="address2" :showLabel="true" :label="'Address 2'" v-model="dataItemInEdit.address2" :rows="3" :max="'150'"/>
        </div>
    </div>
    <div class="row">
        <div class="col-6 p-1">
            <KTextInput id="phoneNo" v-model="dataItemInEdit.phoneNo" :validator="phoneValidator"
                :showLabel="true" :label="'Phone No'" :type="'string'" :required="true"/>
            <!-- <KMaskedTextBox id="phoneNo"  v-model="dataItemInEdit.phoneNo" :validator="phoneValidator"
                :showLabel="true" :label="'Phone No Mask'" :mask="'+6200000000000'"/> -->
        </div>
        <div class="col-6 p-1">
            <KCheckbox id="isMain" v-model="dataItemInEdit.isMain" 
                :showLabel="true" :label="'Is Active'" />
        </div>
    </div>
    <!-- <div class="row">
        <div class="col-6 p-1">
            <KTextInput id="phoneNo" v-model="dataItemInEdit.phoneNo" :validator="emailValidator"
                :showLabel="true" :label="'Phone No'" :type="'email'" :required="true"/>
        </div>
        <div class="col-6 p-1">
            <KTextInput id="phoneNo" v-model="dataItemInEdit.phoneNo" :validator="passwordValidator" 
                :showLabel="true" :label="'Phone No'" :type="'password'" :required="true"/>
        </div>
    </div> -->

    <!-- <span>{{ dataItemInEdit.phoneNo }}</span> -->
    <!-- <span>{{ indonesiRegionStore.availablePostalCodes }}</span> -->
    <!-- <span>{{ dataItemInEdit.addressName }}</span>
    <br/>
    <span>{{ indonesiRegionStore.availablePostalCodes }}</span>
    <br/>
    <span>{{ dataItemInEdit?.postalCode }}</span>
    <br/>
    <span>{{ dataItemInEdit.country }}</span>
    <br/>
    <span>{{ dataItemInEdit.province }}</span>
    <br/>
    <span>{{ indonesiRegionStore.selectedProvince }}</span>
    <br/>
    <span>{{ dataItemInEdit.city }}</span>
    <br/>
    <span>{{ dataItemInEdit.district }}</span>
    <br/>
    <span>{{ dataItemInEdit.village }}</span>
    <br/> -->
    </fieldset>
</form>
</template>

<script setup lang="ts">
import { CAddress } from "@/dtos"
import KComboBox from "@/components/kendo/KComboBox.vue"
import KCheckbox from "@/components/kendo/KCheckbox.vue"
import KTextArea from "@/components/kendo/KTextArea.vue"
import KTextInput from "@/components/kendo/KTextInput.vue"
// import KMaskedTextBox from "@/components/kendo/KMaskedTextBox.vue"

import { useIndonesiaRegionStore } from "@/stores/regionindonesia"

import { nameValidator, phoneValidator } from "@/stores/validators"
// import { passwordValidator, emailValidator } from "@/stores/validators"

let dataItemInEdit = ref<CAddress>(new CAddress())

const indonesiRegionStore = useIndonesiaRegionStore()
// indonesiRegionStore.refreshIndonesiaRegions()

let props = defineProps<{
    dataItem: any,
    clientList: any[]
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    resetForm()
});

const resetForm = async () => {
    // indonesiRegionStore.currSelectedCountryName = props.dataItem?.country
    // console.log(indonesiRegionStore.currSelectedCountryName)
    dataItemInEdit.value = Object.assign({}, props.dataItem as CAddress)
    const currSelectedRegion =  { 
        "Country" : props.dataItem?.country, 
        "Province" : props.dataItem?.province,
        "City" : props.dataItem?.city,
        "District" : props.dataItem?.district,
        "Village" : props.dataItem?.village
    }
    indonesiRegionStore.availablePostalCodes = dataItemInEdit.value.postalCode
    indonesiRegionStore.refreshIndonesiaRegions(currSelectedRegion)
    
}

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.country = indonesiRegionStore.selectedCountry?.name
    dataItemInEdit.value.province = indonesiRegionStore.selectedProvince?.name
    dataItemInEdit.value.city = indonesiRegionStore.selectedCity?.name
    dataItemInEdit.value.district = indonesiRegionStore.selectedDistrict?.name
    dataItemInEdit.value.village = indonesiRegionStore.selectedVillage?.name
    dataItemInEdit.value.postalCode = indonesiRegionStore.availablePostalCodes
    emit('save', { dataItem : dataItemInEdit.value })
}

defineExpose({
    resetForm
})
</script>