<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <span v-on="on" v-bind="attrs">
          <v-btn :disabled="disabled" icon @click="edit">
            <v-icon color="yellow lighten-1">mdi-pencil-outline</v-icon>
          </v-btn>
        </span>
      </template>
      <span v-if="disabled">Can't edit other user's collection</span>
      <span v-else>Edit Collection</span>
    </v-tooltip>
  </span>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "EditBtn",
  props: ["disabled"],
  methods: {
    ...mapGetters(["collections", "selectedCollection", "user"]),
    edit() {
      window.localStorage.removeItem("selectedCollection");
      this.$router.push(`/search?q=${this.selectedCollection().name}`);
    }
  }
};
</script>
