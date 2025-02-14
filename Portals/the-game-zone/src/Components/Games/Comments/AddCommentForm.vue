<template>
    <div class="d-flex flex-column align-items-stretch">
        <InputText label="Content" v-model="content" class="mb-4"/>
        <button class="btn btn-primary align-self-center px-4" @click="addComment">Post</button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useGameStore } from '@stores/gameStore'
import { AddGameCommentCommand } from '@services/apiService'
import { InputText } from 'tzz-shared'

const gameStore = useGameStore();
const author = ref('')
const content = ref('')

interface IProps {
    gameInfoId: number|undefined
}

const props = defineProps<IProps>()

const addComment = async () => {
    await gameStore.addComment({
        gameInfoId: props.gameInfoId,
        content: content.value
    } as AddGameCommentCommand)

    await gameStore.loadComments(props.gameInfoId!);
    content.value = ''
}
</script>