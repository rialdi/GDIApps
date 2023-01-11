import { ref, computed } from "vue"
import { checkAuth, logout } from "./api"
import { AuthenticateResponse } from "./dtos"
import { Router } from "vue-router"
import {GetUserInfoDetail, AppUser} from "@/dtos"
import { client } from "@/api"

export function createAttrs(auth?: AuthenticateResponse) {
    return auth ? [
        'auth',
        ...(auth?.roles || []).map(role => `role:${role}`),
        ...(auth?.permissions || []).map(perm => `perm:${perm}`),
    ] : []
}

export const appUser = ref<AppUser>(new AppUser())
export const auth = ref<AuthenticateResponse|undefined>()
checkAuth().then(r => auth.value = r)

export async function revalidate() {
    loading.value = true
    signin(await checkAuth())
    loading.value = false
}

export const getAppUser = async (userName:string|undefined) => {
    const api = await client.api(new GetUserInfoDetail({ userNameOrEmail: userName}))
    if (api.succeeded) {
        appUser.value = api.response! ?? []
    }
}

export const loading = ref(false)

export const signedIn = () => auth.value !== undefined

export const attrs = computed(() => createAttrs(auth.value))

export const signin = (response?: AuthenticateResponse) => {
    auth.value = response
    getAppUser(auth.value?.userName)
    return auth.value
}

export const signout = async (router:Router, redirectTo?: string) => {
    auth.value = undefined
    await logout()
    // Always redirect to force re-running auth route guards
    await router.replace({ path: redirectTo ?? router?.currentRoute?.value.path, force: true })
}

export const hasRole = (role: string) => (auth?.value?.roles || []).indexOf(role) >= 0
export const hasPermission = (permission: string) => (auth?.value?.permissions || []).indexOf(permission) >= 0


