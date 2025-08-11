<template>
    <Panel class="text-secondary p-0">
        <div class="p-2 d-flex flex-row justify-content-between align-items-center" id="header-box">
            <h4 class="mb-1 mt-1">Social Link Gifts</h4>
            <button @click="isGiftModalVisible = true" class="btn btn-outline-primary"><span class="fa fa-plus"></span>Add</button>
        </div>

        <p v-if="globalStore.socialLinkStore.socialLinkGifts.length == 0" class="m-3">No gifts have been added yet...</p>
        <table v-else class="table table-striped table-bordered table-dark mt-3">
            <thead>
                <tr>
                    <th>Gift</th>
                    <th>Acquired At</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="gift of globalStore.socialLinkStore.socialLinkGifts">
                    <td>
                       {{ gift.giftName }} 
                    </td>
                    <td>
                        <i>
                       {{ gift.giftAcquiredAt }} 
                        </i>
                    </td>
                    <td>
                        <button @click="() => onDelete(gift.socialLinkGiftId!)" class="btn btn-outline-danger"><span class="fa fa-trash"></span></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </Panel>
    <AddGiftModal :is-visible="isGiftModalVisible" @cancel="isGiftModalVisible = false" @submit="onSubmit" :social-link-id="socialLinkId"/>
</template>

<script setup lang="ts">
import { AddSocialLinkGiftCommand } from '@/Services/pp.api';
import { useGlobalStore } from '@/Stores/globalStore';
import { Panel } from 'tzz-shared';
import { onMounted, ref } from 'vue';
import AddGiftModal from './AddGiftModal.vue';

interface IProps {
    socialLinkId: number
}
const props = defineProps<IProps>()

const globalStore = useGlobalStore();
const isGiftModalVisible = ref(false)

const onSubmit = (body: AddSocialLinkGiftCommand) => {
    globalStore.socialLinkStore.addSocialLinkGift(body)
    isGiftModalVisible.value = false
}

const onDelete =async (giftId: number) => {
    await globalStore.socialLinkStore.deleteSocialLinkGift(giftId)
    await globalStore.socialLinkStore.getSocialLinkGifts(props.socialLinkId)

}
onMounted(async () => {
    await globalStore.socialLinkStore.getSocialLinkGifts(props.socialLinkId)
})
</script>

<style scoped lang="scss">
#header-box {
    background-color: #33333333;
    border-bottom: solid 2px #33333366;
    padding-bottom: 8px;
}
</style>