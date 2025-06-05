<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { InputText } from 'tzz-shared';
import { useCharacterStore } from '@/Stores/characterStore';
import { useGameStore } from '@/Stores/gameStore'
import { useForm, useIsFormDirty, useIsFormValid } from 'vee-validate'
import { AddCharacterCommand } from '@/Services/pp.api.ts'
import * as yup from 'yup'

const characterStore = useCharacterStore();
const gameStore = useGameStore()

const existingCharacters = computed((): string[] => characterStore.characters.map(x => x.name))
const form = useForm({
    validationSchema: yup.object().shape({
        gameId: yup.string().required(),
        name: yup.string()
            .required()
            .test('not-existing', 'This character already exists', 
                v => !existingCharacters.value.includes(v))
    })
})

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

const [name] = form.defineField('name')
const [gameId] = form.defineField('gameId')

const gameOptions = computed(() => {
    return gameStore.games.map(x => x.name)
})

const addCharacter = async () => {
    
    await characterStore.addCharacter({
        name: name.value,
        gameId: gameStore.games.filter(x => x.name == gameId.value).map(x => x.id)[0]
    } as AddCharacterCommand)
    name.value = ''
    gameId.value = ''
    await characterStore.getCharacters();
}

onMounted(async () => {
    await gameStore.getGames()
})

</script>

<template>
    <div class="d-flex flex-flex-row align-items-end justify-content-start p-3">
        <InputText label="Name" type="text" v-model="name" :options="gameStore.games"
            placeholder="Provide a name for the character.." class="mx-3 mb-0" for="name"/>

        <InputText label="Game" type="select" v-model="gameId" :options="gameOptions"
            placeholder="Provide the game the character is in..." class="mx-3 mb-0" for="gameId"/>

        <button class="btn btn-danger" @click="addCharacter" :disabled="!(isFormValid && isFormDirty)">Add</button>
    </div>
</template>