<template>
    <div class="scrollable" v-if="socialLinkStore.socialLinks.length > 0" style="max-height: 420px;">
        <table class="w-100">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Game</th>
                    <th>Arcana</th>
                    <th>Character</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="socialLink in socialLinkStore.socialLinks">
                    <td>{{ socialLink.socialLinkId }}</td>
                    <td class="w-100">Mapped GameId</td>
                    <td class="w-100">Mapped ArcanaId</td>
                    <td class="w-100">Mapped CharacterId</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <span>No Social Links have been configured...</span>
    </div>
</template>
<script setup lang="ts">
import { useSocialLinkStore } from '@/Stores/socialLinkStore';
import { onMounted } from 'vue';

const socialLinkStore = useSocialLinkStore();

onMounted(async () => {
    await socialLinkStore.getSocialLinks();
})
</script>

<style lang="scss" scoped>
.scrollable {
    overflow-y: auto;
}
</style>