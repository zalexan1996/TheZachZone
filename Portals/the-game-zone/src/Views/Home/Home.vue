<template>
    <h3>Home</h3>
    <button class="btn btn-success">test</button>
</template>
<script setup lang="ts">
import { onMounted } from 'vue'
import { useToastStore } from 'tzz-shared'
const toastStore = useToastStore()

onMounted(() => {
    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 3000,
        severity: 'primary'
    })

    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 4000,
        severity: 'secondary'
    })

    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 5000,
        severity: 'info'
    })

    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 6000,
        severity: 'success'
    })
    
    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 6000,
        severity: 'warning'
    })
    
    toastStore.push({
        title: 'Welcome back.',
        body: 'Welcome back to the game zone.',
        duration: 6000,
        severity: 'danger'
    })
})
</script>