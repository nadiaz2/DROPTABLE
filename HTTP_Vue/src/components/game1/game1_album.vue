
<template>
  <v-app>

    <v-container>
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
      <v-col cols="4" v-for="(item, index) in items" :key="index" class="no-padding">
        <!-- Use a div as a clickable wrapper for the image -->
        <div @click="sendMsg(item.Message)" class="image-container cursor-pointer">
          <v-img :src="item.image" aspect-ratio="1" cover></v-img>
        </div>
      </v-col>
    </v-row>
  </v-container>
  </v-app>
</template>

<script>

import connection from "@/plugins/connection";
export default {
  name: 'game1_album',
  data() {
    return {

      route: 'game1_main',
      items: [

        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '01-FOUNDPHOTO', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/1/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },
        { Message: '', image: 'https://cdn.photoswipe.com/photoswipe-demo-images/photos/2/img-2500.jpg' },

        // Add more image URLs here
      ],


    };
  },
  methods: {
    flipListener(e) {
      console.log(page);
    },
    navigateToPage(routeName) {
      this.$router.push({ name: routeName });
    },
    sendMsg(message) {
      connection.send(message);
      console.log(message);
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