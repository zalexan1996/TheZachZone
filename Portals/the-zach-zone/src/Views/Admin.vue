<template>
    <div class="d-flex panel">
        <div class="grid">
            <div class="d-flex flex-column justify-content-between p-2">
                <ul class="list-unstyled w-100">
                    <li>
                        <button class="btn btn-secondary" @click="makeActive('pocket-persona')" :class="{active: activePage == 'pocket-persona'}">
                            Pocket Persona
                        </button>
                    </li>
                    <li>
                        <button class="btn btn-secondary" @click="makeActive('admin-access')" :class="{active: activePage == 'admin-access'}">
                            Admin Access
                        </button>
                    </li>
                    <li style="height: auto"></li>
                </ul>
            </div>
            <div class="d-flex flex-column separator"></div>
            <div class="p-2 d-flex flex-column justify-content-between align-items-stretch">
                <div v-if="activePage == 'pocket-persona'">
                    <h4>Pocket Persona</h4>
                    <div class="d-flex flex-column align-items-start">
                        <p>Actions</p>
                        <button class="btn btn-outline-danger">Seed Data</button>
                    </div>
                </div>
                <div v-else-if="activePage == 'admin-access'">
                    Admin Access
                </div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { useAccountStore } from '@/Stores/accountStore';
import { useAdminStore } from '@/Stores/adminStore';
import { ref } from 'vue';

const adminStore = useAdminStore();
const accountStore = useAccountStore();

const activePage = ref('pocket-persona')
const makeActive = (page: string) => {
    activePage.value = page;
}

</script>

<style scoped lang="scss">
@import '@/assets/styles/_theme.scss';

.grid {
    display: grid;
    grid-template-columns: 12rem 5px auto;
    width: 100%;
}
.panel {
    padding: 2rem 1rem;
    border: solid 1px $primary;
    background-color: $secondary;
    min-height: 40vh;
    min-width: 50vw;
    border-radius: 5px;
}

.separator {
    background-color: white;
    width: 1px;
}

ul li, ul button {
    width: 100%;
}

.tab-page {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: start;
}

.bordered {
    padding: 1rem;
    border: solid 1px #373f42;
    border-radius: 8px;
}
</style>