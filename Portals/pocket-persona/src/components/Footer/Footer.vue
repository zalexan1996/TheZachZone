<template>
    <div id="footer">
        <div class="d-flex flex-row justify-content-between align-items-center px-3">
            <div class="d-flex flex-row w-100">
                <div class="btn-group">
                    <StyledButton v-for="game of gameStore.games" :key="game.id" :color="game.primaryColor!" :outline="game.id != gameStore.activeGame"
                        class="btn" v-model="game.name" @click="() => gameStore.setActiveGame(game.id!)">
                    </StyledButton>
                </div>
            </div>
            <span class="my-2 w-100">Made by Zach</span>
            <div class="w-100 d-flex flex-row justify-content-end">
                <CreateMenu/>
                <SiteSwitcherToggler style="right: 0px;"/>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
#footer {
    text-align: center;
    background-color: #0d0d0dd0;
    border-top: 1px solid black;
    padding: {
        top: 6px;
        bottom: 0px;
    }
    color: #732424;
};

</style>

<script setup lang="ts">
import { SiteSwitcherToggler } from 'tzz-shared'
import { useGameStore } from '@stores/gameStore';
import { StyledButton } from 'tzz-shared';
import CreateMenu from './CreateMenu.vue';
const gameStore = useGameStore()
</script>