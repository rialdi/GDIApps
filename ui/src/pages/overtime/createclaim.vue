<template>
<BasePageHeading title="Create Overtime Claim" subtitle="Create Draft">
    <template #extra>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb breadcrumb-alt">
                    <li class="breadcrumb-item">
                        <a class="link-fx" href="javascript:void(0)">Overtime</a>
                    </li>
                    <li class="breadcrumb-item" aria-current="page">Create Claim</li>
                </ol>
            </nav>
        </template>
</BasePageHeading>
<div class="content">
    <BaseBlock title="Employee Selection" btn-option-fullscreen btn-option-content>
       <div>selected date: {{ selectedDate }}
    <DatePicker :style="{width:'230px'}" v-model="selectedDate"></DatePicker>
    </div>
       <div>selected employees:{{ selectedEmpIds }}</div>
       <kGrid 
            :columns="gridEmployeeColumn" 
            :data-items="gridEmployeeData"
            :pageable="true"
            :skip="grdEmployeePaging.skip"
            :take="grdEmployeePaging.take"
            :total="grdEmployeePaging.total"
            @pagechange="grdEmployeePagingHandler" 
            :filterable="true"
            :filter="grdEmployeePaging.filter"
            @filterchange="grdEmpFilterHandler"
            :loader="grdEmpLoader"
            @selectionchange="onGridEmployeeSelect" :style="{height:'440px'}" :selected-field="'selected'">
        <template v-slot:EmptyHeader="{props}">
        <div></div>
        </template>
        </kGrid>
        <kbutton @click="CreateDraft">Create Draft</kbutton>


    </BaseBlock>
    <BaseBlock title="Overtime Draft" btn-option-fullscreen btn-option-content>
        <kGrid
        :columns="GridOTColumn"
        :data-items="GridOTData"
        :edit-field="'inEdit'"
        @itemchange="otItemChange"
        >
        <template v-slot:CommandTemplate="{props}">
        <CommandCell 
        :data-item="props.dataItem" 
        @edit="onClaimEdit"
        @save="onClaimSave"
        @remove="onClaimRemove"
        @cancel="onClaimCancelChanges"/>
        </template>
        <template v-slot:reasonCell="{props}">
        <td v-if="!props.dataItem.inEdit" :class="props.className">{{ props.dataItem[props.field] }}</td>
        <td v-else>
            <DropDownList
                :data-items="OTReasonData"
                :text-field="'lookupText'"
                :data-item-key="'lookupValue'" 
                :value-field="'lookupValue'"
                v-model="UserInput.OT_REASON_CODE"
                :value-primitive="true"
            ></DropDownList>
        </td>
        </template>
        <template v-slot:otnumbercell="{props}">
        <td v-if="IsSubmitable(props.dataItem)">
            <div>{{ props.dataItem.OT_NUMBER }}</div>
            <div>
                <kbutton icon="redo" :title="Submit"
                :theme-color="'success'"
                style="width:30px; height:30px;"
                @click="submit(props.dataItem)"
                ></kbutton>
            </div>
        </td>
        <td v-else>
            <div>{{ props.dataItem.OT_NUMBER }}</div>
        </td>
        </template>
    </kGrid>
        {{ UserInput }}
    </BaseBlock>
</div>

</template>
<script setup lang="ts">
import { onMounted,ref,computed,reactive } from 'vue';
import {EmployeeOption,EmployeeSelections,EmployeeSelectionsResponse,CreateOvertimeDraft,CreateOvertimeResponse, QueryOvertimeDraft, Lookup, QueryLookups, LOOKUPTYPE, UpdateClaimRequest, SubmitClaimRequest} from "@/dtos";
import {client} from "@/api";
import {Grid as kGrid,GridColumnProps,GridSelectionChangeEvent,GridPageChangeEvent,GridFilterChangeEvent} from "@progress/kendo-vue-grid";
import {DataResult} from "@progress/kendo-data-query";
import { DatePicker } from '@progress/kendo-vue-dateinputs';
import { Button as kbutton } from '@progress/kendo-vue-buttons';
import CommandCell from '../../layouts/partials/KGridCommandCell.vue';
import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
import {showNotifSuccess,showNotifError} from '@/stores/commons';

let selectedDate=ref<Date>(new Date());
let selectedEmpIds=ref<string[]>([]);
let EmployeeOptions=ref<EmployeeOption[]>([]);
let gridEmployeeData=ref<DataResult>({data:[] as any,total:0}).value;

const gridEmployeeColumn=[
    {field:'selected',title:'',headerCell:"EmptyHeader",filterable:false},
{field:'EMPLOYEE_ID',title:"Employee ID"},
{field:"NAME",title:"Name"},
{field:"POS_DEPT",title:"Department"}
] as GridColumnProps;
const grdEmployeePaging=reactive({
    skip:0,
    take:10,
    total:1000,
    filter:{
        logic:'and',
        filters:[]
    }
});
let grdEmpLoader=ref<boolean>(false);
const grdEmployeePagingHandler=(e:GridPageChangeEvent)=>{
grdEmployeePaging.skip=e.page.skip;
grdEmployeePaging.take=e.page.take;

LoadEmployeeOptions();
}
const grdEmpFilterHandler=async (e:GridFilterChangeEvent)=>{
    grdEmployeePaging.filter=e.filter;
 await LoadEmployeeOptions();
}
const MapGridEmployee=()=>{
    gridEmployeeData.data=EmployeeOptions.value.map((item,index)=>{
        return {
            ...item,
            EmpDisplay:item.EMPLOYEE_ID + ' - '+item.NAME,
            selected:selectedEmpIds.value.indexOf(item.EMPLOYEE_ID!)>=0?true:false
        }
    })
}
const onGridEmployeeSelect=(e: GridSelectionChangeEvent)=>{
    let idx=selectedEmpIds.value.indexOf(e.dataItem.EMPLOYEE_ID);
    if(idx>=0){
        selectedEmpIds.value.splice(idx);
    }else{
        selectedEmpIds.value.push(e.dataItem.EMPLOYEE_ID);
    }
}
const LoadEmployeeOptions=async()=>{
    grdEmpLoader.value=true
    let strOdata='';
    if(grdEmployeePaging.skip>=0)
        strOdata="$skip="+grdEmployeePaging.skip;
    if(grdEmployeePaging.take>=0)
        strOdata+="&$top="+grdEmployeePaging.take;
    if(grdEmployeePaging.filter!=null){
        let strOdataFilter=grdEmployeePaging.filter.filters.map((f:any,i:number)=>{
            if (f.operator == 'startswith')
                    return `startsWith(${f.field},'${f.value}')`;
                if (f.operator == 'eq')
                    return `${f.field} eq '${f.value}'`;
        }).join(` ${grdEmployeePaging.filter.logic} `);
        strOdata+='&$filter='+strOdataFilter;
    }
    let request=new EmployeeSelections({oDataParam:strOdata});
    var apiResult=await client.api(request);
    if(apiResult.completed){
        if(apiResult.succeeded){
            EmployeeOptions.value=apiResult.response?.employees??[];
            MapGridEmployee();
        }
        grdEmpLoader.value=false;
    }
}
const GetSelectedDatestring=()=>{
    var month = selectedDate.value.toLocaleString("en-US", { month: 'short' }).toUpperCase();
        var day = selectedDate.value.toLocaleString("en-US", { day: '2-digit' });
        var year = selectedDate.value.toLocaleString("en-US", { year: 'numeric' });
        return `${day}-${month}-${year}`;
}
const CreateDraft= async(e:any)=>{
    if(selectedDate.value && selectedEmpIds.value.length>0){
        var request=new CreateOvertimeDraft({otDate:GetSelectedDatestring(),employeeIds:selectedEmpIds.value});
        var apiRes=await client.api(request);
        if(apiRes.completed){
          // alert("created: "+apiRes.response?.items?.length);
           showNotifSuccess('Create Overtime',"created: "+apiRes.response?.items?.length);
           await LoadOTDraftData();
        }

    }
}

let OvertimeDraftData=ref<Overtime[]>([]);
let GridOTData=ref<DataResult>({data:[] as any,total:0}).value;
let InEditOTData=ref<string>('');
const GridOTColumn=[ 
    {field:"OT_NUMBER",title:"OT Number",editable:false,cell:"otnumbercell"},
    {field:"EmpDisplay",title:"Employee",editable:false},
    { field: "OT_DATE", title: "OT Date" ,editable:false},
    { field: "OT_HOUR", title: 'OT Hour' ,editable:true,editor:"numeric"},
    { field: "OT_REASON", title: "Reason",editable:true,cell:"reasonCell"},
    {cell:"CommandTemplate",title:"Action",width:200,className:"center"}
] as GridColumnProps;

const MapGridOTData=()=>{
    GridOTData.data=OvertimeDraftData.value.map((item,index)=>{
        return {
            ...item,
            EmpDisplay:item.EMPLOYEE_ID + ' - ' + item.NAME,
            inEdit:InEditOTData.value==item.OT_NUMBER
        }
    })
}
const LoadOTDraftData=async ()=>{
    let request=new QueryOvertimeDraft({id:"", jsconfig:'DateHandler:ISO8601DateOnly'});
    let resultApi=await client.api(request);
    if(resultApi.completed){
        OvertimeDraftData.value=resultApi.response?.results??[];
        MapGridOTData();
    }
}
const onClaimEdit=(e:any)=>{
    UserInput.OT_HOUR=e.dataItem.OT_HOUR;
    UserInput.OT_REASON_CODE=e.dataItem.OT_REASON_CODE;
    InEditOTData.value=e.dataItem.OT_NUMBER;
    MapGridOTData();
}
let UserInput=reactive({
    OT_HOUR:0,
    OT_REASON_CODE:''
});
const otItemChange=(e:any)=>{
    UserInput[e.field]=e.value;
    e.dataItem[e.field]=e.value;
}
const onClaimSave= async (e:any)=>{
    let req=new UpdateClaimRequest({
        otHour:UserInput.OT_HOUR,
        otNumber:e.dataItem.OT_NUMBER,
        reasonCode:UserInput.OT_REASON_CODE
    });
    var apiResult=await client.api(req);
    if(apiResult.completed){
        InEditOTData.value=undefined;
        await LoadOTDraftData();
    }
}
const onClaimRemove=(e:any)=>{

}
const onClaimCancelChanges=(e:any)=>{
InEditOTData.value=undefined;
MapGridOTData();
}
let OTReasonData=ref<Lookup[]>([]);
const LoadReasonData=async()=>{
let request=new QueryLookups({
    lookuptype:LOOKUPTYPE.OT_REASON
});
let apiResult=await client.api(request);
if( apiResult.completed){
    OTReasonData.value=apiResult.response?.results??[];
}
}

const IsSubmitable=(data:any)=>{
    if(data.OT_HOUR>0 && data.OT_REASON_CODE)
        return true;
    return false;
}
const submit=async (data:any)=>{
    let req=new SubmitClaimRequest({
        otNumber:data.OT_NUMBER
    });
    let apires=await client.api(req);
    if(apires.completed){
        if(apires.response?.success){
            showNotifSuccess("Submit Overtime","Success");
            await LoadOTDraftData();
        }
    }
}
onMounted(async()=>{
await LoadEmployeeOptions();
await LoadOTDraftData();
await LoadReasonData();
})
</script>