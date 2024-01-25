<template>
    <v-app>
        <v-toolbar color="cyan-lighten-1">
            <v-btn variant="text" icon="mdi-arrow-left" @click="navigateToPage(route)"></v-btn>

            <v-toolbar-title>Inbox</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn variant="text" icon="mdi-magnify"></v-btn>
        </v-toolbar>

        <v-list :items="items">
            <template v-for="(item, index) in items" :key="index">
                <!-- Check if the item is a divider -->
                <v-divider v-if="item.type === 'divider'" :inset="item.inset"></v-divider>

                <!-- Render regular list item for non-divider items -->
                <v-list-item v-else @click="navigateToPage(item.route)">
                    <v-list-item-content>
                        <v-list-item-title v-html="item.title"></v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </template>
        </v-list>





    </v-app>
</template>
  

  
<script>
import router from "@/router";

export default {
    name: 'game1main',
    data() {
        return {
            route: 'game1_main',
            items: [

                {
                    title: 'Alice',
                    route: 'contact_tom',
                },
                { type: 'divider', inset: true },
                {
                    title: 'Bob',
                    route: 'contact_tom',
                },
                { type: 'divider', inset: true },
                {
                    title: 'Chloe',
                    route: 'contact_tom',
                },
                { type: 'divider', inset: true },
                {
                    title: 'Derek',
                    route: 'contact_tom',
                },
                { type: 'divider', inset: true },
                {
                    title: 'Erin',
                    route: 'contact_tom',
                },

            ],
        };
    },
    methods: {
        flipListener(e) {
            console.log(page);
        },

        navigateToPage(routeName) {
            console.log("Navigating to:", routeName); // For debugging
            if (routeName) {
                this.$router.push({ name: routeName });
            }
        },

        testClick(items) {
            console.log("Item clicked:", items);
            // If this works, replace with navigateToPage(item.route)
        },
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

.custom-contacts {
    font-size: 100px !important;
}
</style>

