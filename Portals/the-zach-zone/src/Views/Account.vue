<template>
    <div class="d-flex panel">
        <div class="grid">
            <div class="d-flex flex-column justify-content-between p-2">
                <ul class="list-unstyled w-100">
                    <li>
                        <button class="btn btn-secondary" @click="makeActive('general')" :class="{active: activePage == 'general'}">
                            General Information
                        </button>
                    </li>
                    <li>
                        <button class="btn btn-secondary" @click="makeActive('security')" :class="{active: activePage == 'security'}">
                            Security
                        </button>
                    </li>
                    <li style="height: auto"></li>
                </ul>
                <div class="d-flex flex-column justify-content-stretch">
                    <button class="btn btn-outline-danger mb-3" @click="logout_Click">Logout</button>
                    <button class="btn btn-outline-grey" @click="back_Click">Back</button>
                </div>
            </div>
            <div class="d-flex flex-column separator"></div>
            <div class="p-2 d-flex flex-column justify-content-between align-items-stretch">
                <GeneralInformationForm v-if="activePage == 'general'"/>
                <ResetPasswordForm v-else-if="activePage == 'security'"/>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router'
import { useAccountStore } from '@stores/accountStore.ts'
import GeneralInformationForm from '@components/Account/GeneralInformationForm.vue'
import ResetPasswordForm from '@components/Account/ResetPasswordForm.vue'

const activePage = ref('general')
const makeActive = (page: string) => {
    activePage.value = page;
}
const accountStore = useAccountStore();
const router = useRouter();

const logout_Click = async () => {
    await accountStore.logout();
    router.push({
        name: 'Home'
    })
}

const back_Click = async () => {
    router.back()
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
</style>