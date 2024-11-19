<template>
    <div class="d-flex flex-column">
        <h3>User Management</h3>
        <table class="table table-striped table-dark">
            <thead>
                <tr>
                    <th>User Id</th>
                    <th>Email</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Roles</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in accountStore.users">
                    <td>{{ user.userId }}</td>
                    <td>{{ user.email }}</td>
                    <td>{{ user.firstName }}</td>
                    <td>{{ user.lastName }}</td>
                    <td>{{ user.roles }}</td>
                    <td>
                        <div class="d-flex flex-row">
                            <span v-if="!user.roles.find(x => x == 'Admin')" class="fa fa-plus text-warning mx-2" title="Grant Admin" @click="accountStore.grantAdmin(user.userId)"></span>

                            <span v-else class="fa fa-minus text-warning mx-2" title="Revoke Admin" @click="accountStore.revokeAdmin(user.userId)"></span>

                            <span class="fa fa-trash text-danger mx-2" title="Delete User" @click="accountStore.deleteUser(user.userId)"></span>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useAccountStore } from '@stores/accountStore.ts'

const accountStore = useAccountStore();


onMounted(async () => {
    await accountStore.listUsers()
})
</script>

<style scoped lang="scss">

span.fa {
    cursor: pointer;
}
th, td {
    padding-left: 4rem;
    text-align: left;
}
</style>