import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: () => import('@/components/game1/game1_main.vue') },
    { path: '/pause', name: 'pause', component: () => import('@/components/others/pause.vue') },
    { path: '/game1_main', name: 'game1_main', component: () => import('@/components/game1/game1_main.vue') },
    { path: '/game1_album/', name: 'game1_album', component: () => import('@/components/game1/game1_album.vue') },
    { path: '/game1_album', name: 'game1_album', component: () => import('@/components/game1/game1_album.vue') },
    { path: '/game1_browser', name: 'game1_browser', component: () => import('@/components/game1/game1_browser.vue') },
    { path: '/game1_contact', name: 'game1_contact', component: () => import('@/components/game1/game1_contact.vue') },
    { path: '/game1_message', name: 'game1_message', component: () => import('@/components/game1/game1_message.vue') },
    { path: '/contact_tom', name: 'contact_tom', component: () => import('@/components/contacts/contact_tom.vue') },
    // Add more routes as needed
  ],
});

export default router;
