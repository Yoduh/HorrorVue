
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

    <v-tooltip bottom>
    <template v-slot:activator="{ on, attrs }">
        <v-btn v-if="!isLoggedIn" icon @click="navLogin" v-bind="attrs" v-on="on">
            <v-icon>mdi-login</v-icon>
        </v-btn>
    </template>
    <span>Login</span>
    </v-tooltip>

    <v-tooltip bottom>
    <template v-slot:activator="{ on, attrs }">
        <v-btn v-if="isLoggedIn" icon @click="logout" v-bind="attrs" v-on="on">
            <v-icon>mdi-logout</v-icon>
        </v-btn>
    </template>
    <span>Logout</span>
    </v-tooltip>

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