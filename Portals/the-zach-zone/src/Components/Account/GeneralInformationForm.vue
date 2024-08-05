<template>
    <div class="d-flex flex-column justify-content-start align-items-center">
    <div class="form-group"> 
        <label class="form-label">First Name:</label>
        <input v-model="firstName" class="form-control" placeholder="Provide a first name..."/>
    </div>
    <div class="form-group"> 
        <label class="form-label">Last Name:</label>
        <input v-model="lastName" class="form-control" placeholder="Provide a last name..."/>
    </div>
    <div class="form-group"> 
        <label class="form-label">Email:</label>
        <input readonly v-model="accountStore.userInfo.email" class="form-control"/>
    </div>
    <button :disabled="!(isFormValid && isFormDirty)" class="btn btn-success" @click="save_Click">Save Changes</button>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate'
import * as yup from 'yup'
import { useAccountStore } from '@stores/accountStore.ts'

const accountStore = useAccountStore();
const form = useForm({
    validationSchema: {
        firstName: yup.string().required(),
        lastName: yup.string().required()
    }
})

const [firstName] = form.defineField('firstName')
const [lastName] = form.defineField('lastName')

const isFormValid = useIsFormValid();
const isFormDirty = useIsFormDirty();

const save_Click = async () => {
    await accountStore.updateGeneralInformation({
        firstName: firstName.value,
        lastName: lastName.value
    })
}

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