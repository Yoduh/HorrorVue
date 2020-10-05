<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon @click="dialog = !dialog" v-bind="attrs" v-on="on">
          <v-icon color="red lighten-1">mdi-delete</v-icon>
        </v-btn>
      </template>
      <span>Delete Collection</span>
    </v-tooltip>
    <v-dialog v-model="dialog" max-width="600px" dark>
      <v-card>
        <v-card-title>
          <span class="headline">Warning</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            Are you sure you want to delete this collection?
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false"
            >Cancel</v-btn
          >
          <v-btn color="blue darken-1" text @click="sendDelete">Confirm</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </span>
</template>

<script>
export default {
  name: "DeleteBtn",
  data() {
    return {
      dialog: false
    };
  },
  methods: {
    sendDelete() {
      this.$emit("delete");
      this.dialog = false;
    }
  }
};
</script>

<style scoped></style>
