<template>
  <v-app>
    <v-container >
      <v-app-bar elevation="0" aspect-ratio="1" class="bar">
        <v-btn @click="navigateToPage(route)">
          <v-icon size="40">mdi-arrow-left</v-icon>
        </v-btn>
        <v-toolbar-title class="title-size">Album</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-app-bar>
    </v-container>

    <v-container class="photo">
      <v-row>
        <v-col cols="4" v-for="(image, index) in images" :key="index" class="no-padding">
          <v-img :src="image" aspect-ratio="1" cover></v-img>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      route: 'game1_main',
      images: [
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg', 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',
        'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg',

        // Add more image URLs here
      ]
    };
  },
  methods: {
    flipListener(e) {
      console.log(page);
    },
    navigateToPage(routeName) {
      this.$router.push({ name: routeName });
    }
  }
}
</script>

<style scoped>
.photo {
  margin-top: 10%;

}
.no-padding {
  padding: 1% !important;
}
.bar {
  padding-top: 2%;
}
.title-size {
  font-size: 150%;
  /* font-weight: bold; */
  /* color: white; */
}
</style>