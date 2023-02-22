<template>
    <form-element :horizontal="true" id="mainForm" :style="{ maxWidth: '650px' }">
        <fieldset class="k-form-fieldset">
            <field :id="'clientId'" :name="'clientId'" :label="'Client'" 
                :data="clientList"
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
            <div :style="{ display: 'flex', justifyContent: 'space-between' }">
                <field :id="'addressName'" :name="'addressName'" :label="'Address Name'" 
                    :component="'myTemplate'"
                    :validator="nameValidator" 
                >
                    <template v-slot:myTemplate="{props}">
                        <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                    </template>
                </field>
                <field :id="'isMain'" :name="'isMain'" :label="'Is Main'" :component="'myTemplate'">
                    <template v-slot:myTemplate="{props}">
                        <formcheckbox v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                    </template>
                </field>
            </div>
            <div :style="{ display: 'flex', justifyContent: 'space-between' }">
                <field :id="'postalCode'" :name="'postalCode'" :label="'Postal Code'"
                    :component="'myTemplate'" :max="200"
                >
                <template v-slot:myTemplate="{props}">
                    <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                </template>
                </field>
                <field :id="'phoneNo'" :name="'phoneNo'" :label="'Phone No'"
                    :component="'myTemplate'" :max="200"
                >
                <template v-slot:myTemplate="{props}">
                    <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                </template>
                </field>
            </div>
            <div :style="{ display: 'flex', justifyContent: 'space-between' }">
                <field :id="'country'" :name="'country'" :label="'Country'"
                    :component="'myTemplate'"
                >
                <template v-slot:myTemplate="{props}">
                    <cboProvince v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"
                        valueField="name"
                        textField="name"
                        v-bind:pvalue="props.value"
                    />
                </template>
                </field>
                <field :id="'state'" :name="'state'" :label="'State'"
                    :component="'myTemplate'" :max="200"
                >
                <template v-slot:myTemplate="{props}">
                    <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                </template>
                </field>
            </div>
            <div :style="{ display: 'flex', justifyContent: 'space-between' }">
                <field :id="'city'" :name="'city'" :label="'City'"
                    :component="'myTemplate'" :max="200"
                >
                <template v-slot:myTemplate="{props}">
                    <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                    <!-- <formtextarea v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :rows="4"/> -->
                </template>
                </field>
                <field :id="'district'" :name="'district'" :label="'District'"
                    :component="'myTemplate'" :max="200"
                >
                <template v-slot:myTemplate="{props}">
                    <forminput v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus"/>
                </template>
                </field>
            </div>
            
            <field :id="'address1'" :name="'address1'" :label="'Address 1'"
                :component="'myTemplate'" :max="200"
            >
            <template v-slot:myTemplate="{props}">
                <formtextarea v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :rows="4"/>
            </template>
            </field>
            <field :id="'address2'" :name="'address2'" :label="'Address 2'"
                :component="'myTemplate'" :max="200"
            >
            <template v-slot:myTemplate="{props}">
                <formtextarea v-bind="props" @change="props.onChange" @blur="props.onBlur" @focus="props.onFocus" :rows="4"/>
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
    import FormDropDownListProvince from "@/components/kform/FormDropDownListProvince.vue";
    import FormTextArea from "@/components/kform/FormTextArea.vue";
    import { Button } from "@progress/kendo-vue-buttons";
    import { Checkbox } from "@progress/kendo-vue-inputs";
    import {
      emailValidator,
      requiredValidator,
      nameValidator,
    } from "@/stores/validators";

    
    import provinces from "@/data/regions/provinces";
    import cities from "@/data/regions/cities";
    import districts from "@/data/regions/districts";
    import villages from "@/data/regions/villages";
    
    export default {
        props: {
            clientList: Object
        },
        components: {
          field: Field,
          'form-element': FormElement,
          forminput: FormInput,
          formcheckbox: FormCheckbox,
          formtextarea: FormTextArea,
          cboProvince: FormDropDownListProvince
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