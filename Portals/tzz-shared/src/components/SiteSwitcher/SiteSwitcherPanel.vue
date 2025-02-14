<template>
    <div id="switcher-panel" v-if="siteSwitcherStore.isSwitcherOpen" class="bordered d-flex flex-column justify-content-between align-items-stretch" >
        <ul class="list-unstyled">
            <li>
                <a href="http://localhost:8010/">
                    <button id="tgz-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/game-pattern.png')">
                        <span class="chromatic-abberation">The Game Zone</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8010/">
                    <button id="tsz-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/school-pattern.png')">
                        <span class="chromatic-abberation">The School Zone</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8020/">
                    <button id="pp-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/persona-pattern.png')">
                        <span class="chromatic-abberation">Pocket Persona</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8010/">
                    <button id="cz-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/crapazon-pattern.png')">
                        <span class="chromatic-abberation">Crapazon</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8010/">
                    <button id="al-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/agile-pattern.png')">
                        <span class="chromatic-abberation">Agile Life</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8030/">
                    <button id="pe-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/earth-pattern.png')">
                        <span class="chromatic-abberation">Planet Earth</span>
                    </button>
                </a>
            </li>
            <li>
                <a href="http://localhost:8000/">
                    <button id="tzz-button" class="btn btn-outline-secondary chromatic-abberation" style="background-image: url('/wavy-pattern.png')">
                        <span class="chromatic-abberation">The Zach Zone</span>
                    </button>
                </a>
            </li>
        </ul>
    </div>
</template>

<style scoped lang="scss">
.chromatic-abberation {
    &:hover {
        span {
            animation: pulse ease 200ms  infinite;
        }
    }
}

#tzz-button {
    &:hover {
        text-shadow: 3px 2px 2px #7107bd, 0px -1px 3px #47a36e,-3px 0px 2px #ce13ce;
        border: solid 1px #8455aa;
    }
}

#tgz-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(0, 206, 233), 0px -1px 3px rgb(12, 215, 251),-3px 0px 2px rgb(0, 47, 255);
        border: solid 1px rgb(0, 206, 233);
    }
}

#tsz-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(0, 206, 233), 0px -1px 3px rgb(8, 0, 117, 0.5),-3px 0px 2px rgb(51, 72, 170);
        border: solid 1px rgb(0, 43, 233);
    }
}

#cz-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(93, 107, 14), 0px -1px 3px rgb(8, 0, 117),-3px 0px 2px rgb(239, 255, 94);
        border: solid 1px rgb(232, 241, 95);
    }
}
#pp-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(209, 0, 0), 0px -1px 3px rgb(0, 255, 234),-3px 0px 2px rgb(132, 0, 255);
        border: solid 1px rgb(209, 0, 0);
    }
}

#pe-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(0, 91, 209), 0px -1px 3px rgb(0, 255, 13),-3px 0px 2px rgb(132, 0, 255);
        border: solid 1px rgb(0, 255, 13);
    }
}

#al-button {
    &:hover {
        text-shadow: 3px 2px 2px rgb(0, 195, 209), 0px -1px 3px rgb(197, 16, 16),-3px 0px 2px rgb(132, 0, 255);
        border: solid 1px rgb(0, 255, 255);
    }
}

ul button {
    width: 100% !important;
    font-size: 24px;
    padding: {
        top: 2rem;
        bottom: 2rem;
    }
    border-radius: 0px;
    color: white;
    background-size: 600px 400px;
    border: none;

    &:hover {
        background-size: 800px 500px;
        background-position:-60px -20px;
    }

}
.bordered {
    background: #25292e;
    border-left: solid 1px #0e0d0d;
}

@keyframes pulse { 
    0%{ opacity:.9;transform: skewX(.5deg);}
    25%{ opacity:1;}
    50%{opacity:.9;}
    75%{ opacity:1; transform:skewX(.5deg);}
    100%{opacity:.9;}
}
</style>

<script setup lang="ts">
import { useSiteSwitcherStore } from '@stores/siteSwitcherStore.ts'

const siteSwitcherStore = useSiteSwitcherStore();
</script>