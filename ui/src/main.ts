import "@/styles/index.css"
import "@/styles/main.css"
import '@progress/kendo-theme-default/dist/all.css';

import { createApp } from "vue"
import { createPinia } from "pinia"
import App from "@/App.vue"
import { Tooltip } from 'bootstrap'

import Notifications from '@kyvg/vue3-notification'

// You can use the following starter router instead of the default one as a clean starting point
// import router from "./router/starter";
// import router from "./router/templateindex";
import router from "./router/index";

// Template components
import BaseBlock from "@/components/BaseBlock.vue";
import BaseBackground from "@/components/BaseBackground.vue";
import BasePageHeading from "@/components/BasePageHeading.vue";

// Template directives
import clickRipple from "@/directives/clickRipple";

// Bootstrap framework
import * as bootstrap from "bootstrap";
window.bootstrap = bootstrap;

import { createHead } from "@vueuse/head"
// import { createRouter, createWebHistory } from "vue-router"


// import { setupLayouts } from "virtual:generated-layouts"
// import generatedRoutes from "~pages"

// import { configRouter } from "@/routing"

const app = createApp(App)
const head = createHead()

// Register global components
app.component("BaseBlock", BaseBlock);
app.component("BaseBackground", BaseBackground);
app.component("BasePageHeading", BasePageHeading);

// Register global directives
app.directive("click-ripple", clickRipple);

app.directive('tooltip', Tooltip as any)
// const routes = setupLayouts(generatedRoutes)

// export const router = configRouter(createRouter({
//     history: createWebHistory(),
//     routes,
// }))

const pinia = createPinia()



app
    .use(head)
    .use(router)
    .use(pinia)
    .use(Notifications)
    .mount('#app')
