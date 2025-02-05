<template>
    <div class="h-100 d-flex flex-column">
        <h4 class="text-white">Games</h4>
        <Panel class="h-100">
            <GamesTable/>
        </Panel>
        <hr class="my-4"/>
        <h4 class="text-white">Add game</h4>
        <Panel>
            <AddGameForm/>
        </Panel>
    </div>
</template>
<script setup lang="ts">
import AddGameForm from '@components/Games/AddGameForm.vue';
import GamesTable from '@components/Games/GamesTable.vue';
import { Panel } from 'tzz-shared'
</script>