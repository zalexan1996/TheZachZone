import { ref } from 'vue'
import { defineStore } from "pinia";
import { GameClient } from '@services/pp.api.ts'
import {PP_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';
import { AddGameCommand, GameDto } from '@/Services/pp.api';

export const useGameStore = defineStore('gameStore', () => {
    const client = new GameClient(PP_API_BASE_URL, AxiosInstance)

    const games = ref<GameDto[]>([])
    const activeGame = ref<number|undefined>()

    const getGames = async () => {
        games.value = await client.getGames()
        return games.value
    }

    const addGame = async (name: string) => {
        await client.addGame({
            name: name
        } as AddGameCommand);
    }

    const removeGame = async (id: number) => {
        await client.removeGame(id);
    }

    const setActiveGame = (id: number) => {
        activeGame.value = id
    }

    return {
        getGames,
        addGame,
        removeGame,
        setActiveGame,
        games,
        activeGame
    }
})