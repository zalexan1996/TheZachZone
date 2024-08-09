<template>
    <span class="justify-content-center d-flex mt-3" v-if="loadStatus == null">Loading...</span>
    <canvas v-if="loadStatus == null || loadStatus == true" id="game-canvas"></canvas>
    <span v-if="loadStatus == false" class="text-danger justify-content-center d-flex mt-3">The game failed to load. This is probably due to an upload failure.</span>
</template>

<style scoped lang="scss">
canvas {
    width: 100%;
    height: 100%;
}
</style>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useGameStore } from '@/Stores/gameStore'
import { useToastStore } from 'tzz-shared'

const route = useRoute();
const gameStore = useGameStore()
const toastStore = useToastStore()
const loadStatus = ref<boolean|null>(null)

onMounted(async () => {
    loadStatus.value = false
    toastStore.push({
        title: 'Game failed to load.',
        body: 'Play game is not implemented yet.',
        duration: 3000,
        severity: 'danger'
    })
})
</script>