<template>
  <v-app>
    <v-main>
      <navbar />
      <v-container fluid>
      <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar.vue";

export default {
  name: "App",

  components: {
    Navbar
  },

  data: () => ({
    //
  }),
};
</script>

<style>
html, body, .v-main {
  color: white;
  background-color: rgb(40, 44, 52);
}
</style>