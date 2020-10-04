<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon v-bind="attrs" v-on="on">
          <v-icon color="red lighten-1">mdi-delete</v-icon>
        </v-btn>
      </template>
      <span>Delete Collection</span>
    </v-tooltip>
  </span>
</template>

<script>
export default {
  name: "DeleteBtn"
};
</script>

<style scoped></style>
