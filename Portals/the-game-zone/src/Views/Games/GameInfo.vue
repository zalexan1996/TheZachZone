
<template>
    <div class="container-fluid mt-4">
        <div class="row mx-3">
            <div class="col-12 d-flex justify-content-between align-items-start">
                <div class="d-flex flex-column justify-content-start">
                    <h2>{{ game?.name }}</h2>
                    <span>Author: {{ game?.author }}</span>
                    <span>Date Uploaded: {{ game?.uploadDate?.toLocaleDateString() }}</span>
                </div>
                <div>
                    <RouterLink :to="{name: 'Play-Game', params: {'id': game?.id}}">
                        <button class="btn btn-success">Play</button>
                    </RouterLink>
                    <RouterLink :to="{name: 'Review-Game', params: {'id': game?.id}}">
                        <button class="btn btn-outline-info mx-3">Add Review</button>
                    </RouterLink>
                    <button class="btn btn-danger" @click="deleteGame"><span class="fa fa-trash me-2"></span>Delete</button>
                </div>
            </div>
            <div class="col-12 box my-4 p-4">
                <h5>Description</h5>
                <hr/>
                <p>{{ game?.description }}</p>
                <div class="mt-4">
                    <Badge class="me-1" v-for="genre in game?.genres" severity="secondary">{{genre}}</Badge>
                </div>
            </div>
            <div class="col-12 box p-4 mb-4">
                <h5>Images</h5>
                <hr/>
                <ImageCarousel v-model="imageDatas"/>
            </div>
            <div class="col-12 col-lg-7 box p-4 mb-4">
                <h5>Reviews</h5>
                <hr/>
                <div v-for="review in reviews" class="review">
                    <h6>{{ review.author }}</h6>
                    <span>{{ review.createdOn?.toLocaleDateString() }}</span>
                    <hr/>
                    <div v-html="review.content"></div>
                </div>
            </div>
            <div class="col-m-12 col ms-lg-4 box p-4 mb-4">
                <h5>High Score</h5>
                <hr/>
                <p>This is where the high scores will be rendered</p>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { onMounted, computed, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useGameStore } from '@/Stores/gameStore'
import { Badge } from 'tzz-shared'
import GameComments from '@/Components/Games/Comments/GameComments.vue'
import ImageCarousel from '@/Components/Games/Images/ImageCarousel.vue'
import { ReviewDto } from '../../Services/tgz.api'
import '@vueup/vue-quill/dist/vue-quill.snow.css'

const route = useRoute()
const router = useRouter()
const gameStore = useGameStore()

const reviews = ref<ReviewDto[]>()

const game = computed(() => gameStore.games.length == 1 ? gameStore.games.at(0) : null)

const imageDatas = computed(() => {
    const result = gameStore.images.map(i => {
        return i.data
    })
    
    return result
})
const deleteGame = async () => {
    let id = Number.parseInt(route.params.id as string)
    await gameStore.deleteGame(id)
    router.push({name: 'Recent-Games'})
}
onMounted(async () => {
    let id = Number.parseInt(route.params.id as string)
    await gameStore.loadGames(id)
    await gameStore.getImages(id)

    reviews.value = await gameStore.getReviewsForGame(id)
})
</script>

<style scoped lang="scss">
.box {
    border: solid 1px black;
    background-color: #242c36;
    border-radius: 6px;
}

.review {
    border: solid 1px black;
    background-color: #3d2a4d;
    border-radius: 6px;
    padding: 1rem;
    img {
        max-width: 80vw;
    }
    max-width: 80vw;
}


</style>