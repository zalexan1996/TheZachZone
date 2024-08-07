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
        component: Account
    }
]

export const useRouterService = () => {
    const router = createRouter({
        history: createWebHistory(),
        routes: routes,
    });
    
    router.beforeEach(async () => {
        const accountStore = useAccountStore();
        if (!accountStore.isLoggedIn()) {
          await accountStore.loadUserInfo()
        }
    })

    return router;
}
