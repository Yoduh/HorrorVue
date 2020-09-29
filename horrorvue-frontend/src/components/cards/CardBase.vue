<template>
  <v-card class="mb-5">
    <v-img
      :src="getImagePath(result.poster_path)"
      contain
      class="white--text align-end"
      max-width="150"
    ></v-img>
    <v-card-title
      v-text="result.title"
      class="text-subtitle-2 px-2 py-2"
    ></v-card-title>
    <v-card-subtitle class="px-2 py-0">
      {{ result.release_date.substring(0, 4) }} <slot name="info"></slot>
    </v-card-subtitle>

    <slot></slot>
  </v-card>
</template>

<script>
export default {
  name: "CardBase",
  props: ["result"],
  methods: {
    getImagePath(apiPath) {
      if (apiPath && apiPath.length > 0)
        return `https://image.tmdb.org/t/p/w500${apiPath}`;
      else {
        // eslint-disable-next-line no-undef
        return require("@/assets/no-image-found.png");
      }
    }
  }
};
</script>

<style scoped>
.v-card {
  max-width: fit-content !important;
  background-color: transparent !important;
}
.v-card__title {
  color: black;
  background-color: white;
  word-break: normal !important;
}
.v-card__subtitle {
  color: black !important;
  background-color: white;
  display: flex;
  align-items: center;
}
.v-card__text {
  color: black !important;
  background-color: white;
}
</style>
