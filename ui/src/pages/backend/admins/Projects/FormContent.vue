<template>
<form-element :horizontal="true" id="mainForm">
    <fieldset class="k-form-fieldset">
        <field :id="'clientId'" :name="'clientId'" :label="'Client'" :data="clientList"
            :hint="'Hint: Client'" 
            :component="'myTemplate'"
            :validator="requiredValidator"
        >
            <template v-slot:myTemplate="{props}">
                <FormDropDownList v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"
                    valueField="id"
                    textField="name"
                />
            </template>
        </field>
        <field :id="'code'" :name="'code'" :label="'Code'" :hint="'Hint: Project Code'" 
            :component="'myTemplate'"
            :validator="nameValidator" 
        >
            <template v-slot:myTemplate="{props}">
                <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
            </template>
        </field>
        <field :id="'name'" :name="'name'" :label="'Name'" :hint="'Hint: Project Name'"
            :component="'myTemplate'"
            :validator="nameValidator"
        >
        <template v-slot:myTemplate="{props}">
            <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </field>
        <field :id="'description'" :name="'description'" :label="'Description'" :hint="'Hint: Project Description'" 
            :component="'myTemplate'" :max="200"
        >
        <template v-slot:myTemplate="{props}">
            <formtextarea v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :rows="4"/>
        </template>
        </field>
        <field :id="'isActive'" :name="'isActive'" :label="'Is Active'" :component="'myTemplate'">
            <template v-slot:myTemplate="{props}">
                <formcheckbox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
            </template>
        </field>
    </fieldset>
</form-element>
</template>

<script>
import { Field, FormElement } from "@progress/kendo-vue-form";
import FormInput from "@/components/kform/FormInput.vue";
import FormCheckbox from "@/components/kform/FormCheckbox.vue";
import FormDropDownList from "@/components/kform/FormDropDownList.vue";
import FormTextArea from "@/components/kform/FormTextArea.vue";
import { Button } from "@progress/kendo-vue-buttons";
import { Checkbox } from "@progress/kendo-vue-inputs";
import {
  emailValidator,
  requiredValidator,
  nameValidator,
} from "@/stores/validators";

export default {
    props: {
        clientList: Object
    },
    components: {
      field: Field,
      'form-element': FormElement,
      forminput: FormInput,
      formcheckbox: FormCheckbox,
      formtextarea: FormTextArea
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
          nameValidator
        }
    },
    methods: {
      resetForm(){
        this.kendoForm.onFormReset();
      }
    }
};

</script>