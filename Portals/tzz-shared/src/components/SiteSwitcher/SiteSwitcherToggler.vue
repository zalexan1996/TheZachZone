<template>
    <button class="btn btn-outline-secondary" :title="title" @click="siteSwitcherStore.toggle()">
        <i :class="icon"></i>
    </button>
</template>
<style scoped lang="scss">

</style>
<script setup lang="ts">
import { useSiteSwitcherStore } from '@stores/siteSwitcherStore.ts'
import { computed } from 'vue'

const title = computed(() => siteSwitcherStore.isSwitcherOpen ? 'Hide other sites' : 'Show other sites')
const icon = computed(() => siteSwitcherStore.isSwitcherOpen ? 'fa fa-right-long' : 'fa fa-bars')
const siteSwitcherStore = useSiteSwitcherStore();
</script>