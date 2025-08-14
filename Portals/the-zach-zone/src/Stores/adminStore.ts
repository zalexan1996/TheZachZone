import { AxiosInstance, TZZ_API_BASE_URL } from "@/Services/AxiosService";
import { AccountClient, AdminClient } from "@/Services/tzz.api";
import { defineStore } from "pinia";

export const useAdminStore = defineStore('admin', () => {
    
    const accountClient = new AccountClient(TZZ_API_BASE_URL, AxiosInstance)
    const adminClient = new AdminClient(TZZ_API_BASE_URL, AxiosInstance)
    
    const seedData = async () => {
        await adminClient.seed()
    }

    return {
        seedData
    }
})