<template>
    <div class="d-flex flex-row justify-content-between">
        <div class="w-100 d-flex"></div>
        <div class="input-group w-100 d-flex">
            <div class="dropdown w-100">
                <input v-model="searchTerm" class="form-control" placeholder="Search for a character, game, arcana, etc..." @input="onInput" data-bs-toggle="dropdown"/>
                
                <ul class="w-100 dropdown-menu list-unstyled p-0 bg-dark">
                    <li v-for="r of searchStore.searchResults">
                        <SearchResult :entity-id="r.entityId!" :entity-type="r.entityType as EntityType"/>
                    </li>
                </ul>
            </div>
        </div>
        <div class="w-100 d-flex justify-content-end">
            <RouterLink :to="{path: '/'}" v-if="route.fullPath != '/'" class="btn btn-outline-secondary d-flex flex-row align-items-center">
                <span class="fa-regular fa-square-caret-left me-2"></span>
                Back
            </RouterLink>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useSearchStore } from '@/Stores/searchStore';
import { ref } from 'vue';
import { debounce } from 'lodash'
import SearchResult from './Search/SearchResult.vue';
import { useRoute } from 'vue-router';
import { EntityType } from '@/Services/entityService';

const searchStore = useSearchStore();
const route = useRoute()
const searchTerm = ref<string>('')
const debouncedSearch = debounce(async () => {
        if (searchTerm.value != '') {
            await searchStore.search(searchTerm.value)
        }
        else {
            searchStore.clearSearch()
        }
    }, 300);

const onInput = async () => {
    await debouncedSearch()
}
</script>