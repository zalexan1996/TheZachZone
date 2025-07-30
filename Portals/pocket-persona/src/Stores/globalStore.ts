import { defineStore } from "pinia";
import { useSocialLinkStore } from "./socialLinkStore";
import { useArcanaStore } from './arcanaStore'
import { useCharacterStore } from './characterStore'
import { useGameStore } from './gameStore'
import { useActivityStore } from "./activityStore";
import { useSearchStore } from "./searchStore";

export const useGlobalStore = defineStore('global', () => {

    const activityStore = useActivityStore();
    const arcanaStore = useArcanaStore();
    const characterStore = useCharacterStore();
    const gameStore = useGameStore();
    const socialLinkStore = useSocialLinkStore()

    const load = async () => {
        await Promise.all([
            arcanaStore.getArcana(),
            characterStore.getCharacters(),
            gameStore.load(),
            socialLinkStore.load(),
            activityStore.load()
        ])
    }

    return {
        arcanaStore,
        characterStore,
        gameStore,
        socialLinkStore,
        load
    }
})