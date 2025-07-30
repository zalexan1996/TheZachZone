import { useGlobalStore } from "@/Stores/globalStore"
import { getRouteNameByEntityType } from "./routerService"

export type EntityType = 'Character' | 'Arcana' | 'Game'

export const useEntityService = () => {
    const globalStore = useGlobalStore()
    

    const getName = (entityType: EntityType, entityId: number) => {
        switch (entityType)
        {
            case 'Character':
                return globalStore.characterStore.getCharacterDetails(entityId)?.name 
            case 'Arcana':
                return globalStore.arcanaStore.getArcanaDetails(entityId)?.name
            case 'Game':
                return globalStore.gameStore.getGameDetails(entityId)?.name
        }
    }

    const buildRouterLinkFromEntity = (entityType: EntityType, entityId: number) => {
        return {
            name: getRouteNameByEntityType(entityType),
            params: {
                id: entityId
            }
        }
    }
        

    return {
        getName,
        buildRouterLinkFromEntity
    }
}