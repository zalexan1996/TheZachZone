import { ref } from 'vue'
import { defineStore } from "pinia";
import { ArcanaClient } from '@services/pp.api.ts'
import { PP_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';
import { AddArcanaCommand, ArcanaDto } from '@/Services/pp.api';

export const useArcanaStore = defineStore('arcanaStore', () => {
    const client = new ArcanaClient(PP_API_BASE_URL, AxiosInstance)

    const arcana = ref<ArcanaDto[]>([])
    const getArcana = async () => {
        arcana.value = await client.getArcanas()
        return arcana.value
    }

    const addArcana = async (name: string) => {
        await client.addArcana({
            name: name
        } as AddArcanaCommand);

    }

    const removeArcana = async (id: number) => {
        await client.removeArcana(id);
    }
    return {
        getArcana,
        addArcana,
        removeArcana,
        arcana
    }
})