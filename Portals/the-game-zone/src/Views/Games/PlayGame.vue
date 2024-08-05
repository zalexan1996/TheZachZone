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
import { useToastStore } from '@/Stores/toastStore'

const route = useRoute();
const gameStore = useGameStore()
const toastStore = useToastStore()
const loadStatus = ref<boolean|null>(null)

onMounted(async () => {
    let id = Number.parseInt(route.params.id as string)
    let path = `/src/assets/games/${id}/index.ts`
    //let path = await gameStore.getHostedPathFromId(id)
    console.log(path)
    if (path != null) {
        import(path).then(m => {
            loadStatus.value = true
            console.log(m)
            m.Play()
        }).catch((e) => {
            loadStatus.value = false
            console.error(e)
            toastStore.push({
                title: 'Game failed to load.',
                body: e,
                duration: 3000,
                severity: 'danger'
            })
        })
    }
    else {
        loadStatus.value = false
        toastStore.push({
            title: 'Game failed to load.',
            body: 'Could not find game. Upload must have failed.',
            duration: 3000,
            severity: 'danger'
        })
    }

})
</script>