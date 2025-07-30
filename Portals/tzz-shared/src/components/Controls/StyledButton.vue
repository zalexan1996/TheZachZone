<template>
    <button :style="buttonStyle"
        @click="e => emits('click', e)">
        {{ model }}
    </button>
</template>

<script setup lang="ts">
import { computed, defineModel } from 'vue';
interface IProps {
    color: string,
    outline: boolean
}

const buttonStyle = computed(() => props.outline ? {
    color: props.color,
    backgroundColor: '#00000000',
    border: 'solid 1px ' + props.color
} : {
    color: '#eaeaea',
    backgroundColor: props.color,
    border: 'none'
})
const props = withDefaults(defineProps<IProps>(), {
    outline: false
})
const emits = defineEmits(['click'])
const model = defineModel<string>();
</script>

<style scoped lang="scss">
button {
    border-radius: 6px;
    &:hover {
        filter: brightness(75%);
    }
    &:active {
        filter: brightness((50%));
    }
}
</style>