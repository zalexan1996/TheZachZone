import HomeView from '@views/Home/Home.vue'
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router"

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
]
export const CreatePPRouter = () => createRouter({
    history: createWebHistory(),
    routes: routes
})