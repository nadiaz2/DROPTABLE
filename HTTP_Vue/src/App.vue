import { RouterLink, RouterView } from 'vue-router'

<template>
  <RouterView />

</template>

<script>


import 'vuetify/styles';

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
.app-card{
  border-radius:10px;
  height:70px;
  width: 70px;
}
.background{
  background-image: url("https://images.pexels.com/photos/1366919/pexels-photo-1366919.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1");
}
.app-card{
  margin: auto;
}
</style>
