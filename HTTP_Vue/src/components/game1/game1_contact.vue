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
                <v-list-item v-else @click="dialogVisible = true">
                    <v-list-item-content>
                        <v-list-item-title v-html="item.title"></v-list-item-title>
                    </v-list-item-content>

                </v-list-item>

            </template>
        </v-list>

        <v-dialog v-model="dialogVisible"  max-width="600px" >
            <v-card elevation="7" style="height: 500px;" >
                <v-card-title class="justify-center">
                    <v-row justify="center" class="avatar">
                        <v-avatar size="160px">
                            <v-img :src="contact.avatar"></v-img>
                        </v-avatar>
                    </v-row>
                </v-card-title>

                <v-card-subtitle class="text-center">
                    <br>
                    <h1 style="font-size: 40px; color: black">{{ contact.name }}</h1>
                    <br>
                    <p style="font-size: 20px; color: black">{{ contact.phone }}</p>
                </v-card-subtitle>

                <v-card-text>
                    <v-list-item>
                        <v-list-item-content>
                            <v-list-item-title>Address</v-list-item-title>
                            <v-list-item-subtitle>{{ contact.address }}</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                    <v-list-item>
                        <v-list-item-content>
                            <v-list-item-title>Email</v-list-item-title>
                            <v-list-item-subtitle>{{ contact.email }}</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                    <v-list-item>
                        <v-list-item-content>
                            <v-list-item-title>Birthday</v-list-item-title>
                            <v-list-item-subtitle>{{ formatDate(contact.birthday) }}</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                </v-card-text>
            </v-card>
        </v-dialog>





    </v-app>
</template>
  

  
<script>
import router from "@/router";

export default {
    name: 'game1main',
    data() {
        return {
            dialogVisible: false,
            route: 'game1_main',
            contact: {
                name: 'John Doe',
                phone: '123-456-7890',
                email: 'johndoe@example.com',
                address: '1234 Vue Street, Vuetown, VU',
                avatar: 'https://cdn.vuetifyjs.com/images/lists/2.jpg',// Placeholder image URL
                birthday: '1990-01-01'
            },
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
        formatDate(dateString) {
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            return new Date(dateString).toLocaleDateString(undefined, options);
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
.v-card-title {
    margin-top: 40px;
}
</style>

