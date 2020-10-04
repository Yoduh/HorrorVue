<template>
  <span>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon v-bind="attrs" v-on="on">
          <v-icon color="yellow lighten-1">mdi-pencil</v-icon>
        </v-btn>
      </template>
      <span>Edit Collection</span>
    </v-tooltip>
  </span>
</template>

<script>
export default {
  name: "EditBtn"
};
</script>
