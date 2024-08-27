<template>
    <div class="d-flex flex-column justify-content-start align-items-center">
        <InputText for="firstName" v-model="firstName" placeholder="Provide a first name..." label="First Name" title="Provide a last name..."/>
        <InputText for="lastName" v-model="lastName" placeholder="Provide a last name..." label="Last Name" title="Provide a last name..."/>
        <InputText for="email" v-model="email" label="Email" :disabled="true" title="Your email is read only."/>
    <button :disabled="!(isFormValid && isFormDirty)" class="btn btn-success" @click="save_Click">Save Changes</button>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate'
import * as yup from 'yup'
import { useAccountStore } from '@stores/accountStore.ts'
import { InputText, useToastStore } from 'tzz-shared'

const accountStore = useAccountStore();
const toastStore = useToastStore();
const form = useForm({
    validationSchema: {
        firstName: yup.string().required(),
        lastName: yup.string().required()
    }
})

const [firstName] = form.defineField('firstName')
const [lastName] = form.defineField('lastName')
const email = ref<string>('')
const isFormValid = useIsFormValid();
const isFormDirty = useIsFormDirty();

const save_Click = async () => {
    accountStore.updateGeneralInformation({
        firstName: firstName.value,
        lastName: lastName.value
    }).then(_ => {
        toastStore.push({
            title: 'Changes updated',
            body: 'Your general information has been updated.',
            duration: 3000,
            severity: 'success'
        })
    }).catch(_ => {
        toastStore.push({
            title: 'Changes not updated',
            body: 'An error occurred while trying to save your changes.',
            duration: 3000,
            severity: 'danger'
        })
    })
}

onMounted(async () => {
    let generalInformation = await accountStore.getGeneralInformation();
    firstName.value = generalInformation.firstName
    lastName.value = generalInformation.lastName
    email.value = generalInformation.email
})

</script>


<style scoped lang="scss">
.form-group {
    display: flex;
    flex-direction: column;
    align-items: start;
    margin-bottom: 1rem;

    input {
        min-width: 20rem;
    }
}
</style>