<template>
  <v-container>
    <v-row class="justify-center">
      <v-col class="text-center align-center">
        <div class="text-h1 font-weight-regular">Movie Rankings</div>
        <div class="text-h4 mb-5 font-italic font-weight-light">
          Ranking movies with friends
        </div>

        <transition name="fade">
          <v-img class="mb-5" src="@/images/movie-banner2.jpg" />
        </transition>
        <div class="text-h4 mt-5">How to use this site</div>
        <div class="text-h5 mt-3">
          Creating an account will give you access to the rankings page
        </div>
        <v-img
          src="@/images/new.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          Create a collection by using the search bar to find movies
        </div>
        <v-img
          src="@/images/search.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <span class="font-italic">Select movies to add to a collection</span>
        <span class="font-italic"
          >Movies marked as added will carry over to the results of new
          searches</span
        >
        <span class="font-italic"
          >Click the red Save button when finished adding</span
        >
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          View your newly created collection on the Rankings page!
        </div>
        <v-img
          src="@/images/collection.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          Click "Set Ranking" to rank and rate the collection
        </div>
        <v-img
          src="@/images/rank.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <span class="font-italic"
          >Rank: Drag and drop movies up and down in the order you like them the
          most</span
        >
        <span class="font-italic"
          >Rate: For each movie you can give a star rating from 0 to 5 (Half
          star increments are allowed)</span
        >
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          Go to your Profile page and send your created collections to your
          friends!
        </div>
        <v-img
          src="@/images/email.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <span class="font-italic">
          Send to multiple friends by pressing enter between e-mail addresses
        </span>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          Ask your friends to view their Profile to accept your invites!
        </div>
        <v-img
          src="@/images/profile.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          View you and your friends rankings by clicking "Toggle Rankings" for a
          collection
        </div>
        <v-img
          src="@/images/rank-table.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
        <span class="font-italic"
          >Use the Sort dropdown to rearrange by a specific user's
          rankings</span
        >
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <div class="mb-5 text-h2">.</div>
        <v-icon class="mb-5 text-h2">mdi-chevron-down</v-icon>
        <div class="text-h5">
          Create and share more collections!
        </div>
        <v-img
          src="@/images/collections.png"
          class="mt-4 mb-5"
          :max-width="imageWidth"
        />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "Home",
  computed: {
    imageWidth() {
      switch (this.$vuetify.breakpoint.name) {
        case "xs":
        case "sm":
        case "md":
          return "100%";
        case "lg":
        case "xl":
        default:
          return "80%";
      }
    }
  }
};
</script>

<style scoped>
.v-image {
  -webkit-box-shadow: 5px 5px 15px 2px #000000;
  box-shadow: 5px 5px 15px 2px #000000;
  /* max-width: 66% !important; */
}
footer {
  margin-top: 200px;
}
</style>
