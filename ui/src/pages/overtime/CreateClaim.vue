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
                   :pageable="true"
                   :skip="grdEmployeeDataState.skip"
                   :take="grdEmployeeDataState.take"
                   :total="grdEmployeeDataState.total"
                   @pagechange="grdEmpPagingHandler"
                   :selected-field="employeeSelectedField"
                   @selectionchange="onGridEmployeeSelect"
                   :filterable="true"
                   :filter="grdEmployeeDataState.filter"
                   @filterchange="grdEmpFilterHandler"
                 
                   :columns="gridEmployeeColumns"
                   :loader="grdEmployeeDataState.loader">
                <template v-slot:filterSlotTemplate="{ props }">
                    <div class="k-filtercell">
                        <div class="k-filtercell-wrapper">
                            <input class="k-textboxt k-input"
                                   :value="props.value || ''"
                                   />
                            <div class="k-filtercell-operator">
                                <DropDownList  class="k-dropdown-operator" @change="custFilterOperatorChange"
                                              :icon-class-name="'k-i-filter k-icon'" :data-items="operators" :text-field="'text'"
                                              :popup-settings="{width: ''}">
                                </DropDownList>
                                <button class="props.value === null || props.value === '') || props.operator) ?
                                    'k-button k-button-icon k-clear-button-visible' :
                                    'k-button k-button-icon'"
                                        title="filter" type="button" @click="clear">
                                    <span class="k-icon k-i-filter-clear" />
                                </button>
                            </div>
                        </div>
                    </div>
                </template>

                <template v-slot:EmptyHeader="{props}">
                    <div></div>
                </template>
            </kGrid>
            <div>{{grdEmployeeDataState}}</div>
            <div>  <DatePicker :style="{width:'230px'}" @change="dateChange"></DatePicker><kbutton @click="CreateDraft">Create Draft</kbutton></div>
            <div>{{OvertimeDraft}}</div>
        </BaseBlock>
    </div>

</template>

<script setup lang="ts">
    import { onMounted, ref, computed, reactive } from "vue";
    import { DatePicker } from '@progress/kendo-vue-dateinputs';
    import { Button as kbutton } from '@progress/kendo-vue-buttons';
    import { CreateClaimItemResponse, CreateOvertimeDraft, EmployeeOption, EmployeePOCO, EmployeeQuery, EmployeeSelections, Overtime, QueryOvertimeDraft, QueryResponse } from '@/dtos';
    import { client } from "@/api";
    import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps,GridSelectionChangeEvent, GridPageChangeEvent,GridFilterChangeEvent } from '@progress/kendo-vue-grid';
    import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
    import { DataResult, process, toODataString } from '@progress/kendo-data-query';
    import { showNotifError, showNotifSuccess } from '@/stores/commons';

    //import { client } from "@/api"
    //import { CreateOvertimeDraft, CreateClaimItemResponse, CreateOvertimeResponse, EmployeeOption, EmployeeSelections, EmployeeSelectionsResponse } from "@/dtos"

    //let SelectedEmployees = ref<String[]>();
    let selectedDate = ref<Date>(new Date())
    let selectedEmpIds = ref<string[]>([]);
    let EmployeeOptions = ref<EmployeeOption[]>([]);


    const DateDisplay = computed(() => {
        var month = selectedDate.value.toLocaleString("en-US", { month: 'short' }).toUpperCase();
        var day = selectedDate.value.toLocaleString("en-US", { day: '2-digit' });
        var year = selectedDate.value.toLocaleString("en-US", { year: 'numeric' });
        return `${day}-${month}-${year}`;
    });
    const dateChange = (e: any) => {
        let dt = e;
        alert(dt.value);
        selectedDate.value = dt.value;
    }
    const gridEmployeeColumns = [
        { field: 'selected', headerCell: 'EmptyHeader' ,filterable:false},
        { field: 'EMPLOYEE_ID', title: 'Employee ID' },
        { field: 'NAME', title: 'Name' },
        { field: 'POS_DEPT', title: 'Department' }

    ] as GridColumnProps[];
    let gridEmployeeData = ref<DataResult>({ data: [] as any, total: 0 }).value;

    const employeeSelectedField = 'selected';
    const onGridEmployeeSelect = (e: GridSelectionChangeEvent) => {
        let idx = selectedEmpIds.value.indexOf(e.dataItem.EMPLOYEE_ID);
        if (idx >= 0) {
            selectedEmpIds.value.splice(idx);
        } else {
            selectedEmpIds.value.push(e.dataItem.EMPLOYEE_ID);
        }
    }
    const grdEmployeeDataState = reactive({
        skip: 0,
        take: 10,
        total: 100,
        loader: false,
        filter: {
            logic: 'and',
            filters:[]
        }
    });
    const grdEmpPagingHandler = (e: GridPageChangeEvent) => {
      
        grdEmployeeDataState.skip = e.page.skip;
        
        grdEmployeeDataState.take = e.page.take;
        LoadEmployeeOptions();


    }   
    const operators = [{ text: "Starts With", value: 'startswith' }, { text: "Is equal to", value: 'eq' }];
    const grdEmpFilterHandler = (e: GridFilterChangeEvent) => {
        grdEmployeeDataState.filter = e.filter;
      
    }
    const custFilterOperatorChange = (e: DropDownListChangeEvent) => {
        var value = e.value;
       
    }
    const filterRender = function (h, defaultRendering, props, change) {
       
        return defaultRendering;
    }
    const LoadEmployeeOptions = async () => {
        grdEmployeeDataState.loader = true;
        let strOdata = "";
        let req = new EmployeeSelections({ oDataParam: "$top=100" });

        if (grdEmployeeDataState.skip >= 0)
            strOdata = "$skip=" + grdEmployeeDataState.skip.toString();

        if (grdEmployeeDataState.take >= 0)
            strOdata += "&$top=" + grdEmployeeDataState.take.toString();
        if (grdEmployeeDataState.filter != null) {
            let strOdataFilter = grdEmployeeDataState.filter.filters.map((f: any, i: number) => {
                if (f.operator == 'startswith')
                    return `startsWith(${f.field},'${f.value}')`;
                if (f.operator == 'eq')
                    return `${f.field} eq '${f.value}'`;
            }).join(' and ');
            req.oDataParam = strOdata + '&$filter=' + strOdataFilter;
        } else {
            req.oDataParam = strOdata ;
        }
        
        var api = await client.api(req);
        if (api.succeeded)
            EmployeeOptions.value = api.response?.employees ?? [];
        gridEmployeeData.data = EmployeeOptions.value.map((item) => {

            return { ...item, selected: selectedEmpIds.value.indexOf(item.EMPLOYEE_ID!) >= 0 ? true : false }
        });
        grdEmployeeDataState.loader = false;
        //    process(EmployeeOptions.value, {}).data.map((item) => {
        //    return {...item,selected:false}
        //});

    }
    let OvertimeDraft = ref<Overtime[]>([]);
    let gridOTData = ref<DataResult>({ data: [] as any, total: 0 }).value;
    const LoadOvertimeDraft =async () => {
        let request = new QueryOvertimeDraft({id:""});
        var resultApi = await client.api(request);
        if (resultApi.completed) {
            OvertimeDraft.value = resultApi.response?.results ?? [];
            gridOTData.data = OvertimeDraft.value;
        }
    }
    const GritOTColumns = [
        { field: "OT_NUMBER", title: 'OT Number' },

    ] as GridColumnProps[];
    const CreateDraft = async (e: Event) => {
        if (selectedDate.value && selectedEmpIds.value.length > 0) {
            alert(DateDisplay.value);
            var request = new CreateOvertimeDraft({
                otDate: DateDisplay.value,
                employeeIds:selectedEmpIds.value
            });

            var apiResp = await client.api(request);
           
            if (apiResp.completed) {
                apiResp.response?.items?.map((item: CreateClaimItemResponse, idx: number) => {
                    if (item.success)
                        showNotifSuccess('Create Overtime', `Successfully added new overtime ${item.otNumber}`);
                    else
                        showNotifError('Create Overtime', item.errorMessage);
                });
            }
        }
    }
    onMounted(async () => {

        selectedEmpIds.value = ["0000008444"];
        await LoadEmployeeOptions();
        await LoadOvertimeDraft();
    });

</script>

<style scoped>
</style>