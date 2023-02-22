import { acceptHMRUpdate, defineStore } from "pinia"
import { client } from "@/api"
import { Bank, QueryBanks } from "@/dtos"
import { process, filterBy } from '@progress/kendo-data-query'

export const useMasterBankStore = defineStore('masterbanks', () => {

    // State
    const sourceMasterBankList = ref<Bank[]>([])
    let masterBankList = ref<Bank[]>([])
    let selectedMasterBank = ref<Bank | undefined>()

    const getMasterBankList = async() => {
        const api = await client.api(new QueryBanks())
        if (api.succeeded) {
            sourceMasterBankList.value = api.response!.results ?? []
            masterBankList.value = process(sourceMasterBankList.value, {}).data as any[]
        }
    }

    const onChangeMasterBank = (e: any) => {
        selectedMasterBank.value = e.component.dataItems[e.component.index]
        console.log(selectedMasterBank.value)
    }

    const onFilterMasterBank  = (e : any) => {
        const data = process(sourceMasterBankList.value, {}).data as any[]
        masterBankList.value = filterBy(data, e.filter)
    }

    return {
        masterBankList,
        selectedMasterBank,
        getMasterBankList,
        onChangeMasterBank,
        onFilterMasterBank
    }
})

if (import.meta.hot)
    import.meta.hot.accept(acceptHMRUpdate(useMasterBankStore, import.meta.hot))