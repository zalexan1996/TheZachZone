<template>
    <div class="d-flex flex-row justify-content-start align-items-center">
        <img placeholder="/assets/controller.png" src="/src/assets/controller.png" width="72px"/>
        <div class="d-flex flex-column ">
            <RouterLink class="brand" :to="{name: 'Home'}">The Game Zone</RouterLink>
            <span class="welcome" v-if="loggedIn">Welcome back.</span>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import {RouterLink} from 'vue-router';
import { useHomeStore } from '@/Stores/homeStore.ts'
const loggedIn = ref(false)
const homeStore = useHomeStore()
onMounted(async () => {
    loggedIn.value = await homeStore.isLoggedIn();
})
</script>
<style scoped lang="scss">
.brand {
    font-size: 24px;
    color: white;
    text-decoration: none;
    margin-left: 2rem;
}

.welcome {
    margin-top: 0px;
    margin-bottom: 0px;
    margin-left: 2rem;
    font-size: 14px;
    font-style: italic;
}
</style>