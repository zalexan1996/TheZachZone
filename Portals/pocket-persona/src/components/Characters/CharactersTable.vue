<template>
    <div v-if="characterStore.characters.length > 0">
        <table class="w-100">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Game</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="character in characterStore.characters" :key="character.id">
                    <td>{{ character.characterId }}</td>
                    <td class="w-100">{{ character.name }}</td>
                    <td class="w-100">{{ resolveGameName(character.gameId) }}</td>
                    <td>
                        <span class="fa fa-trash clickable" title="Delete" @click="removeCharacter(character.characterId)"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <span>No characters have been configured...</span>
    </div>
</template>
<script setup lang="ts">
import { onMounted } from 'vue';
import { useCharacterStore } from '@/Stores/characterStore';
import { useGameStore } from '@/Stores/gameStore';

const characterStore = useCharacterStore();
const gameStore = useGameStore();

const resolveGameName = (gameId: number) => {
    return gameStore.games.filter(g => g.id == gameId).map(x => x.name)[0]
}

const removeCharacter = async (id: number) => {
    await characterStore.removeCharacter(id)
    await characterStore.getCharacters()
}

onMounted(async () => {
    await characterStore.getCharacters();
    await gameStore.getGames();
})
</script>

<style scoped lang="scss">
</style>