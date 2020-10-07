<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon v-bind="attrs" v-on="on" @click="edit">
          <v-icon color="yellow lighten-1">mdi-pencil</v-icon>
        </v-btn>
      </template>
      <span>Edit Collection</span>
    </v-tooltip>
  </span>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "EditBtn",
  methods: {
    ...mapGetters(["collections", "selectedCollection", "user"]),
    edit() {
      window.localStorage.removeItem("selectedCollection");
      this.$router.push(`/search?q=${this.selectedCollection().name}`);
    }
  }
};
</script>
