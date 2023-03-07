import { acceptHMRUpdate, defineStore } from "pinia"
import { client } from "@/api"
import { AppMenu, GetAppMenuByRole } from "@/dtos"
import { process } from '@progress/kendo-data-query'

export const useAppMenuStore = defineStore('appmenus', () => {

    // State
    const sourceAppMenuList = ref<AppMenu[]>([])
    let appMenuList = ref<AppMenu[]>([])
    let isReady = ref<boolean>(false)
    
    const getAppMenuList = async(selectedRoleName : string) => {
        // console.log(selectedRoleName)
        const api = await client.api(new GetAppMenuByRole( {roleName: selectedRoleName}))
        if (api.succeeded) {
            sourceAppMenuList.value = api.response! as AppMenu[]?? []
            appMenuList.value = process(sourceAppMenuList.value, {}).data as any[]
        }
    }

    return {
        isReady,
        appMenuList,
        getAppMenuList
    }
})

if (import.meta.hot)
    import.meta.hot.accept(acceptHMRUpdate(useAppMenuStore, import.meta.hot))