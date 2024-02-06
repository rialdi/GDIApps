<script setup lang="ts">
import { toDate } from "@servicestack/client"
import { formatDate } from "@/utils"
import { DailyScrumMeeting } from "@/dtos"
import KDateTimePicker from "@/components/kendo/KDateTimePicker.vue"
import { Editor } from "@progress/kendo-vue-editor";

const blockersRef = ref<InstanceType<typeof Editor>>()
const wdydyesterdayRef = ref<InstanceType<typeof Editor>>()
const wgtodayRef = ref<InstanceType<typeof Editor>>()

const kEditorTools = [
    ["Bold", "Italic", "Underline", "Strikethrough"],
    ["Subscript", "Superscript"],
    ["AlignLeft", "AlignCenter", "AlignRight", "AlignJustify"],
    ["Indent", "Outdent"],
    ["OrderedList", "UnorderedList"],
    "FontSize",
    "FontName",
    "FormatBlock",
    ["Undo", "Redo"],
    ["Link", "Unlink", "InsertImage", "ViewHtml"],
    ["InsertTable"],
    ["AddRowBefore", "AddRowAfter", "AddColumnBefore", "AddColumnAfter"],
    ["DeleteRow", "DeleteColumn", "DeleteTable"],
    ["MergeCells", "SplitCell"],
]
const blockersHtml = ref<string | undefined> ()
const wdydyesterdayHtml = ref<string | undefined> ()
const wgtodayHtml = ref<string | undefined> ()

let meetingTime = ref<Date | undefined>(undefined)


let dataItemInEdit = ref<DailyScrumMeeting>(new DailyScrumMeeting())

let props = defineProps<{
    dataItem: any,
}>()

const emit = defineEmits<{
    (e:'save', dataItem: object): () => void
}>()

onMounted(async () => {
    // masterBankStore.getMasterBankList()
    resetForm()
});

const resetForm = async () => {
    dataItemInEdit.value = Object.assign({}, props.dataItem as DailyScrumMeeting)
    meetingTime.value = toDate(dataItemInEdit.value.meetingDate)
    blockersRef.value?.setHTML(dataItemInEdit.value.blockers)
    wdydyesterdayRef.value?.setHTML(dataItemInEdit.value.whatDidYouDoYesterday)
    wgtodayRef.value?.setHTML(dataItemInEdit.value.whatGoalsToday)
}

const onSubmit = async (e: Event) => {
    dataItemInEdit.value.meetingDate = formatDate(meetingTime.value?.toString(), "DD-MMM-yyyy HH:mm")
    dataItemInEdit.value.blockers = blockersRef.value?.getHTML()
    dataItemInEdit.value.whatDidYouDoYesterday = wdydyesterdayRef.value?.getHTML()
    dataItemInEdit.value.whatGoalsToday = wgtodayRef.value?.getHTML()
    emit('save', { dataItem : dataItemInEdit.value })
}

defineExpose({
    resetForm
})
</script>

<template>
    <form @submit.prevent="onSubmit" id="mainForm" class="k-form">
    <fieldset>
    <!-- <div class="row">
        <div class="col-12">     -->
            <BaseBlock>
                <div class="row">
                    <KDateTimePicker id="meetingDate" v-model="meetingTime" :valid="true"
                        :showLabel="true" :label="'Meeting Time'" :label-position="'left'" :format="'dd-MMM-yyyy HH:mm'"/>
                </div>

                <template #content>
                    <ul class="nav nav-tabs nav-tabs-block" role="tablist">
                        <li class="nav-item">
                            <button
                                class="nav-link active"
                                id="btabs-static-blockers-tab"
                                data-bs-toggle="tab"
                                data-bs-target="#btabs-static-blockers"
                                role="tab"
                                aria-controls="btabs-static-blockers"
                                aria-selected="true"
                                type="button"
                            >
                            Blockers
                            </button>
                        </li>
                        <li class="nav-item">
                            <button
                                class="nav-link"
                                id="btabs-static-wdydyesterday-tab"
                                data-bs-toggle="tab"
                                data-bs-target="#btabs-static-wdydyesterday"
                                role="tab"
                                aria-controls="btabs-static-wdydyesterday"
                                aria-selected="false"
                                type="button"
                            >
                            What did you do yesterday?
                            </button>
                        </li>
                        <li class="nav-item">
                            <button
                                class="nav-link"
                                id="btabs-static-wgtoday-tab"
                                data-bs-toggle="tab"
                                data-bs-target="#btabs-static-wgtoday"
                                role="tab"
                                aria-controls="btabs-static-wgtoday"
                                aria-selected="false"
                                type="button"
                            >
                            What goals today?
                            </button>
                        </li>
                    </ul>
                    <div class="block-content tab-content">
                        <div
                            class="block-content tab-pane active"
                            id="btabs-static-blockers"
                            role="tabpanel"
                            aria-labelledby="btabs-static-blockers-tab"
                            tabindex="1"
                        >
                            <Editor
                                :tools="kEditorTools"
                                :content-style="{ height: '300px'}"
                                :default-content="blockersHtml"
                                ref="blockersRef"
                                />
                        </div>
                        <div
                            class="block-content tab-pane"
                            id="btabs-static-wdydyesterday"
                            role="tabpanel"
                            aria-labelledby="btabs-static-wdydyesterday-tab"
                            tabindex="1"
                        >
                            <Editor
                                :tools="kEditorTools"
                                :content-style="{ height: '300px'}"
                                :default-content="wdydyesterdayHtml"
                                ref="wdydyesterdayRef"
                                />
                        </div>
                        <div
                            class="block-content tab-pane"
                            id="btabs-static-wgtoday"
                            role="tabpanel"
                            aria-labelledby="btabs-static-wgtoday-tab"
                            tabindex="2"
                        >
                            <Editor
                                :tools="kEditorTools"
                                :content-style="{ height: '300px'}"
                                :default-content="wgtodayHtml"
                                ref="wgtodayRef"
                                />
                        </div>
                    </div>
                </template>
            </BaseBlock>
        <!-- </div>
    </div> -->
    </fieldset>
    </form>
</template>