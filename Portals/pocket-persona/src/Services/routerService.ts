import HomeView from '@views/Home/HomeView.vue'
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router"
import GamesView from '../Views/Home/GamesView.vue'
import ArcanaView from '../Views/Home/ArcanaView.vue'
import CharactersView from '../Views/Home/CharactersView.vue'
import SocialLinksView from '../Views/Home/SocialLinksView.vue'

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/games',
        name: 'Games',
        component: GamesView
    },
    {
        path: '/arcana',
        name: 'Arcana',
        component: ArcanaView
    },
    {
        path: '/characters',
        name: 'Characters',
        component: CharactersView
    },
    {
        path: '/socialLinks',
        name: 'SocialLinks',
        component: SocialLinksView
    },
]
export const CreatePPRouter = () => createRouter({
    history: createWebHistory(),
    routes: routes
})