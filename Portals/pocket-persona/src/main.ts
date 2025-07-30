import { createApp } from 'vue'
import "@/assets/styles/style.scss"
import '/node_modules/bootstrap/dist/js/bootstrap.bundle.js'
import { CreatePPRouter } from './Services/routerService.ts'
import { createPinia } from 'pinia'
import App from '@views/App.vue'

const app = createApp(App)
    .use(CreatePPRouter())
    .use(createPinia())

// A directive applied to Inputs that allows me to perform an action on Enter in the input. Helpful when not using a form.
app.directive('onEnter', {
    mounted: (el: HTMLInputElement, binding) => {
        el.addEventListener('keyup', ev => {
            if (ev.key == 'Enter') {
                binding.value()
            }
        })
    }
})

app.mount('#app')
