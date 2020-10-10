<template>
  <v-dialog
    v-model="dialog"
    max-width="600px"
    @click:outside="close"
    @keydown.esc="close"
  >
    <v-card>
      <v-card-text class="pb-0">
        <v-container>
          <h3>
            Enter the e-mail address of each user you want to send an invite to
          </h3>
          <v-list dense class="py-0">
            <v-list-item
              v-for="(email, i) in emails"
              :key="i"
              class="item mb-1"
            >
              {{ email }}
              <v-btn
                icon
                color="red"
                x-small
                class="ml-1 remove"
                @click="remove(i)"
              >
                <v-icon>mdi-close</v-icon>
              </v-btn>
            </v-list-item>
          </v-list>
          <v-text-field
            label="E-mail"
            v-model="value"
            :rules="rules"
            hint="Press enter to add another address"
            clearable
            @change="addToList"
          ></v-text-field>
          <div class="mt-3">Sending invite for collections:</div>
          <div
            v-for="collection in collections"
            :key="collection.id"
            class="mt-1"
          >
            <i>{{ collection.name }}</i>
          </div>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close">
          Cancel
        </v-btn>
        <v-btn color="green" text @click="email">
          Send
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import db from "@/api/db";
import { mapGetters } from "vuex";

export default {
  name: "EmailModal",
  props: ["dialog", "collections"],
  data: () => ({
    rules: [
      value => {
        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return value ? pattern.test(value) || "Invalid e-mail." : true;
      }
    ],
    value: "",
    emails: []
  }),
  methods: {
    ...mapGetters(["user"]),
    async email() {
      if (this.emails.length === 0) {
        console.log("no emails given");
        return;
      }
      const res = await db.sendInvite(
        this.user().id,
        this.emails,
        this.collections.map(c => c.id)
      );
      if (res.isSuccess) {
        console.log("make a success snackbar here");
      }
      this.close();
    },
    addToList(email) {
      if (email === "" || email === null) return;
      this.emails.push(email);
      this.value = null;
    },
    close() {
      this.value = null;
      this.emails = [];
      this.$emit("close");
    },
    remove(index) {
      this.emails.splice(index, 1);
    }
  }
};
</script>

<style scoped>
.remove {
  height: auto;
  margin: 0;
}
.item {
  height: auto;
  min-height: auto;
}
</style>
