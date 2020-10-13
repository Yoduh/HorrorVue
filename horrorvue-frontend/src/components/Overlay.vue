<template>
  <v-overlay :z-index="zIndex" :value="isLoading() || this.$auth.loading">
    <v-progress-circular
      :size="64"
      color="primary"
      indeterminate
    ></v-progress-circular>
  </v-overlay>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Overlay",
  data() {
    return {
      overlay: false,
      zIndex: 9999
    };
  },
  methods: {
    ...mapGetters(["isLoading"])
  }
};
</script>

<style scoped>
.v-progress-circular {
  margin: 1rem;
}
</style>
