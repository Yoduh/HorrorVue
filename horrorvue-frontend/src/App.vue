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
import db from '@/api/db'
import store from "@/store";
import { mapActions } from 'vuex';

export default {
  name: "App",
  components: {
    Navbar
  },
  methods: {
      ...mapActions(['finalizeLogin', 'setCollections'])
  },
  data() {
    return {
      isLoading: this.$auth.loading
    }
  },
  // reset store if user refreshes page
  async updated() {
    if (this.$auth.isAuthenticated) {
        this.finalizeLogin(this.$auth.user);
        const user = await db.getUser(store.getters.user.id);
        if (user.collections)
          this.setCollections(user.collections);
    }
  }
};
</script>

<style>
html, body, .v-main {
  color: white;
  background-color: rgb(40, 44, 52);
}
/* v-col {
  display: flex;
  flex-direction: column;
} */
</style>