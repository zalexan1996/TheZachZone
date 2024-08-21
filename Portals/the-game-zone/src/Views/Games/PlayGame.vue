<template>
    <iframe class="w-100 h-100" :src="gameUrl"
    allow="autoplay; fullscreen *; cross-origin-isolated; transparency"
    scrolling="no" sandbox="allow-scripts allow-same-origin allow-pointer-lock"></iframe>
    <span v-if="loadStatus == false" class="text-danger justify-content-center d-flex mt-3">The game failed to load. This is probably due to an upload failure.</span>
</template>

<style scoped lang="scss">
canvas {
    width: 100%;
    height: 100%;
}
</style>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useGameStore } from '@/Stores/gameStore'
import { useToastStore } from 'tzz-shared'

const route = useRoute();
const gameStore = useGameStore()
const toastStore = useToastStore()
const loadStatus = ref<boolean|null>(null)

const gameId = computed(() => route.params.id)
const gameUrl = computed(() => {
    return `https://localhost:8011/games/${gameId.value}/index.html`
})

onMounted(async () => {
    loadStatus.value = false
    
})
</script>