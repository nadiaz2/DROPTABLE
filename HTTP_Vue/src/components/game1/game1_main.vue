<template>
  <v-container class="background">
    <v-col>
      <v-row class="ma-2, pa-2">
        <v-card @click="$router.push('pause')" class="app-card">
          <v-card-title>Browser</v-card-title>
        </v-card>

        <v-card class="app-card">
          <v-card-title>Album</v-card-title>
        </v-card>

        <v-card class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>

        <v-card  class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>
      </v-row>

      <v-row class="ma-2, pa-2">
        <v-card  class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>

        <v-card  class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>

        <v-card  class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>

        <v-card  class="app-card">
          <v-card-title>App</v-card-title>
        </v-card>
      </v-row>

    </v-col>
  </v-container>
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
  methods: {
    flipListener(e) {
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
