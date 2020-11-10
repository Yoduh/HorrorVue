<template>
  <v-app>
    <v-main v-if="!this.$auth.loading">
      <overlay />
      <navbar />
      <v-container fluid>
        <router-view></router-view>
      </v-container>

      <v-footer class="font-weight-medium">
        <v-col class="text-center flex-row justify-center" cols="12">
          {{ new Date().getFullYear() }} — <strong>Alex Handlovits</strong>
        </v-col>
      </v-footer>
      <snackbar
        :show="snackbar()"
        :message="snackbarText"
        :color="snackbarColor"
      />
    </v-main>
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar";
import Overlay from "@/components/Overlay";
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
    Navbar,
    Overlay
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

/* height (horizontal) */
::-webkit-scrollbar {
  height: 10px;
  width: 10px;
  cursor: pointer;
}
/* Track */
::-webkit-scrollbar-track {
  background: transparent;
  border-radius: 10px;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 10px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>
