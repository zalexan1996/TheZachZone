<template>
    <div class="dropup">

        <!-- Drop-Up Button -->
        <button class="btn btn-outline-secondary mx-2" data-bs-toggle="dropdown">
            Create <span class="fa fa-arrow-up-from-bracket"></span>
        </button>

        <!-- Drop-Up menu -->
        <ul class="dropdown-menu list-unstyled p-0">
            <li>
                <button 
                    class="w-100 btn btn-outline-success btn-link text-dark text-decoration-none" 
                    @click="showAddGameModal = true">Game</button>
            </li>
            <li>
                <button class="w-100 btn btn-outline-success btn-link text-dark text-decoration-none"
                    @click="showAddArcanaModal = true">
                    Arcana
                </button>
            </li>
            <li>
                <button class="w-100 btn btn-outline-success btn-link text-dark text-decoration-none"
                    @click="showAddCharacterModal = true">
                    Character
                </button>
            </li>
        </ul>
    </div>


    <!-- Modals -->
    <AddGameModal
        :is-visible="showAddGameModal" 
        @cancel="showAddGameModal = false" 
        @submit="showAddGameModal = false"/>
    <AddArcanaModal
        :is-visible="showAddArcanaModal"
        @cancel="showAddArcanaModal = false"
        @submit="showAddArcanaModal = false"/>
    <AddCharacterModal
        :is-visible="showAddCharacterModal"
        @cancel="showAddCharacterModal = false"
        @submit="showAddCharacterModal = false"/>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import AddGameModal from './Modals/AddGameModal.vue'
import AddArcanaModal from './Modals/AddArcanaModal.vue'
import AddCharacterModal from './Modals/AddCharacterModal.vue'
import { useGlobalStore } from '@/Stores/globalStore'
const showAddGameModal = ref(false)
const showAddArcanaModal = ref(false)
const showAddCharacterModal = ref(false)

const globalStore = useGlobalStore();
</script>

<style scoped lang="scss">

</style>