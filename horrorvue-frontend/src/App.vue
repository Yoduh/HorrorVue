<template>
  <v-app>
    <v-main v-if="!this.$auth.loading">
      <navbar />
      <v-container fluid>
        <router-view></router-view>
      </v-container>
      <snackbar
        :show="snackbar()"
        :message="snackbarText"
        :color="snackbarColor"
      />
    </v-main>
    <div v-else>
      Loading...
    </div>
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import db from "@/api/db";
import store from "@/store";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "App",
  data() {
    return {
      snackbarText: "",
      snackbarColor: ""
    };
  },
  components: {
    Navbar
  },
  methods: {
    ...mapActions(["finalizeLogin", "setCollections", "toggleSnackbar"]),
    ...mapGetters(["isLoading", "snackbar"])
  },
  // reset store if user refreshes page
  async updated() {
    if (
      this.$auth.isAuthenticated &&
      store.getters.user === null &&
      !this.isLoading()
    ) {
      const div = this.$auth.user.sub.indexOf("|");
      const id = this.$auth.user.sub.slice(div + 1);
      const user = await db.getUser(id);
      await this.finalizeLogin(user);
      if (user.collections) await this.setCollections(user.collections);
    }
  },
  created() {
    this.$eventBus.$on("close-snackbar", () => {
      this.toggleSnackbar();
    });
    this.$eventBus.$on("open-snackbar", (text, color) => {
      this.snackbarText = text;
      this.snackbarColor = color;
      this.toggleSnackbar();
    });
  }
};
</script>

<style>
html,
body,
.v-main {
  color: white;
  background-color: rgb(40, 44, 52);
}
.col {
  display: flex;
  flex-direction: column;
}
.v-expansion-panel-header__icon {
  margin-left: 0 !important;
}
</style>
