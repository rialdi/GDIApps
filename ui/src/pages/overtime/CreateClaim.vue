<template>
    <BasePageHeading title="Create Overtime Claim"
                     subtitle="Create Draft Overtime">
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
        <BaseBlock>
          
           
            <div>selected date: {{DateDisplay}}</div>
            <div>selected employee: {{selectedEmpIds}}</div>
            <kGrid ref="grid"
                   :style="{height: '440px'}"
                   :data-items="gridEmployeeData"
                    
                 
                   :columns="gridEmployeeColumns"></kGrid>
                <div>  <DatePicker :style="{width:'230px'}" @change="dateChange"></DatePicker><kbutton @click="CreateDraft">Create Draft</kbutton></div>
                <div>{{EmployeeOptions}}</div>
</BaseBlock>
    </div>
  
</template>

<script setup  lang="ts">
    import { onMounted, ref ,computed} from "vue";
    import { DatePicker } from '@progress/kendo-vue-dateinputs';
    import { Button as kbutton } from '@progress/kendo-vue-buttons';
    import { EmployeeOption, EmployeePOCO,EmployeeQuery,EmployeeSelections,Overtime, QueryResponse } from '@/dtos';
    import { client } from "@/api";
    import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps } from '@progress/kendo-vue-grid';
    import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
    import { DataResult, process } from '@progress/kendo-data-query'
    //import { client } from "@/api"
    //import { CreateOvertimeDraft, CreateClaimItemResponse, CreateOvertimeResponse, EmployeeOption, EmployeeSelections, EmployeeSelectionsResponse } from "@/dtos"

    //let SelectedEmployees = ref<String[]>();
    let selectedDate= ref<Date>(new Date())
    let selectedEmpIds = ref<string[]>([]);
    let EmployeeOptions = ref<EmployeeOption[]>([]);

  
    const DateDisplay = computed(() => selectedDate.value.toLocaleDateString());
    const dateChange = (e: any) => {
        let dt = e;
        alert(dt.value);
        selectedDate.value = dt.value;
    }
    const gridEmployeeColumns = [
        { field: 'EMPLOYEE_ID', title: 'Employee ID' },
        { field: 'NAME', title: 'Name' },
        {field:'POS_DEPT',title:'Department'}

    ] as GridColumnProps[];
    let gridEmployeeData = ref<DataResult>({ data: [] as any, total: 0 }).value;
    const LoadEmployeeOptions = async () => {
        let req = new EmployeeSelections({ oDataParam:"$top=100" });
        var api = await client.api(req);
        if (api.succeeded) 
            EmployeeOptions.value = api.response?.employees??[];
        gridEmployeeData.data =process(EmployeeOptions.value, {}).data;
    
    }

    const CreateDraft = (e: Event) => {
        alert('Draft: ' + selectedDate.value.toDateString() + ', ' + selectedEmpIds.value.toString());
    }
    onMounted( async () => {
        await LoadEmployeeOptions();
        selectedEmpIds.value = ["0000007142"];
       
    });
   
</script>

<style scoped>
</style>