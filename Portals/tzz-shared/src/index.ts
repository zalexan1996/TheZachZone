// Components
import { createPinia, setActivePinia } from "pinia"
import Badge from "./components/Common/Badge.vue"
import InputText from "./components/Controls/InputText.vue"
import Toast from "./components/Toast/Toast.vue"
import ToastPanel from "./components/Toast/ToastPanel.vue"

// Stores
import { useToastStore, ToastOptions } from "./stores/toastStore"

setActivePinia(createPinia())

export {
    Badge,
    InputText,
    Toast,
    ToastPanel,
    
    useToastStore,
    ToastOptions
}