import { ref } from 'vue'
import { defineStore } from "pinia";
import { AccountClient, GeneralInformationDto, GetUsersQueryDto  } from "@services/tzz.api";
import {TZZ_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';
import { useToastStore } from 'tzz-shared'

export const useAccountStore = defineStore('account', () => {

    const toastStore = useToastStore();
    const userInfo = ref<GeneralInformationDto|undefined>()
    const client = new AccountClient(TZZ_API_BASE_URL, AxiosInstance)
    const users = ref<GetUsersQueryDto[]>([])
    
    
    const getUserInfo = async (force: boolean = false) => {
        if (force || userInfo.value == undefined) {
            userInfo.value = await client.getGeneralInformation();
        }
        return userInfo.value;
    }

    const listUsers = async () => {
        users.value = await client.listUsers()
        return users.value
    }
    
    const grantAdmin = async (userId: number) => {
        try
        {
            await client.grantAdminAccess(userId)

            toastStore.push({
                title: 'Grant Admin Successful',
                body: `User ${userId} is now an admin.`,
                duration: 3000,
                severity: 'success'
            })

            users.value = await client.listUsers()
        }
        catch (e)
        {
            let key = Object.keys(e.response.errors)[0]

            toastStore.push({
                title: 'Grant Admin Failure',
                body: e.response.errors[key],
                duration: 3000,
                severity: 'danger'
            })
        }
    }

    const revokeAdmin = async (userId: number) => {
        try
        {
            await client.revokeAdminAccess(userId)

            toastStore.push({
                title: 'Revoke Admin Successful',
                body: `User ${userId} is no longer an admin.`,
                duration: 3000,
                severity: 'success'
            })

            users.value = await client.listUsers()
        }
        catch (e)
        {
            let key = Object.keys(e.response.errors)[0]

            toastStore.push({
                title: 'Revoke Admin Failure',
                body: e.response.errors[key],
                duration: 3000,
                severity: 'danger'
            })
        }
    }
    
    const deleteUser = async (userId: number) => {
        
        try
        {
            await client.deleteUser(userId)

            toastStore.push({
                title: 'Delete Successful',
                body: `User ${userId} was successfully deleted.`,
                duration: 3000,
                severity: 'success'
            })

            users.value = await client.listUsers()
        }
        catch (e)
        {
            let key = Object.keys(e.response.errors)[0]

            toastStore.push({
                title: 'Delete Failure',
                body: e.response.errors[key],
                duration: 3000,
                severity: 'danger'
            })
        }
    }

    return {
        getUserInfo,
        listUsers,
        grantAdmin,
        revokeAdmin,
        deleteUser,
        users
    }
})