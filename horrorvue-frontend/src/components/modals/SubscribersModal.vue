<template>
  <div>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          @click="dialog = !dialog"
          v-bind="attrs"
          v-on="on"
          icon
          x-small
          class="ml-1 arrow"
        >
          <v-icon>mdi-arrow-top-right</v-icon>
        </v-btn>
      </template>
      <span>Show Subscribers</span>
    </v-tooltip>
    <v-dialog v-model="dialog" max-width="600px" dark>
      <v-card>
        <v-card-title>
          <span class="headline">Subscribers</span>
        </v-card-title>
        <v-card-text class="pb-0">
          <ol>
            <li v-for="user in otherUsers" :key="user.id">
              {{ user.firstName }} {{ user.lastName }}
            </li>
          </ol>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false"
            >Close</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "SubscribersModal",
  props: ["appUsers", "user"],
  data() {
    return {
      dialog: false
    };
  },
  computed: {
    otherUsers() {
      return this.appUsers.filter(u => u.id !== this.user.id);
    }
  }
};
</script>

<style scoped>
.arrow {
  color: #afadcb;
}
</style>
