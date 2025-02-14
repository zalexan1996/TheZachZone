<template>
    <div class="d-flex flex-column justify-content-start comment-border">
        <div class="d-flex flex-column mb-3">
            <div class="d-flex flex-row justify-content-between">
                <h6 class="mb-0">
                    {{ $props.commentDto.authorName }}
                </h6>
                <span v-if="showDeleteButton" class="fa fa-trash text-danger" style="cursor: pointer;" @click="deleteComment"></span>
            </div>
            <span style="font-weight: normal; color: gray;">{{  $props.commentDto.postedOn?.toLocaleDateString() }}</span>
            <hr class="text-white mt-1 mb-3"/>
        </div>
        <p>{{ $props.commentDto.content }}</p>
    </div>

</template>

<script setup lang="ts">
import { CommentDto } from '@/Services/apiService.ts'
import { useGameStore } from '@/Stores/gameStore'
import { computed, onMounted, ref } from 'vue';
import { useAccountStore } from '@stores/accountStore';
import { GeneralInformationDto} from '@services/tzz.api.ts'

const gameStore = useGameStore();
const accountStore = useAccountStore()
interface IProps {
    commentDto: CommentDto
}

const props = defineProps<IProps>()
const userInfo = ref<GeneralInformationDto|undefined>()

const showDeleteButton = computed(() => {
    return props.commentDto.authorId == userInfo.value?.id
})

const deleteComment = async () => {
    if (!!props.commentDto.id) {
        await gameStore.deleteComment(props.commentDto.id)
        await gameStore.loadComments(props.commentDto.gameInfoId!)
    }
}

onMounted(async () => {
    userInfo.value = await accountStore.getUserInfo()
})
</script>

<style scoped lang="scss">
.comment-border {
    border: solid 1px black;
    border-radius: 12px;
    background-color: #1f2627;
    padding: {
        top: 1rem;
        bottom: 1rem;
        left: 2rem;
        right: 2rem;
    }
}
</style>