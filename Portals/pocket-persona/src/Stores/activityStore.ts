import { AxiosInstance, PP_API_BASE_URL } from "@/Services/axiosService";
import { ActivityLog, ActivityLogClient } from "@/Services/pp.api";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useActivityStore = defineStore('activity', () => {
    const client = new ActivityLogClient(PP_API_BASE_URL, AxiosInstance);
    
    const activities = ref<ActivityLog[]>([]);

    const load = async () => {
        activities.value = await client.getActivityLogs();
    }
    
    return {
        activities,
        load
    }
})