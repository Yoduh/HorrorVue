
<template>
    <v-app-bar
      color="deep-grey accent-4"
      dense
      dark
    >
    <v-app-bar-nav-icon></v-app-bar-nav-icon>

    <v-toolbar-title>Franchise Rankings</v-toolbar-title>

    <v-spacer></v-spacer>

    <router-link to="/">
        <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
            <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-view-dashboard</v-icon>
            </v-btn>
        </template>
        <span>Home</span>
        </v-tooltip>
    </router-link>

    <div v-if="!$auth.loading">
        <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
            <v-btn v-if="!$auth.isAuthenticated" icon @click="navLogin" v-bind="attrs" v-on="on">
                <v-icon>mdi-login</v-icon>
            </v-btn>
        </template>
        <span>Login</span>
        </v-tooltip>

        <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
            <v-btn v-if="$auth.isAuthenticated" icon @click="navLogout" v-bind="attrs" v-on="on">
                <v-icon>mdi-logout</v-icon>
            </v-btn>
        </template>
        <span>Logout</span>
        </v-tooltip>
    </div>

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
        ...mapGetters(['isAuthenticated'])
    },
    methods: {
        ...mapActions(['login', 'logout']),
        navLogin() {
            this.$auth.loginWithRedirect();
        },
        navLogout() {
            this.$auth.logout({
                returnTo: window.location.origin
            });
        }
    }
}
</script>

<style scoped>
a {
    text-decoration: none;
}
</style>