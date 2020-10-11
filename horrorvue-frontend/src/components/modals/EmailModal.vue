<template>
  <v-dialog
    v-model="dialog"
    max-width="600px"
    @click:outside="close"
    @keydown.esc="close"
    class="pa-5"
  >
    <v-form
      ref="form"
      v-model="valid"
      lazy-validation
      class="white pa-5"
      @submit.prevent="addToList"
    >
      <v-container>
        <div class="text-h5">
          Enter the e-mail address of each user you want to send an invite to
        </div>
        <v-list dense class="py-2">
          <v-list-item
            v-for="(email, i) in emails"
            :key="i"
            class="item mb-1 text-subtitle2"
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
          v-model="email"
          :rules="rules"
          hint="Press enter to add another address"
          clearable
        ></v-text-field>
        <div class="mt-3 text-decoration-underline">
          Sending invite for collections:
        </div>
        <div
          v-for="collection in collections"
          :key="collection.id"
          class="mt-1"
        >
          <i>{{ collection.name }}</i>
        </div>
      </v-container>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="close">
        Cancel
      </v-btn>
      <v-btn color="green" text @click="sendInvite">
        Send
      </v-btn>
    </v-form>
  </v-dialog>
</template>

<script>
import db from "@/api/db";
import { mapGetters } from "vuex";

export default {
  name: "EmailModal",
  props: ["dialog", "collections"],
  data() {
    return {
      valid: true,
      rules: [
        value => {
          const pattern = /^$|^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "Invalid e-mail.";
        },
        value => {
          var regex = new RegExp(this.user().email, "g");
          return !value.match(regex) || "Can't invite yourself!";
        }
      ],
      email: "",
      emails: []
    };
  },
  methods: {
    ...mapGetters(["user"]),
    async sendInvite() {
      if (this.email !== "") {
        if (!this.addToList()) return;
      }
      if (this.emails.length === 0) {
        this.$eventBus.$emit(
          "open-snackbar",
          "You need to add at least 1 e-mail",
          "error"
        );
        return;
      }
      const res = await db.sendInvite(
        this.user().id,
        this.emails,
        this.collections.map(c => c.id)
      );
      if (res.isSuccess) {
        this.$eventBus.$emit("open-snackbar", "Invites sent!", "success");
      } else {
        if (res.message === "Sequence contains no elements")
          this.$eventBus.$emit("open-snackbar", "Could not find user", "error");
        else this.$eventBus.$emit("open-snackbar", res.message, "error");
      }
      this.close();
    },
    addToList() {
      if (!this.valid || this.email === "" || this.email === null) return false;
      this.emails.push(this.email);
      this.email = "";
      return true;
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
  padding-left: 0;
}
</style>
