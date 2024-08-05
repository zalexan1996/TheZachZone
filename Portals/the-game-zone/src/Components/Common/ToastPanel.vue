<template>
    <div class="toast-container">
        <div class="d-flex flex-column">
            <Toast v-for="toast in toastStore.toasts" :options="toast.options"/>
        </div>
    </div>
</template>
<script setup lang="ts">
import { useToastStore } from '@/Stores/toastStore'
import Toast from './Toast.vue'

const toastStore = useToastStore();


</script>

<style scoped lang="scss">
.toast-container {
    position: fixed;
    right: 0px;
    bottom: 0px;
    max-width: 25vw;
}
</style>