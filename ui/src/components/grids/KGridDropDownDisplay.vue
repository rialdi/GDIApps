<template>
    <dropdownlist
        :style="{ width: '100%' }"
        @change="change"
        :value="dataItem[field]"
        v-model="dataItem[field]"
        :data-items="itemList"
        :value-field="valueField"
        :text-field="textField"
        :value-primitive="true">
    </dropdownlist>
</template>
<script>
import { DropDownList } from '@progress/kendo-vue-dropdowns';

export default {
    components: {
        dropdownlist: DropDownList,
    },
    props: {
        field: String,
        itemList: Array,
        dataItem: Object,
        format: String,
        className: String,
        columnIndex: Number,
        columnsCount: Number,
        rowType: String,
        level: Number,
        expanded: Boolean,
        editor: String,
        valueField: String,
        textField: String,
    },
    data() {
        return {
        };
    },
    methods: {
        change(e) {
            this.$emit('update:modelValue', e, e.target.value);
            this.$emit('change', e);
        },
    },
};
</script>