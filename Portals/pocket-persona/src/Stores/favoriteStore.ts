import { EntityType } from "@/Services/entityService";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useFavoriteStore = defineStore('favorite', () => {
    const favorites = ref<{
        entityId: number,
        entityType: EntityType
    }[]>([])

    const toggleFavorite = (entityType: EntityType, entityId: number) => {
       let existingIndex = favorites.value.findIndex(e => e.entityId == entityId && e.entityType.toLowerCase() == entityType.toLowerCase())

        if (existingIndex == -1) {
            favorites.value.push({
                entityId: entityId,
                entityType: entityType
            })
        }
        else {
            favorites.value.splice(existingIndex, 1);
        }
    }
    const isFavorite = (entityType: EntityType, entityId: number) => {
        return !!favorites.value.find(e => e.entityId == entityId && e.entityType.toLowerCase() == entityType.toLowerCase())
    }
    return {
        favorites,
        toggleFavorite,
        isFavorite
    }
})