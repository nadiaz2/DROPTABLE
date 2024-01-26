import { RouterLink, RouterView } from 'vue-router'

<template>
  <transition name="fade" mode="out-in">
    <RouterView :key="$route.fullPath" />

  </transition>
</template>

<script>


import 'vuetify/styles';
import { RouterLink, RouterView } from 'vue-router'

export default {
  name: 'App',
  methods: {
    goToPage(page) {
      console.log(page);
    }
  }


}
</script>


<style scoped>
.app-card {
  border-radius: 10px;
  height: 70px;
  width: 70px;
}

.background {
  background-image: url("https://images.pexels.com/photos/1366919/pexels-photo-1366919.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1");
}

.app-card {
  margin: auto;
}
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active in <2.1.8 */ {
  opacity: 0;
}
</style>
