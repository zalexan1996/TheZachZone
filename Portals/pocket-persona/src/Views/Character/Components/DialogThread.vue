<template>
    <Panel class="text-secondary p-0">
        <div class="p-2 d-flex flex-row justify-content-between align-items-center" id="header-box">
            <h4 class="mb-1 mt-1">Social Link Dialog</h4>
            <button class="btn btn-outline-primary" @click="isDialogModalVisible = true"><span class="fa fa-plus"></span>Add</button>
        </div>
        <div class="px-3 d-flex flex-column justify-content-start align-items-stretch" style="max-height: 600px; overflow-y: scroll;">
            <div class="dialog-rank-group">
                <p>Rank 1</p>
                <hr/>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>Hello there.</p>
                </div>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>Hi.</p>
                </div>
            </div>
            <div class="dialog-rank-group">
                <p>Rank 2</p>
                <hr/>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>Loren Ipsum.</p>
                </div>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>poopen sharten farten</p>
                </div>
            </div>
            <div class="dialog-rank-group">
                <p>Rank 3</p>
                <hr/>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>Hello there.</p>
                </div>
                <div class="dialog-entry" :style="backgroundColorStyle">
                    <p>Hi.</p>
                </div>
            </div>
        </div>
    </Panel>
    <AddDialogModal :is-visible="isDialogModalVisible" @cancel="isDialogModalVisible = false" @submit="isDialogModalVisible = false"/>
</template>

<script setup lang="ts">
import { SocialLinkDto } from '@/Services/pp.api';
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


onMounted(() => {
    socialLink.value = globalStore.socialLinkStore.getSocialLinkDetails(props.socialLinkId)
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