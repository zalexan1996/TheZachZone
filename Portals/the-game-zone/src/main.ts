import { createApp } from 'vue'
import "@/assets/styles/style.scss"
import 'bootstrap'
import { CreateTGZRouter } from './Services/routerService.ts'
import { createPinia } from 'pinia'
import App from '@views/App.vue'
import * as VueQuill from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css'
import BlotFormatter from 'quill-blot-formatter';

const app = createApp(App)
    .use(CreateTGZRouter())
    .use(createPinia())

app.component('QuillEditor', VueQuill.QuillEditor)

VueQuill.Quill.register('modules/blotFormatter', BlotFormatter)

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
