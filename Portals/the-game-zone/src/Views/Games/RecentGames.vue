<template>
    <div class="p-4">
        <h2 class="mb-4">Recent Games</h2>
        <div class="container-fluid">
            <div class="row">
                <GameTile v-if="gameStore.games.length > 0" class="col-6 mb-2" v-for="game in gameStore.games" :gameInfo="game"/>
                <span v-else>There are no games yet.</span>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import GameTile from '@/Components/GameTile.vue'
import {useGameStore} from '@/Stores/gameStore'
import {onMounted} from 'vue'
const gameStore = useGameStore()

onMounted(async () => {
    await gameStore.loadGames()
})
</script>