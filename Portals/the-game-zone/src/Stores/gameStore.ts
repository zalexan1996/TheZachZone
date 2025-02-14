import { AddGameCommentCommand, CommentDto, GameClient, GameImageDto, GameInfoDto, FileParameter } from '@services/tgz.api.ts'
import { TGZ_API_BASE_URL, AxiosInstance } from '@services/axiosService'
import {defineStore} from 'pinia'
import { ref } from 'vue'
import { SubmitReviewCommand } from '@services/tgz.api'
import { GetReviewsQuery } from '../Services/tgz.api'

export const useGameStore = defineStore('game', () => {
    const client = new GameClient(TGZ_API_BASE_URL, AxiosInstance)

    const games = ref<GameInfoDto[]>([])
    const comments = ref<CommentDto[]>([])
    const images = ref<GameImageDto[]>([])

    const loadGames = async (id: number | undefined = undefined, name: string | undefined = undefined, category: string | undefined = undefined) => {
        games.value = await client.getGames(id, name, category)
        return games.value
    }

    const addGame = async (name: string, description: string, genre: string, file: File) => {
        let fileParameter = {
            data: file,
            fileName: 'game.zip'
        } as FileParameter
        return await client.addGame(name, description, genre, fileParameter)
    }

    const deleteGame = async(id: number) => {
        await client.deleteGame(id);
    }

    const getHostedPathFromId = async (id: number) => {
        return `${TGZ_API_BASE_URL}/games/${id}/index.ts`
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

    const getReviewsForGame = async (gameId: number) => {
        return await client.getReviews(gameId)
    }

    const submitReview = async (gameId: number, content: any) => {
        await client.submitReview({
            gameId: gameId,
            content: content
        } as SubmitReviewCommand);
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
        submitReview,
        getReviewsForGame,
        games,
        comments,
        images
    }
})