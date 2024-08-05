import { defineStore } from "pinia";
import { API_BASE_URL, AxiosInstance } from "@/Services/axiosService.ts";
import { HomeClient } from '@/Services/apiService.ts';

export const useHomeStore = defineStore("home", () => {
    
    const client = new HomeClient(API_BASE_URL, AxiosInstance)
    const isLoggedIn = async () => {
        return await client.restricted()
            .then(f => true, e => false)
            .catch(x => false)
    }
    return {
        isLoggedIn
    }
})