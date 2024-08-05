<template>
    <span :class="classString">
        <slot/>
    </span>
</template>
<script setup lang="ts">
import {computed, defineProps} from 'vue'
interface IProps {
    severity: 'danger'|'warning'|'success'|'info'|'primary'|'secondary'
}
const props = defineProps<IProps>()

const classString = computed(() => {
    return `slot ${props.severity}`
})
</script>
<style scoped lang="scss">
.slot {
    font-weight: normal;
    font-size: 16px;
    padding: {
        left: 8px;
        right: 8px;
        top: 6px;
        bottom: 6px;
    }
    border: 1px solid black;
    border-radius: 8px;
    color: white;
}

.danger {
    background-color: red;
}

.secondary {
    background-color: #242c36;
}
.primary {
    background: #0e2a4d;
}

.warning {
    background: #9e5511;
}

.success {
    background: #119e18;
}

.info {
    background: #11829e;
}
</style>