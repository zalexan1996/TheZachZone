<template>
    <RouterLink :to="entityService.buildRouterLinkFromEntity(props.entityType, props.entityId)">
        <div class="search-result d-flex flex-row justify-content-start align-items-stretch h-100 w-100 text-secondary">
            <div id="img-border">
                <img width="128" height="128" class="img-responsive" alt="icon"/>
            </div>
            <div class="d-flex flex-column align-items-stretch">
                <h4>{{ name }}</h4>
                <em>{{ props.entityType }}</em>
            </div>
        </div>
    </RouterLink>
</template>

<script setup lang="ts">
import { EntityType, useEntityService } from '@/Services/entityService';
import { computed } from 'vue';
import { RouterLink } from 'vue-router';

interface IProps {
    entityType: EntityType,
    entityId: number
}

const props = defineProps<IProps>()

const entityService = useEntityService();

const name = computed(() => entityService.getName(props.entityType, props.entityId))
</script>

<style scoped lang="scss">
a {
    text-decoration: none;
}
#img-border {
    border: solid 1px #f3f3f3;
    border-radius: 10px;
    padding: 8px;
    margin-right: 1rem;
}

.search-result {
    border: {
        top: solid 1px black;
        bottom: solid 1px black;
    }
    padding: 1rem;

    &:hover {
        filter: brightness(125%);
        background-color: #00000010;
    }
}
</style>