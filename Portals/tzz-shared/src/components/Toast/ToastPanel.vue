<template>
    <div class="t-container">
        <div class="d-flex flex-column">
            <Toast v-for="toast in toastStore.toasts" :options="toast.options" class="my-1"/>
        </div>
    </div>
</template>
<script setup lang="ts">
import { useToastStore } from '@stores/toastStore'
import Toast from './Toast.vue'

const toastStore = useToastStore();


</script>

<style scoped lang="scss">
.t-container {
    position: fixed;
    right: 1rem;
    bottom: 4rem;
    max-width: 25vw;
}
</style>