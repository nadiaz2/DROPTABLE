<template>
  <v-app>
    <v-app-bar elevation="0" aspect-ratio="1" class="bar">
      <v-btn @click="navigateToPage(route)">
        <v-icon size="40">mdi-arrow-left</v-icon>
      </v-btn>
      <v-toolbar-title class="title-size">Browser</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>
    This is supposed to be Pornhub
  </v-app>
</template>

<script setup>
if (
  DeviceMotionEvent &&
  typeof DeviceMotionEvent.requestPermission === "function"
) {
  DeviceMotionEvent.requestPermission();
}

// let webSocket = new WebSocket('wss://' + window.location.host);
//
// //receiving
// webSocket.onmessage = (event) => {
//   let pc_message = JSON.parse(event.data);
//   console.log(pc_message);
// };
//
// await until(() => webSocket.readyState === WebSocket.OPEN);
//
// const obj = { name: "PhoneFaceUp", message: 1 };
// webSocket.send(JSON.stringify(obj));
window.addEventListener("deviceorientation", handleOrientation);

function handleOrientation(event) {
  console.log(event.absolute);
  if (event.beta > 100 || event.beta < -100) {
    window.location.href = 'pause.html';
  }
}

function until(conditionFunction) {

  const poll = resolve => {
    if (conditionFunction()) resolve();
    else setTimeout(_ => poll(resolve), 100);
  }

  return new Promise(poll);

}

</script>

<script>
import router from "@/router";

export default {
  name: 'game1main',
  data() {
    return {
      route: 'game1_main',
      externalUrl: 'https://www.apple.com/' // Replace with your external URL
    };
  },
  methods: {
    flipListener(e) {
      console.log(page);
    },
    navigateToPage(routeName) {
      this.$router.push({ name: routeName });
    }
  },


}
</script>

<style scoped>
.app-card {
  border-radius: 10px;
  height: 70px;
  width: 70px;
}



.app-card {
  margin: auto;
}
</style>
