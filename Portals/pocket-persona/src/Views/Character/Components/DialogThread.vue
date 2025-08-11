<template>
    <Panel class="text-secondary p-0">
        <div class="p-2 d-flex flex-row justify-content-between align-items-center" id="header-box">
            <h4 class="mb-1 mt-1">Social Link Dialog</h4>
            <button class="btn btn-outline-primary" @click="isDialogModalVisible = true"><span class="fa fa-plus"></span>Add</button>
        </div>
        
        <p v-if="globalStore.socialLinkStore.socialLinkDialog.length == 0" class="m-3">No dialogue entries have been added yet...</p>
        <div v-else class="px-3 d-flex flex-column justify-content-start align-items-stretch pb-4" style="max-height: 600px; overflow-y: scroll;">
            <div v-for="(dtos, rank) of globalStore.socialLinkStore.groupedDialog" class="dialog-rank-group">
                <p>Rank {{ rank }}</p>
                <hr/>
                <div v-for="d in dtos.sort((x, y) => x.order! - y.order!)" class="dialog-entry" :style="backgroundColorStyle">
                    <div class="d-flex flex-row justify-content-between align-items-center">
                        <p>{{ d.order }} - {{ d.text }}</p>
                        <button @click="() => onDelete(d.socialLinkDialogId!)" class="btn btn-danger">
                            <i class="fa fa-trash px-1"></i>
                        </button>
                    </div>
                    <i v-if="d.optionalRequirement">Requires : {{ d.optionalRequirement }}</i>
                </div>
            </div>
        </div>
    </Panel>
    <AddDialogModal :is-visible="isDialogModalVisible" @cancel="isDialogModalVisible = false" @submit="onSubmit" :social-link-id="socialLinkId"/>
</template>

<script setup lang="ts">
import { AddSocialLinkDialogCommand, SocialLinkDto } from '@/Services/pp.api';
import { useGlobalStore } from '@/Stores/globalStore';
import { Panel } from 'tzz-shared';
import { computed, onMounted, ref } from 'vue';
import AddDialogModal from './AddDialogModal.vue';

interface IProps {
    socialLinkId: number
}
const props = defineProps<IProps>()

const globalStore = useGlobalStore();
const socialLink = ref<SocialLinkDto>()
const isDialogModalVisible = ref<boolean>(false)

const backgroundColorStyle = computed(() => {
    let gameColor = globalStore.gameStore.getGameDetails(socialLink.value?.gameId ?? -1)?.primaryColor
    return {
        backgroundColor: toHexWithAlpha(gameColor ?? 'White', 0.4),
        border: `solid 1px ${toHexWithAlpha(gameColor ?? 'White', 0.7)}`
    }
})

function toHexWithAlpha(color: string, alpha = 1) {
    const ctx: CanvasRenderingContext2D = document.createElement("canvas").getContext("2d")!;
    ctx.fillStyle = color;
    const computedColor = ctx.fillStyle;

    ctx.fillStyle = computedColor;
    ctx.fillRect(0, 0, 1, 1);
    const [r, g, b] = ctx.getImageData(0, 0, 1, 1).data;

    const a = Math.round(alpha * 255);

    const toHex = (n: number) => n.toString(16).padStart(2, '0');

    return `#${toHex(r)}${toHex(g)}${toHex(b)}${toHex(a)}`;
}

const onSubmit = async (formData: AddSocialLinkDialogCommand) => {
    isDialogModalVisible.value = false

    await globalStore.socialLinkStore.addSocialLinkDialog(formData);
}

const onDelete = async (id: number) => {
    await globalStore.socialLinkStore.deleteSocialLinkDialog(id);
    globalStore.socialLinkStore.getSocialLinkDialog(props.socialLinkId)
}

onMounted(() => {
    socialLink.value = globalStore.socialLinkStore.getSocialLinkDetails(props.socialLinkId)
    globalStore.socialLinkStore.getSocialLinkDialog(props.socialLinkId)
})
</script>

<style scoped lang="scss">
#header-box {
    background-color: #33333333;
    border-bottom: solid 2px #33333366;
    padding-bottom: 8px;
}
.dialog-entry {
    border-radius: 12px;
    padding-left: 1rem;
    margin-top: 12px;
    p {
        opacity: 100%;
        margin: 0px;
        padding: 0px;
    }
}

.dialog-rank-group {
    margin-top: 2rem;
    hr {
        margin-top: 6px;
        margin-bottom: 12px;
    }
    p {
        margin-bottom: 0px;
    }
}
</style>