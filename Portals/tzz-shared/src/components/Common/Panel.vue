<template>
    <div class="panel">
        <slot/>
    </div>
</template>
<style lang="scss" scoped>
.panel {
    
    border-radius: 8px;
    border: solid 1px #7d9ba0;
    padding: {
        left: 2rem;
        right: 2rem;
        top: 1rem;
        bottom: 1rem;
    }
}
</style>