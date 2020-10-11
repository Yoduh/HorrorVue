<template>
  <v-snackbar
    :value="show"
    :timeout="2000"
    dark
    outlined
    :color="`${color}`"
    @input="close"
  >
    {{ message }}

    <template v-slot:action="{ attrs }">
      <v-btn :color="color" text v-bind="attrs" @click="close">
        Close
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
export default {
  name: "snackbar",
  props: ["message", "show", "color"],
  data() {
    return {
      timeout: 2000
    };
  },
  methods: {
    close() {
      this.$eventBus.$emit("close-snackbar");
    }
  }
};
</script>
