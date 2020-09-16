
<template>
    <v-app-bar
      color="deep-purple accent-4"
      dense
      dark
    >
    <v-app-bar-nav-icon></v-app-bar-nav-icon>

    <v-toolbar-title>Franchise Rankings</v-toolbar-title>

    <v-spacer></v-spacer>

    <router-link to="/">
        <v-btn icon>
            <v-icon>mdi-view-dashboard</v-icon>
        </v-btn>
    </router-link>
    <v-btn v-if="!isLoggedIn" icon @click="navLogin">
        <v-icon>mdi-login</v-icon>
    </v-btn>
    <v-btn v-else icon @click="logout">
        <v-icon>mdi-logout</v-icon>
    </v-btn>

    <v-menu
    left
    bottom
    >
    <template v-slot:activator="{ on, attrs }">
        <v-btn
        icon
        v-bind="attrs"
        v-on="on"
        >
        <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
    </template>

    <v-list>
        <v-list-item
        v-for="n in 5"
        :key="n"
        @click="() => {}"
        >
        <v-list-item-title>Option {{ n }}</v-list-item-title>
        </v-list-item>
    </v-list>
    </v-menu>
</v-app-bar>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';

export default {
    name: "Navbar",
    data () {
      return {
        drawer: true,
        items: [
          { title: 'Dashboard', icon: 'mdi-view-dashboard' },
          { title: 'Login', icon: 'mdi-login' },
          { title: 'Logout', icon: 'mdi-logout' },
        ],
      }
    },
    computed: {
        ...mapGetters(['isLoggedIn'])
    },
    methods: {
        ...mapActions(['login', 'logout']),
        async navLogin() {
            const guser = await this.$gAuth.signIn();
            console.log('guser', guser);
            // save to state
            if (guser)
                this.login(guser);
        }
    }
}
</script>

<style scoped>
a {
    text-decoration: none;
}
</style>