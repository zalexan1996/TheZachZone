<template>
    <div class="scrollable" v-if="arcanaStore.arcana.length > 0" style="max-height: 420px;">
        <table class="w-100">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="arcana in arcanaStore.arcana">
                    <td>{{ arcana.id }}</td>
                    <td class="w-100">{{ arcana.name }}</td>
                    <td>
                        <span class="fa fa-trash clickable" title="Delete" @click="removeArcana(arcana.id)"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <span>No Arcana have been configured...</span>
    </div>
</template>
<script setup lang="ts">
import { useArcanaStore } from '@/Stores/arcanaStore';
import { onMounted } from 'vue';

const arcanaStore = useArcanaStore();

const removeArcana = async (id: number) => {
    await arcanaStore.removeArcana(id)
    await arcanaStore.getArcana()
}
onMounted(async () => {
    await arcanaStore.getArcana();
})
</script>

<style lang="scss" scoped>
.scrollable {
    overflow-y: auto;
}
</style>