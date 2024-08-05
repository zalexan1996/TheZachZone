import { createRouter, createWebHistory, RouteRecord, RouteRecordRaw, RouteRecordRedirect } from "vue-router"
import Home from '@views/Home.vue'
import Login from '@views/Login.vue'
import Account from '@views/Account.vue'
import { useAccountStore } from "../Stores/accountStore"

const routes: RouteRecordRaw[] = [
    {
        name: 'Home',
        path: '/',
        component: Home
    },
    {
        name: 'Login',
        path: '/login',
        component: Login
    },
    {
        name: 'Account',
        path: '/account',
        component: Account,
        beforeEnter: async (to, from) => {
            const accountStore = useAccountStore();
            if (!accountStore.isLoggedIn()) {
              await accountStore.loadUserInfo()
            }
        }
    }
]

export const useRouterService = () => {
    

    return createRouter({
        history: createWebHistory(),
        routes: routes,
    })
}
