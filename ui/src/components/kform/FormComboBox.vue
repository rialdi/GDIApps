<template>
    <fieldwrapper>
        <klabel :editor-id="id" :editor-valid="valid">
        {{label}}
        </klabel>
        <div class="k-form-field-wrap">
             <comboBox
                :name="name"
                :data-items="dataItems" 
                :valid="valid"
                :value="value"
                :id="id"
                :text-field="textField"
                :value-field="valueField"
                :data-item-key="dataItemKey"
                :value-primitive="valuePrimitive"
                @change="handleChange"
                @blur="handleBlur"
                @focus="handleFocus"
                />
            <error v-if="showValidationMessage">
                {{validationMessage}}
            </error>
            <hint v-else>{{hint}}</hint>
        </div>
    </fieldwrapper> 
</template>
<script>
import { FieldWrapper } from "@progress/kendo-vue-form";
import { Error, Hint, Label } from "@progress/kendo-vue-labels";
import { ComboBox } from "@progress/kendo-vue-dropdowns";
export default {
    props: {
        textField: String,
        valueField: String,
        dataItemKey: String,
        valuePrimitive: Boolean,
        dataItems: Object,
        name: String,
        touched: Boolean,
        label: String,
        validationMessage: String,
        hint: String,
        id: String,
        valid: Boolean,
        value: {
             type: [String, Number] ,
             default: function(){
                 return {};
             }
        }
    },
    components: {
        fieldwrapper: FieldWrapper,
        error: Error,
        hint: Hint,
        klabel: Label,
        comboBox: ComboBox
    },
    emits: {
        // update:modelValue,
        change: null,
        blur: null,
        focus: null
    },
    computed: {
        showValidationMessage() {
            return this.$props.touched && this.$props.validationMessage;
        },
        showHint() {
            return !this.showValidationMessage && this.$props.hint;
        },
        hintId() {
            return this.showHint ? `${this.$props.id}_hint` : "";
        },
        errorId() {
            return this.showValidationMessage ? `${this.$props.id}_error` : "";
        }
    },
    methods: {
        handleChange(e){
            // console.log('handleChange')
            // console.log(e)
            // this.$emit('update:modelValue', e.value)
            this.$emit('change', e);
        },
        handleBlur(e){
            this.$emit('blur', e);
        },
        handleFocus(e){
            this.$emit('focus', e);
        }
    }
}
</script>
