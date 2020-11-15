<template>
  <div class="navbar-wrapper">
    <v-app-bar color="deep-grey accent-4" dense dark app>
      <v-row class="align-center">
        <v-btn max-width="40" @click="home">
          <v-img
            src="@/components/icons/film.png"
            max-height="45"
            class="mr-2"
            contain
          ></v-img>
        </v-btn>
      </v-row>

      <v-spacer></v-spacer>

      <v-app-bar-nav-icon
        @click.stop="drawer = !drawer"
        class="d-inline-flex d-sm-none"
      ></v-app-bar-nav-icon>

      <div v-if="!$auth.loading">
        <v-tooltip
          v-for="(item, index) in authenticatedItems"
          :key="index"
          bottom
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              icon
              @click="item.click"
              v-bind="attrs"
              v-on="on"
              class="d-none d-sm-inline-flex"
            >
              <v-icon>{{ item.icon }} </v-icon>
            </v-btn>
          </template>
          <span>{{ item.title }}</span>
        </v-tooltip>
      </div>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" absolute right temporary dark>
      <v-list nav dense>
        <v-list-item-group v-model="group">
          <v-list-item
            v-for="(item, index) in authenticatedItems"
            :key="index"
            @click="item.click"
          >
            <v-list-item-title class="text-h6">
              <v-icon>{{ item.icon }}</v-icon>
              {{ item.title }}
            </v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "Navbar",
  data() {
    return {
      drawer: false,
      group: null,
      items: [
        {
          title: "Rankings",
          icon: "mdi-format-list-bulleted",
          show: true,
          click: this.dashboard
        },
        {
          title: "Profile",
          icon: "mdi-account",
          show: this.$auth.isAuthenticated,
          click: this.profile
        },
        {
          title: "Login",
          icon: "mdi-login",
          show: !this.$auth.isAuthenticated,
          click: this.navLogin
        },
        {
          title: "Logout",
          icon: "mdi-logout",
          show: this.$auth.isAuthenticated,
          click: this.navLogout
        }
      ]
    };
  },
  computed: {
    ...mapGetters(["isAuthenticated"]),
    authenticatedItems() {
      return this.items.filter(i => i.show);
    }
  },
  methods: {
    ...mapActions(["login", "logout"]),
    home() {
      if (this.$route.path !== "/") {
        this.$router.push("/");
      }
    },
    dashboard() {
      if (this.$route.path !== "/dashboard") {
        this.$router.push("/dashboard");
      }
    },
    navLogin() {
      this.$auth.loginWithRedirect();
    },
    navLogout() {
      this.$auth.logout({
        returnTo: window.location.origin
      });
    },
    profile() {
      if (this.$route.path !== "/profile") {
        this.$router.push(`/profile`);
      }
    }
  },
  watch: {
    group() {
      this.drawer = false;
    }
  }
};
</script>

<style scoped>
a {
  text-decoration: none;
}
</style>
