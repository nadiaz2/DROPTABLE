<template>
  <v-container class="background">
    <v-row>
      <v-col
        v-for="n in 24"
        :key="n"
        cols="3"
      >
        <v-card @click="goToPage('page' + n)" class="app-card">
          <v-card-title>App {{ n }}</v-card-title>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>


import 'vuetify/styles';



export default {
  name: 'YourComponent',
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
</style>
