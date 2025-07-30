<template>
    <div class="d-flex flex-column justify-content-start align-items-start">
        <h2>
            {{ arcana?.name }}
            <FavoriteToggle :entity-id="arcanaId!" entity-type="Arcana"/>
        </h2>
        <h5 class="text-secondary">Arcana</h5>
    </div>
</template>

<script setup lang="ts">
import FavoriteToggle from '@/components/Favorite/FavoriteToggle.vue';
import { ArcanaDto } from '@/Services/pp.api';
import { useArcanaStore } from '@/Stores/arcanaStore';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const arcanaId = ref<number>(Number.parseInt(route.params['id']!.toString()))
const arcana = ref<ArcanaDto|undefined>()

const arcanaStore = useArcanaStore();

onMounted(() => {
    arcana.value = arcanaStore.getArcanaDetails(arcanaId.value)
})
</script>

<style scoped lang="scss">

</style>