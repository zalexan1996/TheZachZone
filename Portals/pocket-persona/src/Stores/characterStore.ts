import { ref } from 'vue'
import { CharacterClient, CharacterDto, AddCharacterCommand, FileParameter, UpdateCharacterCommand } from '@services/pp.api'
import { PP_API_BASE_URL, AxiosInstance } from '@services/axiosService';
import { defineStore } from "pinia";
import { useActivityStore } from './activityStore';

export const useCharacterStore = defineStore('character', () => {
    const client = new CharacterClient(PP_API_BASE_URL, AxiosInstance)
    const activityStore = useActivityStore();

    const characters = ref<CharacterDto[]>([]);

    const getCharacterDetails = (id: number) => {
        return characters.value.filter(x => x.characterId == id)[0]
    }
    const getCharacters = async () => {
        characters.value = await client.getCharacters();
        return characters.value
    }

    const addCharacter = async (character: AddCharacterCommand) => {
        await client.addCharacter(character);
        await getCharacters();
        await activityStore.load();
    }

    const removeCharacter = async (id: number) => {
        await client.removeCharacter(id);
        await activityStore.load();
    }
    
    const setCharacterIcon = async (file: File, characterId: number) => {
        const fileParameter: FileParameter = {
            data: file,
            fileName: file.name
        }
        
        console.log(fileParameter)
        await client.setCharacterIcon(characterId, fileParameter)
        await getCharacters();
    }

    const updateCharacter = async (character: UpdateCharacterCommand) => {
        await client.updateCharacter(character);
    }

    return {
        characters,
        getCharacters,
        getCharacterDetails,
        addCharacter,
        removeCharacter,
        setCharacterIcon,
        updateCharacter
    }
})