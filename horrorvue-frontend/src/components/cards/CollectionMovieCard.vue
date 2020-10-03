<template>
  <card-base :result="result">
    <template v-slot:info>
      <v-spacer></v-spacer>
      <info-modal
        :title="result.title"
        :subtitle="result.release_date.substring(0, 4)"
        :description="result.overview"
      />
    </template>
  </card-base>
</template>

<script>
import CardBase from "@/components/cards/CardBase";
import InfoModal from "@/components/modals/InfoModal";

export default {
  name: "CollectionMovieCard",
  components: {
    CardBase,
    InfoModal
  },
  props: ["result"],
  methods: {}
};
</script>

<style scoped>
.v-card__actions {
  background-color: white;
  padding: 0;
}
</style>
