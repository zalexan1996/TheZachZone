<script setup lang="ts">
import { computed, ref } from 'vue'
import { InputText } from 'tzz-shared';
import { useGameStore } from '@/Stores/gameStore';
import { useForm, useIsFormDirty, useIsFormValid } from 'vee-validate'
import * as yup from 'yup'

const gameStore = useGameStore();

const existingGames = computed((): string[] => gameStore.games.map(x => x.name))
const form = useForm({
    validationSchema: yup.object().shape({
        name: yup.string()
            .required()
            .test('not-existing', 'This game already exists', 
                v => !existingGames.value.includes(v))
    })
})

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

const [name] = form.defineField('name')
const addGame = async () => {
    console.log(existingGames.value)
    await gameStore.addGame(name.value)
    name.value = ''
    await gameStore.getGames();
}
</script>

<template>
    <div class="d-flex flex-flex-row align-items-end justify-content-start p-3">
        <InputText label="Name" type="text" v-model="name"
            placeholder="Provide a name for the game.." class="mx-3 mb-0" for="name"/>
        <button class="btn btn-danger" @click="addGame" :disabled="!(isFormValid && isFormDirty)">Add</button>
    </div>
</template>