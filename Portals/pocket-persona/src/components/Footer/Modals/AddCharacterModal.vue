<template>
    <Modal title="Add a Character" 
        :is-visible="isVisible"
        :is-submit-disabled="!(isFormValid && isFormDirty)"
        @cancel="e => emits('cancel', e)"
        @submit="onSubmit">
        <div class="d-flex flex-column align-items-start justify-content-start">
            <InputText for="characterName" :disabled="false" label="Character Name" placeholder="Provide a name for the character..." type="text" v-model="characterName"/>
            <InputText for="game" :disabled="false" label="Game"
                :options="gameStore.getGameOptions()"
                placeholder="Select the game this character is in..." type="select" v-model="game"/>
        </div>
    </Modal>
</template>
<script setup lang="ts">
import { defineProps, ref, watch } from 'vue'
import { InputText, Modal } from 'tzz-shared';
import { useGameStore } from '@/Stores/gameStore';
import { useCharacterStore } from '@/Stores/characterStore';
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate';
import * as yup from 'yup'
import { AddCharacterCommand } from '@/Services/pp.api';
interface IProps {
    isVisible: boolean
}

const gameStore = useGameStore()
const characterStore = useCharacterStore()

const emits = defineEmits(['cancel', 'submit'])
const props = withDefaults(defineProps<IProps>(), {
    isVisible: false,
})
const addCharacterForm = useForm({
    validationSchema: {
        characterName: yup.string().required(),
        game: yup.number().required(),
    }
})
const [characterName] = addCharacterForm.defineField('characterName')
const [game] = addCharacterForm.defineField('game')
const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

const isVisible = ref<boolean>(false)

watch(() => props.isVisible, () => isVisible.value = props.isVisible)

const onSubmit = async (e: any) => {
    // Add the game
    await characterStore.addCharacter({
        name: characterName.value,
        gameId: game.value
    } as AddCharacterCommand)
    
    // Notify parent
    emits('submit', e)
}
</script>
