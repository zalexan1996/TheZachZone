<template>
        <a :href="href">
            <button class="w-100 h-100">
                <span :class="iconClasses"></span>
                <p>{{ props.title }}</p>
                <span :hidden="true">Description</span>
            </button>
        </a>
</template>

<script setup lang="ts">
import { defineProps, computed } from 'vue'

interface IProps {
    href: string,
    title: string,
    icon: string
}
const props = defineProps<IProps>()

const iconClasses = computed(() => `mb-4 ${props.icon}`)
</script>

<style scoped lang="scss">
button {
    text-align: center;
    background-color: #1d2629;
    border: solid 1px #373f42;
    border-radius: 6px;
    color: #eab1f8;

    &:hover {
        background: linear-gradient(to top right, #25023dcc 0%, #3c0166cc 40%, #510a85ff 100%);
        // background-color: #392e41;
    }

    p {
        text-align: center;
        display: block;
        font-weight: bold;
    }

    span.fa {
        font-size: 48px;
    }
}


</style>