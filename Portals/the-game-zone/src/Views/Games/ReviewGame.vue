<template>
    <div class="container">
        <h3>Review - Game</h3>
        <hr class="mb-4"/>
        <h6 class="mb-4">Please <i class="text-info" style="cursor: pointer" @click="addModal">read the rules</i> before leaving a review </h6>
        <div class="row">
            <QuillEditor v-model:content="reviewContent" style="min-height: 50vh;" theme="snow" toolbar="full" ref="editor" :modules="quillModules"></QuillEditor>
        </div>
        <div class="row">
            <div class="d-flex flex-row justify-content-center mt-4">
                <button class="btn btn-success me-2" @click="submitReview">Submit</button>
                <button class="btn btn-outline-danger" @click="discard">Discard</button>

            </div>
        </div>
    </div>
    <div class="modal" tabindex="-1" ref="rulesModal">
        <div class="modal-dialog">
            <div class="modal-content bg-dark">
                <div class="modal-header">
                    <h5 class="modal-title">Rules for Reviews</h5>
                    <a href="#" class="text-white btn-close" data-bs-dismiss="modal" aria-label="Close"><span class="fa fa-x"></span></a>
                </div>
                <div class="modal-body">
                    <p>Use the editor below to write your review. Share your thoughts, experiences, and feedback to help others! Be sure to provide as much detail as possible to make your review helpful. Please adhere to the following rules when writing your review.</p>
                    <ol>
                        <li><b>Be Respectful</b>Always express your opinion in a polite and considerate manner, even if you're sharing negative feedback. Remember, everyone’s experience may differ.</li>
                        <li><b>Stay Relevant</b>Focus on the product, service, or experience you're reviewing. Avoid discussing unrelated topics or personal grievances that aren't tied to the review.</li>
                        <li><b>Be Honest and Specific</b>Share genuine thoughts and provide specific examples to help others understand your perspective. Clear details make your review more useful.</li>
                        <li><b>Avoid Offensive Language</b>Keep your language professional and avoid using profanity, insults, or overly harsh statements. Aim for constructive criticism if needed.</li>
                        <li><b>Respect Privacy</b>Do not share private or confidential information about yourself or others in your review. Keep the content appropriate for a public audience.</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts" setup>
import { onMounted, ref} from 'vue'
import { Modal} from 'bootstrap'
import { useGameStore } from '@stores/gameStore';
import { useRoute, useRouter } from 'vue-router';
import BlotFormatter from 'quill-blot-formatter';

const route = useRoute()
const router = useRouter()
const reviewContent = ref<any|undefined>()
const gameId = ref<number|undefined>()
const rulesModal = ref<HTMLDivElement|undefined>()
const modal = ref<Modal|undefined>()
const gameStore = useGameStore();

const editor = ref()
const quillModules = {
    name: 'blotFormatter',
    module: BlotFormatter
}
const addModal = () => {
    if (modal.value == undefined) {
        modal.value = new Modal(rulesModal.value, {

        })
    }
    modal.value.show()
}

const submitReview = async () => {
    if (gameId.value && reviewContent.value) {
        await gameStore.submitReview(gameId.value, editor.value.getHTML())
        await router.push({name: 'GameInfo', params: {id: gameId.value}})
    }
}

const discard = async () => {
    await router.push({name: 'GameInfo', params: {id: gameId.value}})
}

onMounted(async () => {
    gameId.value = Number.parseInt(route.params.id.toString())
})
</script>

<style scoped lang="scss">
ol {
    padding-bottom: 1rem;
    li {
        margin-bottom: 0.5rem;
        b {
        display: block;
        }
    }
}

</style>