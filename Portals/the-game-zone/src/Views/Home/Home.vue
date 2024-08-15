<template>
    <h3>Home</h3>
    <button class="btn btn-success">test</button>
</template>
<script setup lang="ts">
import { onMounted } from 'vue'
import { useToastStore } from 'tzz-shared'
import { useHomeStore } from '@/Stores/homeStore.ts'

const homeStore = useHomeStore()
const toastStore = useToastStore()

onMounted(async () => {
    let isLoggedIn = await homeStore.isLoggedIn();

    if (isLoggedIn) {
        toastStore.push({
            title: 'Welcome back.',
            body: 'Welcome back to the game zone.',
            duration: 3000,
            severity: 'primary'
        })
    }
})
</script>