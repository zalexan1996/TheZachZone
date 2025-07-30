import { ref } from 'vue'
import { defineStore } from "pinia";
import { GameClient } from '@services/pp.api'
import {PP_API_BASE_URL, AxiosInstance } from '@services/axiosService';
import { AddGameCommand, GameDto } from '@services/pp.api';
import { useActivityStore } from './activityStore';

export const useGameStore = defineStore('gameStore', () => {
    const client = new GameClient(PP_API_BASE_URL, AxiosInstance)
    const activityStore = useActivityStore();
    const games = ref<GameDto[]>([])
    const activeGame = ref<number|undefined>()

    const getGameDetails = (id: number): GameDto => {
        return games.value.filter(g => g.id == id)[0]
    }
    const load = async () => {
        games.value = await client.getGames()
        return games.value
    }

    const addGame = async (name: string, primaryColor: string, secondaryColor: string) => {
        await client.addGame({
            name: name,
            primaryColor: primaryColor,
            secondaryColor: secondaryColor
        } as AddGameCommand);
        await load()
        await activityStore.load();
    }

    const removeGame = async (id: number) => {
        await client.removeGame(id);
        await load()
        await activityStore.load();
    }

    const setActiveGame = (id: number) => {
        activeGame.value = id
    }
    const isActiveGame = (game: GameDto | number) => game instanceof GameDto ? game.id == activeGame.value : game == activeGame.value 

    const getGameOptions = (): [number, string][] => games.value.map(x => [x.id!, x.name!]);
    return {
        load,
        addGame,
        removeGame,
        setActiveGame,
        getGameDetails,
        isActiveGame,
        getGameOptions,
        games,
        activeGame
    }
})