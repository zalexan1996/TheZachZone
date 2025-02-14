import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'

import './assets/styles/style.scss'
import { useRouterService } from './Services/RouterService'
const app = createApp(App)

app.use(createPinia()) 
app.use(useRouterService())
app.mount('#app')
