import { ref } from 'vue'
import { CharacterClient, CharacterDto, AddCharacterCommand } from '@services/pp.api.ts'
import { PP_API_BASE_URL, AxiosInstance } from '@services/axiosService.ts';
import { defineStore } from "pinia";

export const useCharacterStore = defineStore('character', () => {
    const client = new CharacterClient(PP_API_BASE_URL, AxiosInstance)
    
    const characters = ref<CharacterDto[]>([]);

    const getCharacters = async () => {
        characters.value = await client.getCharacters();
        return characters.value
    }

    const addCharacter = async (character: AddCharacterCommand) => {
        await client.addCharacter(character);
    }

    const removeCharacter = async (id: number) => {
        await client.removeCharacter(id);
    }
    return {
        characters,
        getCharacters,
        addCharacter,
        removeCharacter
    }
})