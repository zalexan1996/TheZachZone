import { useGlobalStore } from '@/Stores/globalStore'
import ArcanaProfile from '@/Views/Arcana/ArcanaProfile.vue'
import CharacterProfile from '@/Views/Character/CharacterProfile.vue'
import GameProfile from '@/Views/Game/GameProfile.vue'
import HomeView from '@views/Home/HomeView.vue'
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router"
import { EntityType } from './entityService'


const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/game/:id(\\d+)',
        name: 'Game',
        component: GameProfile
    },
    {
        path: '/character/:id(\\d+)',
        name: 'Character',
        component: CharacterProfile
    },
    {
        path: '/arcana/:id(\\d+)',
        name: 'Arcana',
        component: ArcanaProfile
    }
]
export const CreatePPRouter = () => {

    // Create the router
    let router = createRouter({
        history: createWebHistory(),
        routes: routes
    })
    
    // Add router guards to fetch all store data.
    router.beforeEach(async (_to, _from, next) => {
        const globalStore = useGlobalStore()
        await globalStore.load()
        next()
    })

    return router;
}



export const getRouteNameByEntityType = (entityType: EntityType) => {
    switch (entityType.toLowerCase())
    {
        case 'character':
            return 'Character'
        case 'game':
            return 'Game'
        case 'arcana':
            return 'Arcana'
    }
}
