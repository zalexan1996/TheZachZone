<template>
    <Panel>
        <h4>Allowed Genres</h4>
        <hr/>
        <div class="d-flex flex-row">
            <span v-for="genre in genres" class="badge bg-success py-2 px-2 d-inline-block mx-1">
                {{ genre }}
                <span class="fa fa-x span-button" @click="deleteGenre"></span>
            </span>
        </div>
        <div class="input-group mt-4">
            <input v-model="newGenre" class="form-control" placeholder="Provide a genre name..."/>
            <button class="btn btn-primary" @click="addGenreClick" :disabled="addDisabled">Add</button>
        </div>
        <span v-if="addDisabled" class="text-danger my-2 d-block">{{ disabledMessage }}</span>
    </Panel>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue'
import { Panel } from 'tzz-shared'
import { useMetadataStore } from '@stores/metadataStore';

const metadataStore = useMetadataStore();
const genres = ref<string[]>([])
const newGenre = ref<string>('')

const addDisabled = computed(() => genreNotSupplied.value || genreAlreadyExists.value)
const genreNotSupplied = computed(() => newGenre.value == '')
const genreAlreadyExists = computed(() => genres.value.findIndex(g => g == newGenre.value) > -1)
const disabledMessage = computed(() => {
    if (genreNotSupplied.value) {
        return 'You must supply a genre.'
    }
    if (genreAlreadyExists.value) {
        return 'That genre already exists.'
    }
})

const deleteGenre = async () => {

}

const addGenreClick = async () => {
    await metadataStore.addGenre(newGenre.value)
    await reset()
}

onMounted(async () => {
    await reset()
})

const reset = async () => {
    genres.value = Object.values(await metadataStore.getGenres())
    newGenre.value = ''
}
</script>

<style scoped lang="scss">
.span-button {
    cursor: pointer;
    margin-left: 0.5rem;
}
</style>