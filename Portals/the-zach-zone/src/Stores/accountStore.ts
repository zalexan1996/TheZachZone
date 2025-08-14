import { defineStore } from "pinia";
import { AccountClient, CreateAccountCommand, UserInfoDto, UpdateGeneralInformationCommand, ResetPasswordCommand  } from "@services/tzz.api";
import {TZZ_API_BASE_URL, AxiosInstance } from '@services/AxiosService.ts';
import { ref } from "vue";

export const useAccountStore = defineStore("account", () => {

    const client = new AccountClient(TZZ_API_BASE_URL, AxiosInstance)
    const userInfo = ref<UserInfoDto|undefined>()

    const doesUserExist = async (clientId: string) => {
        return await client.doesUserExist(clientId);
    }

    const createAccount = async (command: CreateAccountCommand) => {
        return await client.createAccount(command)
    }

    const login = async (email: string, password: string) => {
        return await client.login(email, password).then(x => true, e => false).catch(e => false)
    }
    const logout = async () => {
        let response = await client.logout().then(x => x, e => undefined).catch(x => undefined);

        if (response) {
            await loadUserInfo()
        }
        return response
    }

    const loadUserInfo = async () => {
        userInfo.value = await client.userInfo().then(x => x as UserInfoDto, e => undefined).catch(x => undefined);
    }

    const isLoggedIn = () => {
        return !!userInfo.value
    }
    const getGeneralInformation = async () => {
        return await client.getGeneralInformation();
    }
    const updateGeneralInformation = async (command: UpdateGeneralInformationCommand) => {
        return await client.updateGeneralInformation(command);
    }

    const isAdmin = () => {
        return userInfo.value?.isAdmin ?? false
    }

    const getPasswordResetToken = async() => client.getPasswordResetToken()
    const resetPassword = async (command: ResetPasswordCommand) => {
        return await client.resetPassword(command)
    }
    return {
        doesUserExist,
        createAccount,
        login,
        logout,
        loadUserInfo,
        isLoggedIn,
        isAdmin,
        getGeneralInformation,
        updateGeneralInformation,
        getPasswordResetToken,
        resetPassword,
        userInfo
    }
})