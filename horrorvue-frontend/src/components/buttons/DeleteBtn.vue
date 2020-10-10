<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <span v-on="on" v-bind="attrs">
          <v-btn v-show="canDelete" icon @click="dialog = !dialog">
            <v-icon color="red lighten-1">mdi-trash-can-outline</v-icon>
          </v-btn>
          <v-btn v-show="!canDelete" icon @click="unsubDialog = !unsubDialog">
            <v-icon color="red lighten-1">mdi-account-multiple-remove</v-icon>
          </v-btn>
        </span>
      </template>
      <span v-if="canDelete">Delete Collection</span>
      <span v-else>Unsubscribe</span>
    </v-tooltip>
    <v-dialog v-model="dialog" max-width="600px" dark>
      <v-card>
        <v-card-title>
          <span class="headline">Warning</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            Are you sure you want to permanently delete?
            <div v-if="collection.appUsers.length > 1" class="mt-5">
              <strong class="red--text text-h6"
                >This collection is shared with other people!</strong
              >
              <p>
                The following users will lose access to this collection if
                deleted:
              </p>
              <li v-for="user in otherUsers" :key="user.id">
                {{ user.firstName }} {{ user.lastName }}
              </li>
            </div>
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

    <v-dialog v-model="unsubDialog" max-width="600px" dark>
      <v-card>
        <v-card-title>
          <span class="headline">Warning</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            Are you sure you want to unsubscribe?
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="unsubDialog = false">
            Cancel
          </v-btn>
          <v-btn color="blue darken-1" text @click="sendUnsubscribe">
            Confirm
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </span>
</template>

<script>
export default {
  name: "DeleteBtn",
  props: ["canDelete", "collection", "userId"],
  data() {
    return {
      dialog: false,
      unsubDialog: false
    };
  },
  methods: {
    sendDelete() {
      this.$emit("delete");
      this.dialog = false;
    },
    sendUnsubscribe() {
      this.$emit("unsubscribe");
      this.unsubDialog = false;
    }
  },
  computed: {
    otherUsers() {
      return this.collection.appUsers.filter(u => u.id !== this.userId);
    }
  }
};
</script>

<style scoped></style>
