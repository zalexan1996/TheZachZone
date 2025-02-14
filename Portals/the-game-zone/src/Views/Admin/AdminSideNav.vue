<template>
    <ul class="list-unstyled">
        <li>
            <RouterLink active-class="active" :to="{name: 'Admin/UserManagement'}" class="link">User Management</RouterLink>
        </li>
        <li>
            <RouterLink active-class="active" :to="{name: 'Admin/Metadata'}" class="link">Metadata</RouterLink>
        </li>
    </ul>
</template>
<style lang="scss" scoped>
.active {
    background-color: #242c36;
    color: white;
}
ul {
    margin-bottom: 0px;
    border-right: 1px solid gray;

    li {
        box-sizing: border-box;
        padding-left: 3px;
        width: 100%;
        &:hover {
            box-sizing: border-box;
            border-bottom: 1px solid white;
        }
    }
}

.link {
    display: block;
    color: white;
    text-decoration: none;
    width: 100%;
    height: 100%;
    padding: {
        top: 8px;
        bottom: 8px;
        left: 12px;
        right: 12px;
    }
}
</style>