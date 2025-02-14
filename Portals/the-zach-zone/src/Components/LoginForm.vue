<template>
    <div class="bordered text-center">
        <div class="d-flex justify-content-start flex-column pe-4">
            <h4>Login</h4>
            <hr/>
            <div>
                <input :readonly="isEmailRegistered != undefined" v-model="email" class="form-control" placeholder="Provide your email..."/>
                <input v-if="isEmailRegistered != undefined" v-model="password" class="form-control mt-2" placeholder="Provide your password..." type="password"/>
                <input v-if="isEmailRegistered == false" v-model="confirmPassword" class="form-control mt-2" placeholder="Confirm your password..." type="password"/>
                <span v-if="loginFailed" class="text-danger">Login failed. Check your password and try again.</span>
            </div>
            <div id="button-group" class="flex-column d-flex justify-content-center">

                <button v-if="isEmailRegistered == undefined" type="button" :disabled="!email" class="btn btn-primary mt-4" @click="checkEmailStatus_Click">
                    Continue
                </button>
                
                <button v-else-if="!isEmailRegistered" class="btn btn-success" @click="createAccount_Click">Create Account</button>
                <button v-else class="btn btn-success" @click="login_Click">Login</button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAccountStore } from '@stores/accountStore';
import { CreateAccountCommand } from '@services/apiService';
import { useRoute, useRouter } from 'vue-router';
import { useToastStore } from 'tzz-shared'

const accountStore = useAccountStore();
const toastStore =  useToastStore()
const route = useRoute();
const router = useRouter();
const email = ref('')
const password = ref('')
const confirmPassword = ref('')

const isEmailRegistered = ref<boolean|undefined>()
const loginFailed = ref(false)
const checkEmailStatus_Click = async () => {
    isEmailRegistered.value = await accountStore.doesUserExist(email.value)
}

const createAccount_Click = async () => {
    let result = await accountStore.createAccount({
        email: email.value,
        password: password.value,
        confirmPassword: confirmPassword.value,
    } as CreateAccountCommand)
    
    if (result.isValid) {
        toastStore.push({
            title: 'Account Created.',
            body: 'Please login to continue.',
            duration: 3000,
            severity: 'success'
        })
    }
    else {
        toastStore.push({
            title: 'Failed to create account.',
            body: 'Please try again later.',
            duration: 3000,
            severity: 'danger'
        })
    }

    router.push('/')
}

const login_Click = async () => {
    const result = await accountStore.login(email.value, password.value)
    if (result) {
        await router.push({
            name: 'Home'
        })
    }
    else {
        loginFailed.value = true;
    }
}

onMounted(() => {
    if (route.query.client_id) {
        email.value = route.query.client_id.toString()
        checkEmailStatus_Click()
    }
})
</script>

<style scoped lang="scss">
.bordered {
    border: solid 1px #373f42;
    border-radius: 6px;
    background-color: #1d2629;
    padding: 2rem;
    color: #eab1f8;
}

.separator {
    background-color: #433c4b;
    width: 1px;
}

#button-group {
    margin-top: 2rem;
}
</style>
