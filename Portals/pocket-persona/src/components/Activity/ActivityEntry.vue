<template>
    <div id="activity-container" v-if="props.activity" class="d-flex flex-row w-100 justify-content-between align-items-center">
        <span class="text-secondary">{{ props.activity.activity }}</span>
        <span>{{ props.activity.entityType }}</span>
        <span :title="'Id: ' + props.activity.entityId">
            {{ entityService.getName(props.activity.entityType!, props.activity.entityId!) }}
            <FavoriteToggle :entity-id="props.activity.entityId!" :entity-type="props.activity.entityType!"/>
        </span>
    </div>
</template>

<script setup lang="ts">
import { useEntityService } from '@/Services/entityService';
import { ActivityLog } from '@/Services/pp.api';
import FavoriteToggle from '../Favorite/FavoriteToggle.vue';

interface IProps {
    activity: ActivityLog
}
const props = defineProps<IProps>()
const entityService = useEntityService()
</script>

<style scoped lang="scss">
span {
    color: #bebebe;
    width: 100%;
}

#activity-container {
    background-color: #33333344;
    margin-top: 2px;
    margin-bottom: 2px;
    padding: 3px;
}
</style>