<template>
    <Panel style="min-height: 300px" class="text-secondary p-0">
        <div class="p-2 d-flex flex-row justify-content-between align-items-center" id="header-box">
            <h4 class="mb-1 mt-1">Social Link Gifts</h4>
            <button class="btn btn-outline-primary"><span class="fa fa-plus"></span>Add</button>
        </div>
    </Panel>
</template>

<script setup lang="ts">
import { SocialLinkDto } from '@/Services/pp.api';
import { useGlobalStore } from '@/Stores/globalStore';
import { Panel } from 'tzz-shared';
import { computed, onMounted, ref } from 'vue';

interface IProps {
    socialLinkId: number
}
const props = defineProps<IProps>()

const globalStore = useGlobalStore();
const socialLink = ref<SocialLinkDto>()

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
</style>