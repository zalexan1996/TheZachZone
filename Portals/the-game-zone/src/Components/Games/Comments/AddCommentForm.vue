<template>
    <div class="d-flex flex-column align-items-stretch">
        <InputText label="Author" v-model="author"/>
        <InputText label="Content" v-model="content" class="my-4"/>
        <button class="btn btn-primary" @click="addComment">Post</button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useGameStore } from '@/Stores/gameStore'
import { AddGameCommentCommand } from '@/Services/apiService'
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
        author: author.value,
        gameInfoId: props.gameInfoId,
        content: content.value
    } as AddGameCommentCommand)

    await gameStore.loadComments(props.gameInfoId!);
}
</script>