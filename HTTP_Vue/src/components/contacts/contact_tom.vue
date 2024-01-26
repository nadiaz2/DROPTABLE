<template>
    <v-toolbar color="cyan-lighten-1">
        <v-btn variant="text" icon="mdi-arrow-left" @click="navigateToPage(route)"></v-btn>

        <v-toolbar-title>Inbox</v-toolbar-title>

        <v-spacer></v-spacer>

        <v-btn variant="text" icon="mdi-magnify"></v-btn>
    </v-toolbar>
    <v-container>
        <v-row justify="center">
            <v-col cols="12" sm="8" md="6">
                <v-card elevation="7">
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
            </v-col>
        </v-row>
    </v-container>
</template>
  
<script>
export default {
    name: 'ContactDetail',
    data() {
        return {
            route: 'game1_contact',
            contact: {
                name: 'John Doe',
                phone: '123-456-7890',
                email: 'johndoe@example.com',
                address: '1234 Vue Street, Vuetown, VU',
                avatar: 'https://cdn.vuetifyjs.com/images/lists/2.jpg',// Placeholder image URL
                birthday: '1990-01-01'
            }
        };
    },
    methods: {
        navigateToPage(routeName) {
            this.$router.push({ name: routeName });
        },
        formatDate(dateString) {
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            return new Date(dateString).toLocaleDateString(undefined, options);
        }
    }

}
</script>
  
<style>
.avatar {
    margin-top: 20px;
    margin-bottom: 20px;
}
</style>
  