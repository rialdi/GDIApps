<script>
import { Spreadsheet, SpreadsheetSheet } from '@progress/kendo-spreadsheet-vue-wrapper';
import "@progress/kendo-ui/js/kendo.spreadsheet";
import JSZip from 'jszip';
window.JSZip = JSZip;

export default {
    name: 'App', 
    props: {
        toolbar: null,
        sheetsbar: null
    },
    emits: {
        getdata: null
    },
    components: {
        'spreadsheet': Spreadsheet,
        'spreadsheet-sheet': SpreadsheetSheet,
    },
    data: function (){
		return {
            JsonData: null
        }
    },
    mounted: function () {
        var spreadsheet = this.$refs.spreadsheet.kendoWidget()
        spreadsheet.element.css('height', '400px')
        spreadsheet.element.css('width', '100%')
        spreadsheet.resize()
    },
    methods: {
        RefreshSpreadSheet(jsonData) {
            // console.log('RefreshSpreadSheet')
            // console.log(jsonData)
            var spreadsheet = this.$refs.spreadsheet.kendoWidget()
            spreadsheet.fromJSON(jsonData)
        },
        GetSpreadSheet(){
            var spreadsheet = this.$refs.spreadsheet.kendoWidget();
            return spreadsheet;
        },
        async GetJsonData() {
            var spreadsheet = this.$refs.spreadsheet.kendoWidget();
            spreadsheet.saveJSON().then(function(data){
                var json = JSON.stringify(data, null, 2);
                // console.log('GetJsonData')
                // console.log(json)
                return json;
                // $emit('getdata', { jsonData: json });
                // // const api = client.api(new SaveDataEntryImacs({
                // //         spreadSheet: JSON.parse(json)
                // //     })
                // );
            });
        },
        onbtnSaveClick() {
            var spreadsheet = this.$refs.spreadsheet.kendoWidget();
            var data = null;
            spreadsheet.saveJSON().then(function(data){
                var json = JSON.stringify(data, null, 2);
                $emit('getdata', { jsonData: json });
                // const api = client.api(new SaveDataEntryImacs({
                //         spreadSheet: JSON.parse(json)
                //     })
                // );
            });
        }

    }
}
</script>

<template>
    <spreadsheet 
        ref="spreadsheet"
        :toolbar="toolbar"
        :sheetsbar="sheetsbar"
    ></spreadsheet>
</template>