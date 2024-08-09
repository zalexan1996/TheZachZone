<template>
    <div :class="toastContainerClasses">
        <h5><span :class="`fa ${iconClass} me-2`"></span>{{ props.options.title }}</h5>
        <hr class="w-100 mt-1 mb-3 text-white"/>
        <div>
            {{ props.options.body }}
        </div>
    </div>
</template>
<script setup lang="ts">
import { defineProps, computed } from 'vue'
import { ToastOptions } from '@stores/toastStore'

interface IProps { 
    options: ToastOptions
}
const props = defineProps<IProps>()

const toastContainerClasses = computed(() => {
    return `toast d-flex flex-column justify-content-start align-items-start bg-dark border-${props.options.severity} text-${props.options.severity}`
})

const iconClass = computed(() => {
    switch (props.options.severity)
    {
        case 'primary': return 'fa-list';
        case 'secondary': return 'fa-book';
        case 'info': return 'fa-question';
        case 'success': return 'fa-check';
        case 'warning': return 'fa-triangle-exclamation';
        case 'danger': return 'fa-bolt';
    }
})
</script>

<style scoped lang="scss">
.toast {
    padding: 1rem;
}

</style>