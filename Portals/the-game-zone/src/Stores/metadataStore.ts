import { defineStore } from "pinia";
import { MetadataClient } from '@services/tgz.api.ts'
import { TGZ_API_BASE_URL, AxiosInstance } from '@services/axiosService'

export const useMetadataStore = defineStore('metadata', () => {
    const client = new MetadataClient(TGZ_API_BASE_URL, AxiosInstance)

    const addGenre = async(genreName: string) => {
        return await client.addGenre(genreName);
    }

    const getGenres = async() => {
        return await client.getGenres();
    }

    return {
        addGenre,
        getGenres
    }
})