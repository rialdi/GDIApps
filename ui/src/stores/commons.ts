import { useNotification } from "@kyvg/vue3-notification";
export const elNotification = useNotification()

export const showNotifError = (title?: string, message?: string) => {
    elNotification.notify({
        title: title ?? "Action" + " Error",
        text: message ?? "The action is error",
        type: 'danger',
        duration: 5000,
        speed: 1000
    })
}

export const showNotifSuccess = (title?: string, message?: string) => {
    elNotification.notify({
        title: title ?? "Action" + " Success" ,
        text: message ?? "The action is succeded",
        type: 'success',
        duration: 5000,
        speed: 1000
    })
}

export const showNotifInfo = (title?: string, message?: string) => {
    elNotification.notify({
        title: title ?? "Action" + " Info",
        text: message ?? "The action information",
        type: 'info',
        duration: 5000,
        speed: 1000
    })
}