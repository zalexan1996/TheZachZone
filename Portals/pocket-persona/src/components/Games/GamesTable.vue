<template>
    <div v-if="gameStore.games.length > 0">
        <table class="w-100">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="game in gameStore.games" :key="game.id">
                    <td>{{ game.id }}</td>
                    <td class="w-100">{{ game.name }}</td>
                    <td>
                        <span class="fa fa-trash clickable" title="Delete" @click="removeGame(game.id)"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <span>No games have been configured...</span>
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useGameStore } from '../../Stores/gameStore';

const gameStore = useGameStore();

const removeGame = async (id: number) => {
    await gameStore.removeGame(id)
    await gameStore.getGames()
}
onMounted(async () => {
    await gameStore.getGames();
})
</script>

<style scoped lang="scss">
</style>