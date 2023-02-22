<template>
    <fieldwrapper>
        <klabel :editor-id="id" :editor-valid="valid">
        {{label}}
        </klabel>
        <div class="k-form-field-wrap">
             <dropdownlist
                :data-items="provinceList" 
                :valid="valid"
                :value="pvalue"
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
import { Province, City, District, Village, QueryProvinces, QueryCities, QueryDistricts, QueryVillages } from "@/dtos"
import { client } from "@/api"
import { process, filterBy } from '@progress/kendo-data-query'

export default {
    props: {
        textField: String,
        touched: Boolean,
        label: String,
        validationMessage: String,
        hint: String,
        id: String,
        valid: Boolean,
        pvalue: {
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
    created() {
        this.getProvinceData();
        // console.log(this.props.pvalue);
    },
    mounted(){
        // console.log(this.props.pvalue);
        // this.provinceValue = this.props.value;
    },
    data() {
        return {
            sourceProvinceList : [],
            provinceList: [],
            provinceValue: null
        }
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
        async getProvinceData() {
            const api = await client.api(new QueryProvinces({ countryId: 62 }));
            if (api.succeeded && api.response) {
                this.sourceProvinceList = api.response.results;
                // this.provinceList = this.sourceProvinceList;
                this.provinceList = process(this.sourceProvinceList, {}).data;
                // console.log(this.provinceList);
            }
        },
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
