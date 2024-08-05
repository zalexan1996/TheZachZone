<template>
    <div :class="toastContainerClasses">
        <h5>{{ props.options.title }}</h5>
        <hr class="w-100 mt-1 mb-3 text-white"/>
        <div>
            {{ props.options.body }}
        </div>
    </div>
</template>
<script setup lang="ts">
import { defineProps, computed } from 'vue'
import { ToastOptions } from '@/Stores/toastStore.ts'

interface IProps { 
    options: ToastOptions
}
const props = defineProps<IProps>()

const toastContainerClasses = computed(() => {
    return `toast-container d-flex flex-column justify-content-start align-items-start ${props.options.severity}`
})
</script>
<style scoped lang="scss">
@import '@/assets/styles/theme.scss';

.toast-container {
    padding: 1rem;
    border: solid 1px black;
    border-radius: 10px;
}

</style>