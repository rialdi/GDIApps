<template>
<!-- <kForm :initialValues="profileDataItem" @submit="onUpdateProfile"> -->
<kFormElement :horizontal="true" id="mainForm">
<BaseBlock title="User Info" >
    <template #options>
        <KButton title="Update Profile" type="submit" :disabled="!allowSubmit" :theme-color="'primary'" >
            Update
        </KButton>
        <KButton title="Reset Profile" type="button" @click="resetForm" :theme-color="'secondary'" >
            Reset
        </KButton>
    </template>
    <fieldset class="k-form-fieldset mt-0">
        <kField :id="'email'" :name="'email'" :label="'Email'" :component="'myTemplate'" :validator="emailValidator">
            <template v-slot:myTemplate="{props}">
                <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
            </template>
        </kField>
        <kField :id="'displayName'" :name="'displayName'" :label="'Display Name'" :component="'myTemplate'" :validator="nameValidator">
        <template v-slot:myTemplate="{props}">
            <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </kField>
        <kField :id="'fullName'" :name="'fullName'" :label="'Full Name'" :component="'myTemplate'" :validator="nameValidator">
        <template v-slot:myTemplate="{props}">
            <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </kField>
        <kField :id="'phoneNumber'" :name="'phoneNumber'" :label="'Phone Number'" :component="'myTemplate'" :validator="phoneValidator">
        <template v-slot:myTemplate="{props}">
            <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </kField>
    </fieldset>
</BaseBlock>
</kFormElement>
<!-- </kForm> -->
</template>
<script setup lang="ts">
// import { auth, appUser, getAppUser } from "@/auth"
import { Field as kField, FormElement as kFormElement } from "@progress/kendo-vue-form";
import  FormInput  from "@/components/kform/FormInput.vue";
import { Button as KButton} from '@progress/kendo-vue-buttons';

import {
  emailValidator,
  phoneValidator,
  nameValidator,
} from "@/stores/validators";

const allowSubmit = computed(() => {
    return true
})

const resetForm = () => {

}

defineExpose( 
    
)

// export default {
//     // props: {
//     //     // dataItem: Object
//     // },
//     components: {
//         kField: Field,
//         kFormElement: FormElement,
//         kFormInput: FormInput,
//         kButton: Button
//     },
//     async mounted() {
//         // if(this.dataItem){
//         //     this.profileDataItem = this.dataItem;
//         // }
//         // else {
//         //     this.profileDataItem = await getAppUser(auth.value?.userName);
//         // }
//     },
//     computed:{
//         formAllowSubmit() {
//             return this.kendoForm.allowSubmit
//         }
//     },
//     inject: {
//       kendoForm: { default: {} },  
//     },
//     data: function () {
//         return { 
//           emailValidator,
//           requiredValidator,
//           nameValidator,
//           profileDataItem: null,
//         //   files: [],
//         //   profileUrl: null
//         }
//     },
//     methods: {
//       resetForm(){
//         this.kendoForm.onFormReset();
//       },
//     //   onUpload(e) {
//     //     console.log(e.target.value);
//     //     console.log(e);
//     //   },
//     //   onAdd (event) {
//     //         console.log('onAdd: ', event.affectedFiles);
//     //         console.log(event)
//     //         this.files= event.newState;
//     //         console.log(this.files[0]);
//     //     },
//     }
// };

</script>