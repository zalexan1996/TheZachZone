import { ref } from 'vue'
import { defineStore } from "pinia";
import { ArcanaClient } from '@services/pp.api'
import { PP_API_BASE_URL, AxiosInstance } from '@services/axiosService';
import { AddArcanaCommand, ArcanaDto } from '@/Services/pp.api';
import { useActivityStore } from './activityStore';

export const useArcanaStore = defineStore('arcanaStore', () => {
    const client = new ArcanaClient(PP_API_BASE_URL, AxiosInstance)

    const activityStore = useActivityStore();
    const arcana = ref<ArcanaDto[]>([])

    const getArcanaDetails = (id: number) => {
        return arcana.value.filter(a => a.id == id)[0]
    }
    const getArcana = async () => {
        arcana.value = await client.getArcanas()
        return arcana.value
    }

    const addArcana = async (name: string) => {
        await client.addArcana({
            name: name
        } as AddArcanaCommand);
        await getArcana()
        await activityStore.load();
    }

    const removeArcana = async (id: number) => {
        await client.removeArcana(id);
        await activityStore.load();
    }
    return {
        getArcana,
        getArcanaDetails,
        addArcana,
        removeArcana,
        arcana
    }
})