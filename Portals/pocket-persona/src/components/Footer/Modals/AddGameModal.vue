<template>
    <Modal title="Add Game" 
        :is-visible="isVisible"
        :is-submit-disabled="!(isFormValid && isFormDirty)"
        @cancel="e => emits('cancel', e)"
        @submit="onSubmit">
        <div class="d-flex flex-column align-items-start justify-content-start">
            <InputText for="gameName" :disabled="false" label="Game name" placeholder="Provide a name for the game..." type="text" v-model="gameName"/>
            <InputText for="primaryColor" :disabled="false" label="Primary Color" placeholder="#RRGGBBAA" type="text" v-model="primaryColor"/>
            <InputText for="secondaryColor" :disabled="false" label="Secondary Color" placeholder="#RRGGBBAA" type="text" v-model="secondaryColor"/>
        </div>
    </Modal>
</template>
<script setup lang="ts">
import { computed, defineProps, ref, watch } from 'vue'
import { InputText, Modal } from 'tzz-shared';
import { useGameStore } from '@/Stores/gameStore';
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate';
import * as yup from 'yup'


/* Props Stuff */
interface IProps {
    isVisible: boolean
}
const props = withDefaults(defineProps<IProps>(), {
    isVisible: false,
})
const isVisible = ref<boolean>(false)
watch(() => props.isVisible, () => isVisible.value = props.isVisible)

/* Events */
const emits = defineEmits(['cancel', 'submit'])


const gameStore = useGameStore()
const addGameForm = useForm({
    validationSchema: {
        gameName: yup.string().required(),
        primaryColor: yup.string().required(),
        secondaryColor: yup.string().required(),
    }
})
const [gameName] = addGameForm.defineField('gameName')
const [primaryColor] = addGameForm.defineField('primaryColor')
const [secondaryColor] = addGameForm.defineField('secondaryColor')
const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()


const onSubmit = async (e: any) => {
    // Add the game
    await gameStore.addGame(gameName.value, primaryColor.value, secondaryColor.value)
    
    // Notify parent
    emits('submit', e)
}
</script>
