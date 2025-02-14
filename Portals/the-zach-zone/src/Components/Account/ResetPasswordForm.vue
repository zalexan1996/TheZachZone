<template>
    <div class="tab-page">
        <InputText for="password" v-model="password" label="New Password" placeholder="Provide a new password..." type="password"/>
        <InputText for="confirmPassword" v-model="confirmPassword" label="Confirm Password" placeholder="Provide a new password..." type="password"/>
        <button :disabled="!(isFormValid && isFormDirty)" class="btn btn-outline-success" @click="resetPassword">Reset Password</button>
    </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { InputText, useToastStore } from 'tzz-shared';
import { useForm, useIsFormDirty, useIsFormValid, ErrorMessage } from 'vee-validate'
import { useAccountStore } from '@/Stores/accountStore.ts'
import { ResetPasswordCommand } from '@/Services/apiService.ts'
import * as yup from 'yup'
import { useRouter } from 'vue-router'

const router = useRouter()
const accountStore = useAccountStore()
const toastStore = useToastStore();
const form = useForm({
    validationSchema: yup.object().shape({
        password: yup.string().min(10, 'Your password must be at least 10 characters').required('Your password is required'),
        confirmPassword: yup.string().min(10, 'Your password must be at least 10 characters').required('Your confirm password is required').oneOf([yup.ref('password')], 'Passwords do not match'),
    })
})

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()
const [password] = form.defineField('password')
const [confirmPassword] = form.defineField('confirmPassword')
const resetToken = ref<string|undefined>()

const resetPassword = async () => {
    try
    {
        await accountStore.resetPassword({
            resetToken: resetToken.value,
            newPassword: password.value,
            confirmPassword: confirmPassword.value
        } as ResetPasswordCommand)

        await accountStore.logout()

        router.push({
            'name': 'Home'
        })
        toastStore.push({
            title: 'Password Updated',
            body: 'Your password has been updated! Login with your new password.',
            severity: 'success',
            duration: 3000
        })
    }
    catch (e) {

        console.error(e)
        toastStore.push({
            title: 'Password Reset Failed',
            body: 'An error occurred while trying to change your password.',
            severity: 'danger',
            duration: 3000
        })
    }
}

onMounted(async () => {
    resetToken.value = await accountStore.getPasswordResetToken()
})
</script>