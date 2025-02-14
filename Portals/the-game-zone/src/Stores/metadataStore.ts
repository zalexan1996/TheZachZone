import { defineStore } from "pinia";
import { MetadataClient } from '@services/tgz.api.ts'
import { TGZ_API_BASE_URL, AxiosInstance } from '@services/axiosService'
import { useToastStore } from "tzz-shared";

export const useMetadataStore = defineStore('metadata', () => {
    const client = new MetadataClient(TGZ_API_BASE_URL, AxiosInstance)
    const toastStore = useToastStore()

    const addGenre = async(genreName: string) => {
        try
        {
            await client.addGenre(genreName);
            toastStore.push({
                title: 'Add Genre',
                body: `Successfully added genre '${genreName}'`,
                duration: 3000,
                severity: 'success'
            })

            return true;
        }
        catch (e)
        {
            toastStore.push({
                title: 'Add Genre',
                body: `Failed to add genre '${genreName}'.`,
                duration: 3000,
                severity: 'danger'
            })
            return false;
        }
    }

    const getGenres = async() => {
        return await client.getGenres();
    }

    const removeGenre = async(genreName: string) => {
        try
        {
            await client.removeGenre(genreName)
            toastStore.push({
                title: 'Remove Genre',
                body: `Successfully removed genre '${genreName}'`,
                duration: 3000,
                severity: 'success'
            })

            return true;
        }
        catch (e)
        {
            toastStore.push({
                title: 'Remove Genre',
                body: `Failed to removed genre '${genreName}'.`,
                duration: 3000,
                severity: 'danger'
            })
            return false;
        }
    }

    return {
        addGenre,
        getGenres,
        removeGenre
    }
})