import { AddGameCommentCommand, CommentDto, FileParameter, GameClient, GameImageDto, GameInfoDto } from '@services/apiService'
import { API_BASE_URL, AxiosInstance } from '@services/axiosService'
import {defineStore} from 'pinia'
import { ref } from 'vue'

export const useGameStore = defineStore('game', () => {
    const client = new GameClient(API_BASE_URL, AxiosInstance)

    const games = ref<GameInfoDto[]>([])
    const comments = ref<CommentDto[]>([])
    const images = ref<GameImageDto[]>([])

    const loadGames = async (id: number | undefined = undefined, name: string | undefined = undefined, category: string | undefined = undefined) => {
        games.value = await client.getGames(id, name, category)
        return games.value
    }

    const addGame = async (name: string, description: string, categories: string[], fileContent: FileParameter) => {
        await client.addGame(name, description, categories, fileContent)
    }

    const deleteGame = async(id: number) => {
        await client.deleteGame(id);
    }

    const getHostedPathFromId = async (id: number) => {
        return `${API_BASE_URL}/games/${id}/index.ts`
    }

    const loadComments = async(gameInfoId: number) => {
        comments.value = await client.getGameComments(gameInfoId);
        return comments.value
    }

    const addComment = async (command: AddGameCommentCommand) => {
        return await client.addGameComment(command);
    }

    const deleteComment = async (commentId: number) => {
        await client.deleteGameComment(commentId);
    }
    const addImage = async (gameInfoId: number, file: FileParameter) => {
        await client.addGameImage(gameInfoId, file)
    }

    const getImages = async (gameInfoId: number) => {
        images.value = await client.getGameImages(gameInfoId);
        return images.value
    }
    return {
        loadGames,
        addGame,
        deleteGame,
        getHostedPathFromId,
        loadComments,
        deleteComment,
        addComment,
        addImage,
        getImages,
        games,
        comments,
        images
    }
})