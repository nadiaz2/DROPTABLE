<template>
    <v-app>
        <v-toolbar color="purple-lighten-1">
            <v-btn variant="text" icon="mdi-arrow-left" @click="navigateToPage(route)"></v-btn>

            <v-toolbar-title>Inbox</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn variant="text" icon="mdi-magnify"></v-btn>
        </v-toolbar>

        <v-list :items="items" item-props lines="three">
            <template v-slot:subtitle="{ subtitle }">
                <div v-html="subtitle"></div>
            </template>
        </v-list>

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
            externalUrl: 'https://www.apple.com/',// Replace with your external URL,
            items: [
                { type: 'subheader', title: 'Today' },
                {
                    prependAvatar: 'https://cdn.vuetifyjs.com/images/lists/1.jpg',
                    title: 'Brunch this weekend?',
                    subtitle: `<span class="text-primary">Ali Connors</span> &mdash; I'll be in your neighborhood doing errands this weekend. Do you want to hang out?`,
                },
                { type: 'divider', inset: true },
                {
                    prependAvatar: 'https://cdn.vuetifyjs.com/images/lists/2.jpg',
                    title: 'Summer BBQ',
                    subtitle: `<span class="text-primary">to Alex, Scott, Jennifer</span> &mdash; Wish I could come, but I'm out of town this weekend.`,
                },
                { type: 'divider', inset: true },
                {
                    prependAvatar: 'https://cdn.vuetifyjs.com/images/lists/3.jpg',
                    title: 'Oui oui',
                    subtitle: '<span class="text-primary">Sandra Adams</span> &mdash; Do you have Paris recommendations? Have you ever been?',
                },
                { type: 'divider', inset: true },
                {
                    prependAvatar: 'https://cdn.vuetifyjs.com/images/lists/4.jpg',
                    title: 'Birthday gift',
                    subtitle: '<span class="text-primary">Trevor Hansen</span> &mdash; Have any ideas about what we should get Heidi for her birthday?',
                },
                { type: 'divider', inset: true },
                {
                    prependAvatar: 'https://cdn.vuetifyjs.com/images/lists/5.jpg',
                    title: 'Recipe to try',
                    subtitle: '<span class="text-primary">Britta Holt</span> &mdash; We should eat this: Grate, Squash, Corn, and tomatillo Tacos.',
                },
            ],
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
  