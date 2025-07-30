<template>
    <span :style="style">{{ game?.name }}</span>
</template>

<script setup lang="ts">
import { GameDto } from '@/Services/pp.api';
import { useGameStore } from '@/Stores/gameStore';
import { computed, onMounted, ref } from 'vue';

interface IProps {
    gameId: number
}

const props = defineProps<IProps>()
const game = ref<GameDto>();
const gameStore = useGameStore();

const style = computed(() => {
    return {
        color: game.value?.primaryColor ?? 'gray',
        border: 'solid 1px ' + (game.value?.primaryColor ?? 'gray'),
        borderRadius: '8px',
        padding: '4px 6px 4px 6px',
        opacity: '90%',
        filter: 'brightness(130%)',
        cursor: 'default'
    }
})

onMounted(() => {
    if (props.gameId) {
        game.value = gameStore.getGameDetails(props.gameId)
    }
})
</script>