<template>
    <span v-if="!dataItem.inEdit" :class="className">{{ currDisplayValue }}</span>
    <KTimePicker v-else
        :id="'dpSA'"
        :value="currValue"
        v-model="currValue"
        :format="dateFormat"
        :disabled="disabled"
        :required="required"
        :validation-message="valMessage"
        @change="change">
    </KTimePicker>
</template>
<script>
import { TimePicker } from '@progress/kendo-vue-dateinputs';
import { toDate, dateFmtHM } from "@servicestack/client"
import { formatDate } from "@/utils"

export default {
    components: {
        KTimePicker: TimePicker,
    },
    props: {
        field: String,
        dataItem: Object,
        format: String,
        disabled: Boolean,
        required: Boolean,
        className: String,
        columnIndex: Number,
        columnsCount: Number,
        rowType: String,
        level: Number,
        expanded: Boolean,
        editor: String,
        valMessage: String
    }, 
    mounted() {

    },
    computed: {
        currDisplayValue: function() {
            return formatDate(this.dataItem[this.field], this.format);
        },
    },
    watch: {
        // currValue: function() {
        //     this.modelValue = dateFmt(this.currValue)
        // }
    },
    data() {
        return {
            dateFormat: this.format.replace('DD', 'dd'),
            // cValue : this.field,
            currValue: toDate(this.dataItem[this.field])
        };
    },
    methods: {
        change(e) {
            var returnVal = dateFmtHM(e.target.value);
            this.dataItem[this.field] = returnVal;
            // console.log(returnVal);
            this.$emit('update:modelValue', returnVal);
            this.$emit('change', e);
        },
    },
};
</script>