<template>
    <div class="d-flex flex-column justify-content-start align-items-start">
        <h2>
            {{ game?.name }}
            <FavoriteToggle :entity-id="gameId!" entity-type="Game"/>
        </h2>
        <h5 class="text-secondary">Game</h5>
    </div>
</template>

<script setup lang="ts">
import FavoriteToggle from '@/components/Favorite/FavoriteToggle.vue';
import { GameDto } from '@/Services/pp.api';
import { useGameStore } from '@/Stores/gameStore';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const gameId = ref<number>(Number.parseInt(route.params['id']!.toString()))
const game = ref<GameDto|undefined>()

const gameStore = useGameStore();

onMounted(() => {
    game.value = gameStore.getGameDetails(gameId.value)
})
</script>

<style scoped lang="scss">

</style>