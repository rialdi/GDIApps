<template>
    <form-element :horizontal="true" id="mainForm">
        <fieldset class="k-form-fieldset">
        <field :id="'clientId'" :name="'clientId'" :label="'Client'" :hint="'Hint: Client'" :data="clientList"
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
            :validator="requiredValidator"
        >
        <template v-slot:myTemplate="{props}">
            <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" autofocus/>
        </template>
        </field>
        <field :id="'name'" :name="'name'" :label="'Name'" :hint="'Hint: Project Name'"
        :component="'myTemplate'"
        :validator="requiredValidator"
        >
        <template v-slot:myTemplate="{props}">
            <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
        </template>
        </field>
        <field :id="'isActive'" :name="'isActive'" :label="'Is Active'" :component="'myTemplate'">
        <template v-slot:myTemplate="{props}">
            <formcheckbox
                v-bind="props"
                @change="props.onChange"
                @blur="props.onBlur"
                @focus="props.onFocus"
            ></formcheckbox>
        </template>
        </field>
        <!-- <div class="k-form-buttons k-buttons-end">
            <kbutton
            :theme-color="'primary'"
            :type="'submit'"
            :disabled="!formAllowSubmit"
            >
            Submit
            </kbutton>
            <kbutton @click="resetForm">
                Reset
            </kbutton>
        </div> -->
        </fieldset>
    </form-element>
</template>
<script>
import { Field, FormElement } from "@progress/kendo-vue-form";
import FormInput from "@/components/kform/FormInput.vue";
import FormCheckbox from "@/components/kform/FormCheckbox.vue";
import FormDropDownList from "@/components/kform/FormDropDownList.vue";
import { Button } from "@progress/kendo-vue-buttons";
import { Checkbox } from "@progress/kendo-vue-inputs";
import {
  emailValidator,
  requiredValidator
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
          requiredValidator
        }
    },
    methods: {
      resetForm(){
        this.kendoForm.onFormReset();
      }
    }
};

</script>