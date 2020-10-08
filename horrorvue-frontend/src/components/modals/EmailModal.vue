<template>
  <v-dialog v-model="dialog" max-width="600px" @click:outside="$emit('close')">
    <v-card>
      <v-card-title>
        <span class="headline">Enter e-mail addresses</span>
      </v-card-title>
      <v-card-text class="pb-0">
        <v-container>
          <v-list>
            <v-list-item v-for="(email, i) in emails" :key="i">
              {{ email }}
            </v-list-item>
          </v-list>
          <v-text-field
            label="E-mail"
            v-model="value"
            :rules="rules"
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
        <v-btn color="blue darken-1" text @click="$emit('close')">
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
    email() {
      console.log("email");
    },
    addToList(email) {
      if (email === "" || email === null) return;
      this.emails.push(email);
      this.value = null;
    }
  }
};
</script>

<style scoped></style>
