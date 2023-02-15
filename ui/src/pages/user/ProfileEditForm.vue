<template>
<!-- <kForm :initialValues="profileDataItem" @submit="onUpdateProfile"> -->
<kFormElement :horizontal="true" id="mainForm">
<BaseBlock title="User Info" btn-option-fullscreen btn-option-content>
    <template #options>
        <kButton title="Update Profile" type="submit" :disabled="!kendoForm.allowSubmit" :theme-color="'primary'">
            Update
        </kButton>
        <kButton title="Reset Profile" type="button" @click="resetForm" :theme-color="'secondary'">
            Reset
        </kButton>
    </template>
    <fieldset class="k-form-fieldset">
        <kField :id="'email'" :name="'email'" :label="'Email'" :component="'myTemplate'" :validator="emailValidator">
            <template v-slot:myTemplate="{props}">
                <kFormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
            </template>
        </kField>
        <kField :id="'fullName'" :name="'fullName'" :label="'Full Name'" :component="'myTemplate'" :validator="nameValidator">
        <template v-slot:myTemplate="{props}">
            <kFormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </kField>
        <kField :id="'phoneNumber'" :name="'phoneNumber'" :label="'Phone Number'" :component="'myTemplate'">
        <template v-slot:myTemplate="{props}">
            <kFormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </kField>
    </fieldset>
</BaseBlock>
</kFormElement>
<!-- </kForm> -->
</template>
<script>
import { auth, appUser, getAppUser } from "@/auth"
import { Field, FormElement } from "@progress/kendo-vue-form";
import FormInput from "@/components/kform/FormInput.vue";
import { Button} from '@progress/kendo-vue-buttons';

import {
  emailValidator,
  requiredValidator,
  nameValidator,
} from "@/stores/validators";


export default {
    // props: {
    //     // dataItem: Object
    // },
    components: {
        kField: Field,
        kFormElement: FormElement,
        kFormInput: FormInput,
        kButton: Button
    },
    async mounted() {
        // if(this.dataItem){
        //     this.profileDataItem = this.dataItem;
        // }
        // else {
        //     this.profileDataItem = await getAppUser(auth.value?.userName);
        // }
    },
    computed:{
        formAllowSubmit() {
            return this.kendoForm.allowSubmit
        }
    },
    inject: {
      kendoForm: { default: {} },  
    },
    data: function () {
        return { 
          emailValidator,
          requiredValidator,
          nameValidator,
          profileDataItem: null,
        //   files: [],
        //   profileUrl: null
        }
    },
    methods: {
      resetForm(){
        this.kendoForm.onFormReset();
      },
    //   onUpload(e) {
    //     console.log(e.target.value);
    //     console.log(e);
    //   },
    //   onAdd (event) {
    //         console.log('onAdd: ', event.affectedFiles);
    //         console.log(event)
    //         this.files= event.newState;
    //         console.log(this.files[0]);
    //     },
    }
};

</script>