<template>
    <hr/>
    <div class="container px-4">
        <div class="row">
            <div class="col-6">
                <h4 class="text-white">Recent</h4>
                <div class="text-secondary d-flex flex-column justify-content-start align-items-stretch" v-if="activityStore.activities.length > 0">
                    <ActivityEntry :activity="activity" v-for="activity of activityStore.activities"/>
                </div>
                <em v-else class="text-secondary">No recent activity yet...</em>
            </div>
            <div class="col-6">
                <h4 class="text-white">Favorites</h4>
                <Favorites/>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import ActivityEntry from '@/components/Activity/ActivityEntry.vue';
import Favorites from '@/components/Favorite/Favorites.vue';
import { useActivityStore } from '@/Stores/activityStore';


const activityStore = useActivityStore();

</script>