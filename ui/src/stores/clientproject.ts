import { acceptHMRUpdate, defineStore } from "pinia"
import { client } from "@/api"
import { Client, ProjectView, QueryClients, QueryProjects } from "@/dtos"
import { process, filterBy } from '@progress/kendo-data-query'

export const useClientProjectStore = defineStore('clientprojects', () => {

    // State
    const sourceClientList = ref<Client[]>([])
    let clientList = ref<Client[]>([])
    let selectedClientId = ref<number | null>()

    const sourceProjectList = ref<ProjectView[]>([])
    let projectList = ref<ProjectView[]>([])
    let selectedProjectId = ref<number | null>()

    // Actions
    const refreshClientProjects = async (slectedRegion? : any) => {
        selectedClientId.value = null
        selectedProjectId.value = null
        getClientList()
    }
    const getClientList = async() => {
        const api = await client.api(new QueryClients({ }))
        if (api.succeeded) {
            sourceClientList.value = api.response!.results ?? []
            clientList.value = process(sourceClientList.value, {}).data as any[]
        }
    }

    const getProjectList = async() => {
        // console.log(selectedClient.value?.id)
        const api = await client.api(new QueryProjects({ 
            clientId: selectedClientId.value ?? 0,
        }))
        if (api.succeeded) {
            sourceProjectList.value = api.response!.results ?? []
            projectList.value = process(sourceProjectList.value, {}).data as any[]
        }
        // selectedProjectId.value = null
    }

    const onChangeClient = (e: any) => {
        selectedClientId.value = e.value
        selectedProjectId.value = null
        // selectedClientId.value = e.component.dataItems[e.component.index]
        getProjectList()
    }

    const onChangeProject = (e: any) => {
        selectedProjectId.value = null
        // selectedProject.value = e.component.dataItems[e.component.index]
        // getProjectList()
    }
    
    const onFilterClient = (e : any) => {
        const data = process(sourceClientList.value, {}).data as any[]
        clientList.value = filterBy(data, e.filter)
    }
    const onFilterProject = (e : any) => {
        const data = process(sourceProjectList.value, {}).data as any[]
        projectList.value = filterBy(data, e.filter)
    }
    

    return {
        selectedClientId,
        selectedProjectId,
        clientList,
        projectList,
        // emit,
        // defineEmits,
        refreshClientProjects,
        onChangeClient,
        onChangeProject,
        onFilterClient,
        onFilterProject
    }

})

if (import.meta.hot)
    import.meta.hot.accept(acceptHMRUpdate(useClientProjectStore, import.meta.hot))