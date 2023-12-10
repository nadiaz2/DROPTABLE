import { createApp } from 'vue';
import App from './App.vue';
import { createVuetify } from 'vuetify';
import 'vuetify/styles';  // Ensure to import Vuetify styles
import router from "@/router";

const app = createApp(App);

const vuetify = createVuetify(); // Initialize Vuetify

app.use(vuetify);
app.use(router)

app.mount('#app');
