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
    background-color: #242c36;
    border-top: 1px solid black;
    padding: {
        top: 6px;
        bottom: 0px;
    }
    color: rgb(165, 165, 165);
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