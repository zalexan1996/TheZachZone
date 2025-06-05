<template>
    <div class="d-flex flex-row justify-content-end align-items-end">
        <InputText label="Choose a game to manage" type="select" v-model="selectedGameId" :options="games"
            placeholder="Provide the game the character is in..." class="mx-3 mb-0 d-flex" for="gameId"/>
        <InputText label="Choose a Arcana to assign to" type="select" v-model="selectedArcanaId" :options="arcanas"
            placeholder="Choose the Arcana..." class="mx-3 mb-0 d-flex" for="arcanaId"/>

        <InputText :disabled="!selectedGameId" label="Choose a character to assign to this arcana" type="select" v-model="selectedCharacterId" :options="characters"
            placeholder="Choose the character..." class="mx-3 mb-0 d-flex" for="characterId"/>
        <button class="btn btn-success mt-4 d-flex">Assign</button>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue'
import { useGameStore } from '@stores/gameStore'
import { useCharacterStore } from '@stores/characterStore'
import { useArcanaStore } from '@stores/arcanaStore'
import { InputText } from 'tzz-shared';

const gameStore = useGameStore()
const characterStore = useCharacterStore();
const arcanaStore = useArcanaStore();
const games = ref<[number, string][]>()
const characters = ref<[number, string][]>()
const arcanas = ref<[number, string][]>()

const selectedGameId = ref<string>()
const selectedCharacterId = ref<string>()
const selectedArcanaId = ref<string>()

watch(() => selectedGameId.value, async () => {
    characters.value = (await characterStore.getCharacters())
        .filter(x => x.gameId == selectedGameId.value)
        .map(x => [x.characterId, x.name])

})

onMounted(async () => {
    games.value = (await gameStore.getGames()).map(x => [x.id, x.name])
    arcanas.value = (await arcanaStore.getArcana())
        .map(x => [x.id, x.name])
})

</script>