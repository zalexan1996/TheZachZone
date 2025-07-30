import { defineStore } from "pinia";
import { SearchClient, SearchResultDto } from "@/Services/pp.api";
import { AxiosInstance, PP_API_BASE_URL } from "@/Services/axiosService";
import { ref } from "vue";

export const useSearchStore = defineStore('search', () => {
    const client = new SearchClient(PP_API_BASE_URL, AxiosInstance)

    const searchResults = ref<SearchResultDto[]>([])
    const search = async (term: string) => {
        searchResults.value = await client.search(term)
    }
    const clearSearch = () => {
        searchResults.value = []
    }
    return {
        searchResults,
        search,
        clearSearch
    }
})