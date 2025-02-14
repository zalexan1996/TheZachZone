<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container-fluid">
            <div class="row justify-content-between w-100">
                <Brand class="navbar-brand col-auto"/>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarContent">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse col-auto justify-content-end" id="navbarContent">
                    <ul class="navbar-nav align-items-stretch justify-content-center">
                        <li class="navbar-item">
                            <RouterLink active-class="active" :to="{name: 'Top-Games'}" class="nav-item">Top</RouterLink>
                        </li>
                        <li class="separator"></li>
                        <li class="navbar-item">
                            <RouterLink active-class="active" :to="{name: 'Recent-Games'}" class="nav-item">Recent Games</RouterLink>
                        </li>
                        <li class="separator"></li>
                        <li class="navbar-item">
                            <RouterLink active-class="active" :to="{name: 'Categories'}" class="nav-item">Categories</RouterLink>
                        </li>
                        <li class="separator"></li>
                        <li class="navbar-item" v-if="userInfo?.id">
                            <RouterLink active-class="active" :to="{name: 'Upload'}" class="nav-item">Upload</RouterLink>
                        </li>
                        <li class="separator"></li>
                        <li class="navbar-item" v-if="userInfo?.isAdmin">
                            <RouterLink active-class="active" :to="{name: 'Admin'}" class="nav-item">Admin</RouterLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </nav>
</template>
<script setup lang="ts">
import Brand from './Brand.vue'
import { RouterLink } from 'vue-router'
import { onMounted, ref } from 'vue'
import { useAccountStore } from '@stores/accountStore.ts'
import { GeneralInformationDto } from '@services/tzz.api.ts'

const accountStore = useAccountStore()
const userInfo = ref<GeneralInformationDto|undefined>()

onMounted(async () => {
    userInfo.value = await accountStore.getUserInfo()
})
</script>
<style scoped lang="scss">

.nav-item {
    color: rgb(165, 165, 165);
    
    text-decoration: none;
    padding: {
        top: 1rem;
        left: 2rem;
        right: 2rem;
        bottom: 1rem;
    };
}
.active {
    background-color: #242c36;
    color: white;
}
.separator {
    border-left: 1px solid rgb(80, 80, 80);
}

</style>