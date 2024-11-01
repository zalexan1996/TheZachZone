import { ref } from 'vue'
import { defineStore } from "pinia";
import { AccountClient, GeneralInformationDto  } from "@services/tzz.api";
import {TZZ_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';

export const useAccountStore = defineStore('account', () => {

    const userInfo = ref<GeneralInformationDto|undefined>()
    const client = new AccountClient(TZZ_API_BASE_URL, AxiosInstance)
    
    const getUserInfo = async (force: boolean = false) => {
        if (force || userInfo.value == undefined) {
            userInfo.value = await client.getGeneralInformation();
        }
        return userInfo.value;
    }

    return {
        getUserInfo
    }
})