<template>
    <fieldwrapper>
        <klabel :editor-id="id" :editor-valid="valid">
        {{label}}
        </klabel>
        <div class="k-form-field-wrap">
             <dropdownlist
                :data-items="dataItems" 
                :valid="valid"
                :value="value"
                :id="id"
                :value-field="valueField"
                :text-field="textField"
                :value-primitive="true"
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
import { DropDownList } from "@progress/kendo-vue-dropdowns";
export default {
    props: {
        textField: String,
        dataItems: Object,
        touched: Boolean,
        label: String,
        validationMessage: String,
        hint: String,
        id: String,
        valid: Boolean,
        value: {
             type: [String, Number],
             default: ''
        },
        valueField: String,
        textField: String
    },
    components: {
        fieldwrapper: FieldWrapper,
        error: Error,
        hint: Hint,
        klabel: Label,
        dropdownlist: DropDownList
    },
    emits: {
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
