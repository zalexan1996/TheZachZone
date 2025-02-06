<template>
    <div class="grid">
        <BigButton title="The Game Zone" icon="fa fa-gamepad" href="http://localhost:8010"/>
        <BigButton title="Crapazon" icon="fa fa-poo"/>
        <BigButton title="Agile Life" icon="fa fa-book"/>
        <BigButton title="Pocket Persona" icon="fa fa-p" href="http://localhost:8020"/>
        <BigButton title="The School Zone" icon="fa fa-school"/>
        <BigButton title="Planet Earth" icon="fa fa-earth-americas" href="http://localhost:8030"/>
        <RouterLink v-if="isLoggedIn" class="btn btn-primary" :to="{name: 'Account'}">View Account</RouterLink>
        <button v-if="isLoggedIn" class="btn btn-primary" @click="logout_Click">Logout</button>
    </div>
    <RouterLink v-if="!isLoggedIn" class="subtitle mb-4" :to="{name: 'Login'}">Login for the best experience.</RouterLink>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAccountStore } from '@stores/accountStore.ts'
import BigButton from '@components/BigButton.vue'

const accountStore = useAccountStore();
const isLoggedIn = ref(false)
const router = useRouter();

const logout_Click = async () => {
    await accountStore.logout();
    router.go(0)
}

onMounted(async () => {
    isLoggedIn.value = await accountStore.isLoggedIn();
})
</script>

<style scoped lang="scss">
.grid {
    display: grid;
    grid-template-columns: 50% 50%;
    grid-template-rows: 12rem 12rem 12rem;
    column-gap: 10px;
    row-gap: 10px;
    margin-bottom: 2rem;
}

hr {
    width: 100%;
    height: 3px;
    
    background: linear-gradient(to left, #6f00ff 0%,#b700ff 100%);
    margin-top: 0px;
    margin-bottom: 2rem;
}
.subtitle {
    color: #eab1f8;
}
</style>