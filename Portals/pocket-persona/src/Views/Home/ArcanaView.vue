<script setup lang="ts">
import ArcanaTable from '@/components/Arcana/ArcanaTable.vue';
import AddArcanaForm from '@/components/Arcana/AddArcanaForm.vue'
import { Panel } from 'tzz-shared'


</script>

<template>
    <div id="container">
        <div id="row1">
            <h4 class="text-white">Arcana</h4>
            <Panel>
                <ArcanaTable/>
            </Panel>
        </div>
        <hr class="my-4"/>
        <div  id="row2">
            <h4 class="text-white">Add Arcana</h4>
            <Panel>
                <AddArcanaForm/>
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