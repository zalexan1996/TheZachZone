import { createApp } from 'vue'
import "@/assets/styles/style.scss"
import 'bootstrap'
import { CreateTGZRouter } from './Services/routerService.ts'
import { createPinia } from 'pinia'
import App from '@views/App.vue'

const app = createApp(App)
    .use(CreateTGZRouter())
    .use(createPinia())

app.mount('#app')
