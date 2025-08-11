<template>
    <div class="container-fluid" :style="{color: primaryColor}">
        <div class="row">
            <div class="col-auto">
                <div class="d-flex flex-column">
                    <div style="position: relative;">
                        <img ref="profilePicImg" class="img-responsive" width="250" height="250" :src="profilePicSrc"/>
                        <button v-if="!character?.imageData" class="upload-button btn btn-secondary" @click="onUploadClick"><span class="fa fa-upload me-2"></span>Upload</button>
                        <button v-if="character?.imageData" class="edit-button btn btn-secondary" @click="onUploadClick"><span class="fa fa-edit"></span></button>
                        <input type="file" ref="fileInput" style="display: none" @change="handleFileUpload" />
                    </div>

                    <SocialLinkPanel :character-id="characterId" class="w-auto p-0 mt-4"/>
                </div>
            </div>
            <div class="col">
                <div class="container-fluid mx-4">
                    <div class="row">
                        <div class="col-12">
                            <h2>
                                {{ character?.name }}
                                <FavoriteToggle :entity-id="characterId!" entity-type="Character"/>
                            </h2>
                            <div class="d-flex flex-row justify-content-start align-items-center">
                                <h5 class="text-secondary me-4">Character</h5>
                                <GameBadge v-if="character?.gameId" :game-id="character.gameId"/>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div v-if="socialLinkId" class="col-md-6 col-sm-12">
                            <DialogThread :social-link-id="socialLinkId"/>
                        </div>
                        <div class="col-md-6 col-sm-12" v-if="socialLinkId">
                            <Gifts :social-link-id="socialLinkId"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import FavoriteToggle from '@/components/Favorite/FavoriteToggle.vue';
import GameBadge from '@/components/GameBadge/GameBadge.vue';
import { CharacterDto } from '@/Services/pp.api';
import { useCharacterStore } from '@/Stores/characterStore';
import { useGameStore } from '@/Stores/gameStore';
import { computed, onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import SocialLinkPanel from './Components/SocialLinkPanel.vue';
import DialogThread from './Components/DialogThread.vue';
import { useGlobalStore } from '@/Stores/globalStore';
import Gifts from './Components/Gifts.vue';

const globalStore = useGlobalStore()
const characterStore = useCharacterStore();
const gameStore = useGameStore();

const fileInput = ref<HTMLInputElement|undefined>()
const route = useRoute()
const characterId = ref<number>(Number.parseInt(route.params['id']!.toString()))
const character = ref<CharacterDto|undefined>()
const profilePicImg = ref<HTMLImageElement>();

const profilePicSrc = computed(() => {
    return character.value?.imageData ? `data:image/png;base64,${character.value.imageData}` : '/character/default.png'
})

const primaryColor = computed(() => {
    return character.value?.gameId ? gameStore.getGameDetails(character.value.gameId).primaryColor : 'gray'
})

const socialLinkId = computed(() => {
    return globalStore.socialLinkStore.getForCharacter(characterId.value)?.socialLinkId
})
const onUploadClick = () => {
    fileInput.value?.click()
}

const handleFileUpload = async (event: any) => {
  const file = event.target.files[0];
  if (file) {
    await characterStore.setCharacterIcon(file, characterId.value)
    await reload()
  }
}

onMounted(async () => {
    await reload()
})

const reload = async () => {
    character.value = characterStore.getCharacterDetails(characterId.value)
}
</script>

<style lang="scss" scoped>

.upload-button {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

.edit-button {
    position: absolute;
    top: 0%;
    right: 0%;
    
    transform: translate(-10%, 10%);
    opacity: 80%;
}
</style>