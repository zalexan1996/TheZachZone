<template>
    <a href="#" @click="onClick"><span :class="classes"></span></a>
</template>

<script setup lang="ts">
import { EntityType } from '@/Services/entityService';
import { useFavoriteStore } from '@/Stores/favoriteStore';
import { onMounted, ref, watch} from 'vue';


interface IProps {
    entityType: EntityType,
    entityId: number
}
const props = defineProps<IProps>()

const favoriteStore = useFavoriteStore()

const classes = ref<string[]>([])

const setClasses = () => {
    if (favoriteStore.isFavorite(props.entityType, props.entityId)) {
        classes.value = [
            'fa', 'fa-star'
        ]
    }
    else {
        classes.value = [
            'fa-regular', 'fa-star'
        ]
    }
}
watch(() => favoriteStore.favorites, setClasses, {
    deep: true
})

const onClick = () => {
    favoriteStore.toggleFavorite(props.entityType, props.entityId)
}

onMounted(setClasses)
</script>
<style scoped lang="scss">
a {
    text-decoration: none;
    color: yellow;
}
</style>