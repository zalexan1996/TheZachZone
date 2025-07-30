<template>
    <div v-if="favoriteStore.favorites.length > 0" class="container-fluid w-100">
        <div class="row g-1">
            <div v-for="f of favoriteStore.favorites" class="px-4 col-auto d-flex flex-column justify-content-start align-items-start favoriteItem m-1">
                <RouterLink :to="entityService.buildRouterLinkFromEntity(f.entityType, f.entityId)" class="d-flex flex-row align-items-center w-100" style="text-decoration: none; color:inherit;">
                    <span class="mr-3 w-100">
                        {{ entityService.getName(f.entityType, f.entityId!) }}
                        <FavoriteToggle style="display: inline-block" :entity-id="f.entityId!" :entity-type="f.entityType"/>
                    </span>
                </RouterLink>
                <em class="w-100">{{ f.entityType }}</em>
            </div>
        </div>
    </div>
    <em v-else class="text-secondary">There's no favorites yet...</em>
</template>

<script setup lang="ts">
import { useEntityService } from '@/Services/entityService';
import { useFavoriteStore } from '@/Stores/favoriteStore';
import FavoriteToggle from './FavoriteToggle.vue';

const favoriteStore = useFavoriteStore()
const entityService = useEntityService()
</script>

<style scoped lang="scss">
.favoriteItem {
    background-color: #33333344;
    margin-top: 2px;
    margin-bottom: 2px;
    padding: 3px;
    border: solid 1px #bcca3b44;
    border-radius: 8px;
}
span, em {
    color: #bebebe;
    width: 100%;
}
</style>