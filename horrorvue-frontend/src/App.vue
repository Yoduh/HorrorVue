<template>
  <v-app>
    <v-main v-if="!this.$auth.loading">
      <navbar />
      <v-container fluid>
        <router-view></router-view>
      </v-container>
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
  components: {
    Navbar
  },
  methods: {
    ...mapActions(["finalizeLogin", "setCollections"]),
    ...mapGetters(["isLoading"])
  },
  // reset store if user refreshes page
  async updated() {
    console.log(
      "APP RELOADED",
      this.$auth.isAuthenticated,
      store.getters.user,
      this.isLoading()
    );
    if (
      this.$auth.isAuthenticated &&
      store.getters.user === null &&
      !this.isLoading()
    ) {
      const div = this.$auth.user.sub.indexOf("|");
      const id = this.$auth.user.sub.slice(div + 1);
      const user = await db.getUser(id);
      this.finalizeLogin(user);
      if (user.collections) this.setCollections(user.collections);
    }
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
</style>
