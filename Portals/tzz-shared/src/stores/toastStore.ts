import { defineStore } from "pinia";
import { ref } from "vue";

export class ToastOptions {
    title: string = 'Title'
    body: string = 'Body'
    severity: 'danger'|'success'|'warning'|'info'|'primary'|'secondary' = 'success'
    duration: number = 3000
}

class Toast {
    constructor(options: ToastOptions, pushedOn: number) {
        this.options = options
        this.pushedOn = pushedOn
    }
    options!: ToastOptions
    pushedOn!: number
}
export const useToastStore = defineStore('toast', () => {
    const toasts = ref<Toast[]>([])
    const push = (options: ToastOptions) => {
        let toast = new Toast(options, Date.now())
        toasts.value.push(toast)
        setTimeout(() => {
            toasts.value.splice(toasts.value.indexOf(toast), 1)
        }, options.duration)
    }
    return {
        push,
        toasts
    }
})