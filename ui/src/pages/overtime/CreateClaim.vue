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
          <kGrid :columns="GritOTColumns"  :loader="loadingDraftData" :selected-field="'selected'" :data-items="gridOTData" :edit-field="'inEdit'" @itemchange="otItemChange">
              <template v-slot:otnumberCell="{props}">
                 
                  <div v-if="IsSubmitable(props.dataItem)">
                      <div><a href="#" @click="ShowOTDetail(props.dataItem)">{{props.dataItem.OT_NUMBER}}</a></div>
                      <div style="text-align:right">
                          <kbutton icon="redo"
                                   :title="Submit"
                                   :theme-color="'success'"
                                   style="width:30px; height:30px;"
                                   class="k-grid-cancel-command"
                                   @click="submit(props.dataItem)">

                          </kbutton>
                      </div>
                  </div>
                  <div v-else>
                      <a href="#" @click="ShowOTDetail(props.dataItem)">{{props.dataItem.OT_NUMBER}}</a>
                  </div>
              </template>
              <template v-slot:CommandTemplate="{props}">
                  <CommandCell :data-item="props.dataItem"
                               @edit="onClaimEdit"
                               @save="onClaimSave"
                               @remove="onClaimRemove"
                               @cancel="onClaimCancelChanges" />
               

              </template>
              <template v-slot:reasonCell="{props}">
                  <td v-if="!props.dataItem.inEdit" :class="props.className">{{ props.dataItem[props.field] }}</td>
                  <td v-else> <DropDownList :data-items="OTreasondata" :text-field="'lookupText'" :value="CustomClaimInput.OT_REASON_CODE"  @change="(e)=> reasonChange(e,props.dataItem)"  :data-item-key="'lookupValue'"  :value-field="'lookupValue'"
        :value-primitive="true" ></DropDownList></td>
                  
              </template>
          </kGrid>
          <div>{{CustomClaimInput}}</div>
        </BaseBlock>
    </div>

</template>

<script setup lang="ts">
    import { onMounted, ref, computed, reactive,defineEmits } from "vue";
    import { DatePicker } from '@progress/kendo-vue-dateinputs';
    import { Button as kbutton } from '@progress/kendo-vue-buttons';
    import { CreateClaimItemResponse, CreateOvertimeDraft, DeleteClaimRequest, EmployeeOption, EmployeePOCO, EmployeeQuery, EmployeeSelections, Lookup, LOOKUPTYPE, Overtime, QueryLookups, QueryOvertimeDraft, QueryResponse, UpdateClaimRequest } from '@/dtos';
    import { client } from "@/api";
    import { Grid as kGrid, GridToolbar as kGridToolbar, GridDataStateChangeEvent, GridColumnProps,GridSelectionChangeEvent, GridPageChangeEvent,GridFilterChangeEvent } from '@progress/kendo-vue-grid';
    import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-vue-dropdowns';
    import { DataResult, process, toODataString } from '@progress/kendo-data-query';
    import { showNotifError, showNotifSuccess } from '@/stores/commons';
    import CommandCell from '../../layouts/partials/KGridCommandCell.vue';
 
    
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
    const grdEmpFilterHandler = async (e: GridFilterChangeEvent) => {
        grdEmployeeDataState.filter = e.filter;
      await  LoadEmployeeOptions();
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
    let gridOTData = ref<DataResult>({ data: [] as any, total: 0 }).value
    let SelectedOt = ref<string[]>([]);
    const MapGridOTData = () => {
       
        gridOTData.data = OvertimeDraft.value.map((item, index) => {
            return {
                ...item,
                inEdit: selectedOtId.value == item.OT_NUMBER,
                selected: SelectedOt.value.indexOf(item.OT_NUMBER!) >= 0 ? true : false ,
                EmpDisplay: item.EMPLOYEE_ID + ' - ' + item.NAME
            }
        });
    }
    const LoadOvertimeDraft = async () => {
        loadingDraftData.value = true;
        let request = new QueryOvertimeDraft({ id: "", jsconfig:'DateHandler:ISO8601DateOnly'});
        var resultApi = await client.api(request);
        if (resultApi.completed) {
            OvertimeDraft.value = resultApi.response?.results ?? [];
            MapGridOTData();
            loadingDraftData.value = false;
        }
    }
    const ShowOTDetail = (data: any)=>{
        alert(data.OT_NUMBER);
    }
    const GritOTColumns = [
        { field: 'selected',  filterable: false },
        { field: "OT_NUMBER", title: 'OT Number', editable: false, cell:'otnumberCell' },
        { field: "EmpDisplay", title: "Employee",editable:false },
        { field: "OT_DATE", title: "OT Date", editable: false },
        { field: "OT_HOUR", title: 'OT Hour',editable:true,editor:'numeric' },
        { field: "OT_REASON", title: "Reason",  cell:'reasonCell'},
        { cell: 'CommandTemplate', filterable: false, title: 'Action', className: "center", width: 200 }


    ] as GridColumnProps[];
    const onClaimEdit = (e: any) => {
        //let index = gridOTData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
        //let updated = Object.assign({}, gridOTData.data[index], { inEdit: true });
        //gridOTData.data.splice(index, 1, updated);
        //if (selectedOtId.value != '' && selectedOtId.value != e.dataItem.OT_NUMBER) {
        //    let index = gridOTData.data.findIndex((p: { id: any; }) => p.id === selectedOtId.value);
        //    let updated = Object.assign({}, gridOTData.data[index]);
        //    gridOTData.data.splice(index, 1, updated);
        //}
        CustomClaimInput.OT_HOUR = e.dataItem.OT_HOUR;
        CustomClaimInput.OT_REASON_CODE = e.dataItem.OT_REASON_CODE;
        selectedOtId.value = e.dataItem.OT_NUMBER;
        MapGridOTData();
    }
    const onClaimSave =async (e: any) => {
        let req = new UpdateClaimRequest({
            otHour: CustomClaimInput.OT_HOUR,
            otNumber: e.dataItem.OT_NUMBER,
            reasonCode: CustomClaimInput.OT_REASON_CODE
        });
        var apiRequest = await client.api(req);
        if (apiRequest.completed) {
            showNotifSuccess("Claim Submit", "Success");
            selectedOtId.value = '';
           await LoadOvertimeDraft();
        }
    }
    const onClaimRemove = async (e: any) => {
        let req = new DeleteClaimRequest({ otNumber: e.dataItem.OT_NUMBER });
        let apiRes = await client.api(req);
        if (apiRes.completed) {
            if (apiRes.response?.success) {
                showNotifSuccess("Delete Draft Claim", `${e.dataItem.OT_NUMBER} deleted successfully`);
            } else {
                showNotifError("Delete Draft Claim", `${e.dataItem.OT_NUMBER} delete failed: ${apiRes.response?.errorMessage}`);
            }
            await LoadOvertimeDraft();
        }
    }
    const IsSubmitable = (data: any) => {
        if (data.OT_HOUR <= 0)
            return false;
        if (data.OT_REASON == null || data.OT_REASON == '')
            return false;
        return true;
    }
   
    let CustomClaimInput =reactive({
        OT_HOUR: 0,
        OT_REASON_CODE:''
    });
    const reasonChange = (e: any, data: Overtime) => {
     
        CustomClaimInput.OT_REASON_CODE = e.value;
    }
    let OTreasondata = ref<Lookup[]>([]);
    const fillOTReasonData = async () => {
        let request = new QueryLookups({
            lookuptype: LOOKUPTYPE.OT_REASON
        });
        var api = await client.api(request);
        if (api.completed) {
            OTreasondata.value = api.response?.results ?? [];
        }
    }
    let selectedOtId = ref<string>('');
    const otItemChange = (e: any) => {
       
        CustomClaimInput[e.field] = e.value;
        
        e.dataItem[e.field] = e.value;
        //if (e.dataItem.id) {
        //    let index = gridOTData.data.findIndex((p: { id: any; }) => p.id === e.dataItem.id);
        //    let updated = Object.assign({}, gridOTData.data[index], { [e.field]: e.value });
        //    gridOTData.data.splice(index, 1, updated);
        //} else {
        //    e.dataItem[e.field] = e.value;
        //}
    }
    
    const onClaimCancelChanges = (e: any) => {
        selectedOtId.value = undefined;
        MapGridOTData();
    }
    let loadingDraftData = ref<boolean>(false);
    const CreateDraft = async (e: Event) => {
        if (selectedDate.value && selectedEmpIds.value.length > 0) {
            loadingDraftData.value = true;
            var request = new CreateOvertimeDraft({
                otDate: DateDisplay.value,
                employeeIds:selectedEmpIds.value
            });

            var apiResp = await client.api(request);
           
            if (apiResp.completed) {
                apiResp.response?.items?.map((item: CreateClaimItemResponse, idx: number) => {
                    if (item.success) {
                        showNotifSuccess('Create Overtime', `Successfully added new overtime ${item.otNumber}`);
                       
                    }
                    else
                        showNotifError('Create Overtime', item.errorMessage);
                });
                await LoadOvertimeDraft();
            }
        }
    }
    onMounted(async () => {

       // selectedEmpIds.value = ["0000008444"];
        await LoadEmployeeOptions();
        await LoadOvertimeDraft();
        await fillOTReasonData();
    });

</script>

<style scoped>
</style>