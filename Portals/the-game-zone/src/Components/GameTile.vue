<template>
<div class="card-body">
    <div class="card-header">
        <div class="d-flex flex-row justify-content-start align-items-center">
            <h3 class="me-4">
                <RouterLink :to="{name: 'GameInfo', params: { id: $props.gameInfo.id }}">
                    {{ props.gameInfo.name }}
                </RouterLink>
            </h3>
            <Badge class="mx-1" v-for="category in props.gameInfo.categories" severity="secondary">{{category}}</Badge>
        </div>
        <span>{{ props.gameInfo.uploadDate?.toLocaleDateString() }}</span>
    </div>
    <div>
        {{ props.gameInfo.description }}
    </div>
</div>
</template>
<script setup lang="ts">
import { GameInfoDto } from '@/Services/apiService'
import { Badge } from 'tzz-shared'

interface IProps {
    gameInfo: GameInfoDto
}

const props = defineProps<IProps>()
</script>
<style scoped lang="scss">
.card-body {
    padding: 1rem;
    border: solid 1px black;
    border-radius: 8px;
}
.card-header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
</style>