<script setup lang="ts">
import { auth } from "@/auth"
import { UpdatePassword } from "@/dtos";
import { client } from "@/api"
import { passwordValidator, formChangePasswordValidator } from "@/stores/validators";
import { showNotifSuccess, showNotifError } from '@/stores/commons'
import { Form as KForm, Field as KField, FormElement as KFormElement, FormValidatorType } from "@progress/kendo-vue-form";
import  FormInput  from "@/components/kform/FormInput.vue";
import { Button as KButton} from '@progress/kendo-vue-buttons';

// import { useTemplateStore } from "@/stores/template";

// const store = useTemplateStore();

const frmChangePasswordRef = ref<InstanceType<typeof KForm>>()

const allowSubmit = computed(() => {
    return frmChangePasswordRef.value?.form.valid ?? false
})

// const changePasswordData = null

const onChangePassword = async (dataItem : any) => {
    console.log(dataItem)
    // console.log(frmChangePasswordRef.value)
    // const formValues = frmChangePasswordRef.value?.values
    // console.log(formValues)

    const api = await client.api(new UpdatePassword({
        username: auth.value?.userName,
        currPassword: dataItem.currPassword,
        newPassword: dataItem.newPassword
    }))
    if (api.succeeded) {
      showNotifSuccess('Change Password', 'Your password has been changed 🎉')
      onFormPasswordClear()
    } 
    else
    {
        showNotifError('Change Password', api.errorMessage)
    }
    // console.log(dataItem)

}

const onFormPasswordClear = () => {
    
    // console.log(frmChangePasswordRef.value?.form)
    frmChangePasswordRef.value?.form.onFormReset()
}

defineExpose( 
    
)
</script>
<template>
    <KForm id="frmChangePassword" ref="frmChangePasswordRef" @submit="onChangePassword" :validator="(formChangePasswordValidator as FormValidatorType)">
        <KFormElement :horizontal="true" id="frmChangePassword">
            <BaseBlock title="Password" btn-option-content>
                <template #options>
                    <KButton type="submit" :disabled="!allowSubmit" :theme-color="'primary'" >
                        Change Password
                    </KButton>
                    <KButton type="button" @click="onFormPasswordClear" :theme-color="'secondary'" >Clear</KButton>
                </template>
                <fieldset class="k-form-fieldset mt-0">
                    <KField :id="'currPassword'" :name="'currPassword'" :label="'Current Password'" :component="'myTemplate'" :validator="passwordValidator">
                        <template v-slot:myTemplate="{props}">
                            <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :type="'password'"/>
                        </template>
                    </KField>
                    <KField :id="'newPassword'" :name="'newPassword'" :label="'New Password'" :component="'myTemplate'" :validator="passwordValidator">
                        <template v-slot:myTemplate="{props}">
                            <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :type="'password'"/>
                        </template>
                    </KField>
                    <KField :id="'newPasswordConfirm'" :name="'newPasswordConfirm'" :label="'Password Confirmation'" :component="'myTemplate'" :validator="passwordValidator">
                    <template v-slot:myTemplate="{props}">
                        <FormInput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :type="'password'"/>
                    </template>
                    </KField>
                </fieldset>
                <!-- <div class="k-form-buttons">
                    <KButton type="submit" :disabled="!allowSubmit">
                        Submit
                    </KButton>
                </div> -->
            </BaseBlock>
        </KFormElement>
        
    </KForm>
</template>