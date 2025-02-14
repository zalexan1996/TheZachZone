<template>
    <Panel>
        <h4>Allowed Genres</h4>
        <hr/>
        <div v-if="genres.length > 0" class="d-flex flex-row flex-wrap">
            <span v-for="genre in genres" class="badge bg-success py-2 px-2 mx-1 mb-1">
                {{ genre }}
                <span class="fa fa-x span-button" @click="removeGenre(genre)"></span>
            </span>
        </div>
        <div class="input-group mt-4">
            <input v-model="newGenre" v-onEnter="addGenreClick" class="form-control" placeholder="Provide a genre name..." ref="genreInput"/>
            <button class="btn btn-primary" @click="addGenreClick" :disabled="addDisabled">Add</button>
        </div>
        <span v-if="addDisabled" class="text-danger my-2 d-block">{{ disabledMessage }}</span>
    </Panel>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue'
import { Panel, useToastStore } from 'tzz-shared'
import { useMetadataStore } from '@stores/metadataStore';

const metadataStore = useMetadataStore();
const genres = ref<string[]>([])
const newGenre = ref<string>('')
const genreInput = ref<HTMLInputElement|undefined>()

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

const removeGenre = async (name: string) => {
    if (await metadataStore.removeGenre(name)) {
        await reset()
    }
}

const addGenreClick = async () => {
    if (await metadataStore.addGenre(newGenre.value)) {
        await reset()
    }
}

onMounted(async () => {
    await reset()
})

const reset = async () => {
    genres.value = await metadataStore.getGenres()
    newGenre.value = ''
    genreInput.value?.focus()
}
</script>

<style scoped lang="scss">
.span-button {
    cursor: pointer;
    margin-left: 0.5rem;
}
</style>