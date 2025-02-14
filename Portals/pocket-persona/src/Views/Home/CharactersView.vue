<script setup lang="ts">
import CharactersTable from '@/components/Characters/CharactersTable.vue';
import AddCharacterForm from '@/components/Characters/AddCharacterForm.vue'
import { Panel } from 'tzz-shared'

</script>

<template>
    <div id="container">
        <div id="row1">
            <h4 class="text-white">Characters</h4>
            <Panel>
                <CharactersTable/>
            </Panel>
        </div>
        <hr class="my-4"/>
        <div  id="row2">
            <h4 class="text-white">Add Character</h4>
            <Panel>
                <AddCharacterForm/>
            </Panel>
        </div>
    </div>
</template>

<style lang="scss" scoped>
#container {
    display: flex;
    flex-direction: column;
    width: 100%;
}
#row1 {
    display: flex;
    flex-direction: column;
    flex: 1;
    width: 100%;
}
#row2 {
    display: flex;
    flex-direction: column;
    flex: 1;
    width: 100%;
}
</style>