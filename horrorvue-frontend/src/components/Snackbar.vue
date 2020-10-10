<template>
  <v-snackbar :value="show" :timeout="2000">
    {{ message }}

    <template v-slot:action="{ attrs }">
      <v-btn color="blue" text v-bind="attrs" @click="close">
        Close
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
import eventBus from "@/eventBus";

export default {
  name: "snackbar",
  props: ["message", "show"],
  data() {
    return {
      timeout: 2000
    };
  },
  methods: {
    close() {
      eventBus.$emit("close-snackbar");
    }
  }
};
</script>
