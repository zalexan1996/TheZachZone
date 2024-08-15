import { defineStore } from "pinia"
import { ref } from "vue"

export const useSiteSwitcherStore = defineStore('site-switcher', () => {

    const isSwitcherOpen = ref(false)

    const toggle = () => {
        isSwitcherOpen.value = !isSwitcherOpen.value
    }
    return {
        isSwitcherOpen,
        toggle
    }
})