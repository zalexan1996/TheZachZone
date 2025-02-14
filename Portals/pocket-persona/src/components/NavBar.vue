<script lang="ts" setup>
import { RouterLink } from 'vue-router'
</script>

<template>
        <ul>
            <li><RouterLink class="link" :to="{name: 'Home'}" active-class="active">Home</RouterLink></li>
            <li><RouterLink class="link" :to="{name: 'Games'}" active-class="active">Games</RouterLink></li>
            <li><RouterLink class="link" :to="{name: 'Arcana'}" active-class="active">Arcana</RouterLink></li>
            <li><RouterLink class="link" :to="{name: 'Characters'}" active-class="active">Characters</RouterLink></li>
            <li><RouterLink class="link" :to="{name: 'SocialLinks'}" active-class="active">Social Links</RouterLink></li>
        </ul>
        <hr/>
</template>

<style lang="scss" scoped>

.link {
    color: #aeaeae;
    cursor: pointer;
    text-decoration: none;
    &:hover {
        color: #d92323;
    }
}
.active {
    color: #732424;
    border-bottom: solid 1px #d92323;
    padding-bottom: 5px;
    border-radius: 6px;
}
ul {
    display: flex;
    flex-direction: row;
    list-style: none;
    margin: 0px;
    padding: 0px;
    li {
        margin-left: 5px;
        margin-right: 5px;
    }
}
</style>