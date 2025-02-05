<template>
    <div id="footer">
        <div id="footer-grid">
        <span class="my-2">Made by Zach</span>
        <SiteSwitcherToggler style="right: 0px;"/>
        </div>
    </div>
</template>

<style scoped lang="scss">
#footer {
    text-align: center;
    background-color: #0d0d0dd0;
    border-top: 1px solid black;
    padding: {
        top: 6px;
        bottom: 0px;
    }
    color: #732424;
};

#footer-grid {
    display: grid;
    grid-template-columns: auto 3rem;
    align-items: center;
}
</style>

<script setup lang="ts">
import { SiteSwitcherToggler } from 'tzz-shared'
</script>