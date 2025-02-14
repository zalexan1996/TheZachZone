<template>
    <div class="d-flex flex-column px-4 pt-1">
        <h4 class="mb-0 mt-4">Available upload methods:</h4>
        <div class="mt-0 mx-0 d-flex flex-lg-row flex-sm-column justify-content-around align-items-stretch ">
            <div class="bordered d-flex flex-column justify-content-between align-items-stretch bg-primary">
                <div class="d-flex flex-column justify-content-start align-items-start">
                    <h4><i class="fa fa-gamepad me-2"></i>Godot</h4>
                    <hr class="align-self-stretch"/>
                    <p>Export your Godot project compress it to a zip file. Make sure your export mode results in an index.html file or the upload will fail.</p>
                    <a class="text-dark" href="https://docs.godotengine.org/en/stable/tutorials/export/exporting_for_web.html">More information...</a>
                </div>
            </div>
            <div class="bordered d-flex flex-column justify-content-between align-items-stretch mx-lg-4 mx-sm-0 bg-warning text-dark">
                <div class="d-flex flex-column justify-content-start align-items-start">
                    <h4><i class="fab fa-unity me-2"></i>Unity</h4>
                    <hr class="align-self-stretch"/>
                    <p>Export your Unity project compress it to a zip file. Make sure your export mode results in an index.html file or the upload will fail.</p>
                    <p>I don't know anything about Unity yet so I hope this works.</p>
                    <a class="text-dark" href="https://docs.unity3d.com/Manual/webgl-building.html">More information...</a>
                </div>
            </div>
            <div class="bordered d-flex flex-column justify-content-between align-items-stretch bg-success">
                <div class="d-flex flex-column justify-content-start align-items-start">
                    <h4><i class="fab fa-html5 me-2"></i>HTML5</h4>
                    <hr class="align-self-stretch"/>
                    <p>Zip up your HTML5 game and upload. Make sure your main html page is called index.html or the upload will fail.</p>
                    <a class="text-dark" href="https://gamedevacademy.org/how-to-make-a-html5-game/">More information...</a>
                </div>
            </div>
        </div>
        <h4 class="mb-0">Game Information</h4>
        <div class="d-flex flex-column my-3 mx-0 bordered justify-content-start">
            <div class="row">
                <InputText for="name" v-model="name" class="col-7" label="Name" 
                    placeholder="Provide a name for your game." helpText="The name of your game should be a hook to entice potential players."/>
                <InputText for="genre" v-model="genre" class="col-auto" label="Genre"
                    placeholder="Choose the genre that best describes your game." :options="Object.values(genres)"
                    type="select" helpText="Choose the option that best describes your game."/>
                <Checkbox for="makePublic" v-model="makePublic" class="col-auto" label="Make Public" placeholder="Should this game be visible to everybody?" helpText="You will be able to change this setting later"/>
            </div>
            <InputText for="description" v-model="description" label="Description" type="textarea" 
                class="w-100" helpText="Give a brief description on what this game is about. Include information about how to play the game if there isn't an in-game help."
                placeholder="Provide a description..."/>
            
            <div class="d-flex flex-column justify-content-stretch align-items-stretch">
                <div class="bordered p-3 bg-dark mx-0 my-4 d-flex flex-column">
                    <input @change="onFileUpload" class="me-4" type="file"/>
                    <ErrorMessage class="text-danger" name="file"></ErrorMessage>
                </div>
                <button class="btn btn-success align-self-center" :disabled="!isFormValid" @click="onSubmit" type="button">Upload</button>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { InputText, Checkbox } from 'tzz-shared'
import { ErrorMessage, useForm, useIsFormValid } from 'vee-validate'
import * as yup from 'yup'
import { useGameStore } from '@/Stores/gameStore.ts'
import { useRouter } from 'vue-router'
import { useMetadataStore } from '@/Stores/metadataStore'

const routerStore = useRouter()
const gameStore = useGameStore()
const metadataStore = useMetadataStore()

const genres = ref({})

const form = useForm({
    validationSchema: {
        name: yup.string().required('The name is required.'),
        description: yup.string().required('The description is required.'),
        genre: yup.string().required('The genre is required.'),
        makePublic: yup.bool().required(),
        file: yup.mixed().required('You must upload a file.')
    },
    validateOnMount: true
})


const [name] = form.defineField('name')
const [description] = form.defineField('description')
const [genre] = form.defineField('genre')
const [makePublic] = form.defineField('makePublic')
const [file] = form.defineField('file')
const isFormValid = useIsFormValid()

const onFileUpload = async (e) => {
    const fileList = e.target.files as FileList;
    file.value = fileList.item(0) ?? undefined
}

const onSubmit = async () => {
    const id = await gameStore.addGame(name.value, description.value, [genre.value], file.value)
    routerStore.push({
        name: 'GameInfo',
        params: { id: id }
    })
}

makePublic.value = false

onMounted(async () => {
    genres.value = await metadataStore.getGenres();
})
</script>

<style scoped lang="scss">
.bordered {
    border: dashed 1px #657272;
    border-radius: 15px;
    padding: 2rem;
    margin: {
        top: 1rem;
        bottom: 1rem;
        left: 0px;
        right: 0px;
    }
}
</style>