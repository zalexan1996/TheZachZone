import { defineStore } from "pinia";
import { ref } from "vue";
import { SocialLinkDto, SocialLinkClient, SetSocialLinkCommand } from '../Services/pp.api'
import { PP_API_BASE_URL, AxiosInstance } from '../Services/axiosService';
import { useActivityStore } from "./activityStore";

export const useSocialLinkStore = defineStore('social-link', () => {
    const client = new SocialLinkClient(PP_API_BASE_URL, AxiosInstance)
    const activityStore = useActivityStore();
    
    const socialLinks = ref<SocialLinkDto[]>([]);

    const load = async () => {
        socialLinks.value = await client.getSocialLinks()
    }
    const getSocialLinkDetails = (socialLinkId: number) => {
        return socialLinks.value.filter(x => x.socialLinkId == socialLinkId)[0]
    }

    const getForGame = (gameId: number) => {
        return socialLinks.value?.filter(x => x.gameId == gameId) ?? []
    }

    const getForCharacter = (characterId: number) => {
        let links = socialLinks.value?.filter(x => x.characterId == characterId);
        if (links.length == 1) {
            return links[0];
        }
        return undefined;
    }

    const addSocialLink = async (command: SetSocialLinkCommand) => {
        await client.setSocialLink(command)
        await load()
        await activityStore.load();
    }

    const deleteSocialLink = async (id: number) => {
        await client.deleteSocialLink(id)
        await load();
        await activityStore.load();
    }
    return {
        socialLinks,
        load,
        getForGame,
        getForCharacter,
        getSocialLinkDetails,
        addSocialLink,
        deleteSocialLink
    }
})