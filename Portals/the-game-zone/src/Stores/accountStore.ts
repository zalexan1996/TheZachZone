import { defineStore } from "pinia";
import { AccountClient, UserInfoDto  } from "@services/tzz.api";
import {TZZ_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';

export const useAccountStore = defineStore('account', () => {

    const client = new AccountClient(TZZ_API_BASE_URL, AxiosInstance)
    const getUserInfo = async () => {
        return await client.getGeneralInformation();
    }

    return {
        getUserInfo
    }
})