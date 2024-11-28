import GameCategories from '@/Views/Games/GameCategories.vue'
import GameInfo from '@/Views/Games/GameInfo.vue'
import PlayGame from '@/Views/Games/PlayGame.vue'
import RecentGames from '@/Views/Games/RecentGames.vue'
import TopGames from '@/Views/Games/TopGames.vue'
import HomeView from '@views/Home/Home.vue'
import UploadGame from '@/Views/Games/UploadGame.vue'
import AdminMetadata from '@views/Admin/AdminMetadata.vue'
import AdminUserManagement from '@views/Admin/AdminUserManagement.vue'
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router"
import AdminHome from '@/Views/Admin/AdminHome.vue'
import ReviewGame from '@views/Games/ReviewGame.vue'

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/games/top',
        name: 'Top-Games',
        component: TopGames
    },
    {
        path: '/games/recent',
        name: 'Recent-Games',
        component: RecentGames
    },
    {
        path: '/games/categories',
        name: 'Categories',
        component: GameCategories
    },
    {
        path: '/games/:id',
        name: 'GameInfo',
        component: GameInfo,
    },
    {
        path: '/games/:id/play',
        name: 'Play-Game',
        component: PlayGame
    },
    {
        path: '/games/:id/review',
        name: 'Review-Game',
        component: ReviewGame
    },
    {
        path: '/upload',
        name: 'Upload',
        component: UploadGame
    },
    {
        path: '/admin',
        name: 'Admin',
        component: AdminHome,
        children: [
            {
                path: 'user-management',
                name: 'Admin/UserManagement',
                component: AdminUserManagement
            },
            {
                path: 'metadata',
                name: 'Admin/Metadata',
                component: AdminMetadata
            }
        ]
    }
]
export const CreateTGZRouter = () => createRouter({
    history: createWebHistory(),
    routes: routes
})