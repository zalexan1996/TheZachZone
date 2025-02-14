<template>
    <div class="d-flex flex-column justify-content-start align-items-stretch w-100">
        <Comment class="my-2" v-for="comment in gameStore.comments" :commentDto="comment"/>
    </div>
    <AddCommentForm :gameInfoId="props.gameInfoId"/>
</template>

<script setup lang="ts">
import { watch, onMounted } from 'vue'
import { useGameStore } from '@/Stores/gameStore.ts'
import Comment from './Comment.vue'
import AddCommentForm from './AddCommentForm.vue'

const gameStore = useGameStore();

interface IProps {
    gameInfoId: number|undefined
}

const props = defineProps<IProps>()
watch(props, () => {
    if (!!props.gameInfoId) {
        gameStore.loadComments(props.gameInfoId);
    }
})

onMounted(() => {
    if (props.gameInfoId) {
        gameStore.loadComments(props.gameInfoId);
    }
})
</script>