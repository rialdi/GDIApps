<template>
    <fieldwrapper>
        <klabel :editor-id="id" :editor-valid="valid">
        {{label}}
        </klabel>
        <div class="k-form-field-wrap">
            <datepicker :style="{ width: '230px' }" 
                :value="currValue"
                v-model="currValue"
                :format="dateFormat"
                :valid="valid"
                :id="id"
                @change="handleChange"
                @blur="handleBlur"
                @focus="handleFocus"
                />
            <error v-if="showValidationMessage">
                {{validationMessage}}
            </error>
            <hint v-else :direction="hintDirection">{{hint}}</hint>
        </div>
    </fieldwrapper> 
</template>
<script>
import { FieldWrapper } from '@progress/kendo-vue-form';
import { Error, Hint, Label } from '@progress/kendo-vue-labels';
import { DatePicker } from '@progress/kendo-vue-dateinputs';
import { toDate, dateFmtHM } from "@servicestack/client"
import { formatDate } from "@/utils"
export default {
    props: {
        hintDirection: String,
        touched: Boolean,
        format: String,
        label: String,
        validationMessage: String,
        hint: String,
        id: String,
        valid: Boolean,
        value: {
            type: Date,
            default: null,
        },
    },
    components: {
      fieldwrapper: FieldWrapper,
      error: Error,
      hint: Hint,
      klabel: Label,
      datepicker: DatePicker,
    },
    emits: {
        change: null,
        blur: null,
        focus: null,
    },
    data() {
        return {
            dateFormat: this.format.replace('DD', 'dd'),
            // cValue : this.field,
            currValue: toDate(this.value)
        };
    },
    computed: {
        currDisplayValue: function() {
            return formatDate(this.dataItem[this.field], this.format);
        },
        showValidationMessage() {
            return this.$props.touched && this.$props.validationMessage;
        },
        showHint() {
            return !this.showValidationMessage && this.$props.hint;
        },
        hintId() {
            return this.showHint ? `${this.$props.id}_hint` : '';
        },
        errorId() {
            return this.showValidationMessage ? `${this.$props.id}_error` : '';
        },
    },
    methods: {
        handleChange(e){
            // console.log(e.target.value);
            // var returnVal = dateFmtHM(e.target.value);
            // e.target.value = returnVal;
            // console.log(returnVal);
            // this.$emit('update:modelValue', returnVal);
            this.$emit('change', e);
        },
        handleBlur(e){
            this.$emit('blur', e);
        },
        handleFocus(e){
            this.$emit('focus', e);
        },
    },
};
</script>
