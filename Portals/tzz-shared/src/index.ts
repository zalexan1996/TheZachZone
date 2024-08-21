// Components
import { createPinia, setActivePinia } from "pinia"
import Badge from "./components/Common/Badge.vue"
import InputText from "./components/Controls/InputText.vue"
import Toast from "./components/Toast/Toast.vue"
import ToastPanel from "./components/Toast/ToastPanel.vue"
import SiteSwitcherPanel from "./components/SiteSwitcher/SiteSwitcherPanel.vue"
import SiteSwitcherToggler from "./components/SiteSwitcher/SiteSwitcherToggler.vue"
import Checkbox from "./components/Controls/Checkbox.vue"

// Stores
import { useToastStore, ToastOptions } from "./stores/toastStore"
import { useSiteSwitcherStore } from "./stores/siteSwitcherStore"

setActivePinia(createPinia())

export {
    Badge,
    InputText,
    Toast,
    ToastPanel,
    Checkbox,
    
    useToastStore,
    ToastOptions,

    useSiteSwitcherStore,
    SiteSwitcherPanel,
    SiteSwitcherToggler
}