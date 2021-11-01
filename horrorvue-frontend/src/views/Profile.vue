<template>
  <v-container v-if="user()">
    <!-- user info -->
    <v-row>
      <v-form>
        <v-row><div class="text-h3 pl-2">My Profile</div></v-row>
        <v-row class="my-5">
          <v-col cols="12" sm="6" class="py-1">
            <v-text-field
              :value="user().firstName"
              label="First Name"
              hide-details
              dark
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" class="py-1">
            <v-text-field
              :value="user().lastName"
              label="Last Name"
              hide-details
              dark
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" class="py-1">
            <v-text-field
              :value="user().email"
              dark
              label="Email"
              hide-details
              disabled
            ></v-text-field>
          </v-col>
        </v-row>
      </v-form>
    </v-row>
    <v-row v-if="user().invites && user().invites.length > 0">
      <div class="text-h4">You've been invited to rank another collection!</div>
      <v-list dense class="mb-5" dark>
        <template v-for="(invite, index) in user().invites">
          <v-divider v-if="index != 0" :key="`divider-${index}`"></v-divider>
          <v-list-item two-line class="pr-0" :key="invite.id">
            <v-list-item-content>
              <v-list-item-title>
                {{ invite.collection.name }}
              </v-list-item-title>
              <v-list-item-subtitle>
                Invited by: {{ invite.fromUser.firstName }}
                {{ invite.fromUser.lastName }}
              </v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action class="flex-row">
              <v-btn class="ml-3" color="success" @click="subscribe(invite)"
                >Accept</v-btn
              >
              <v-btn class="mx-3" color="error" @click="reject(invite)"
                >Reject</v-btn
              >
            </v-list-item-action>
          </v-list-item>
        </template>
      </v-list>
    </v-row>
    <!-- user collections -->
    <profile-collections :collections="collections" :user="user" />
  </v-container>
</template>

<script>
import ProfileCollections from "@/components/ProfileCollections";
import db from "@/api/db";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Profile",
  components: {
    ProfileCollections
  },
  data() {
    return {
      collections: [],
      isLoading: true,
      isNotLoading: false
    };
  },
  methods: {
    ...mapGetters(["user"]),
    ...mapActions([
      "removeInvite",
      "addCollection",
      "removeInvite",
      "toggleSnackbar"
    ]),
    isRanked(index, id) {
      const ranking = this.user().collections[index].rankings.find(
        r => r.collectionId === id && r.userId === this.user().id
      );
      if (!ranking) return "No";

      this.user().collections[index].movies.forEach(m => {
        if (!ranking.order.includes(m.id)) return "Incomplete";
      });
      return "Yes";
    },
    async subscribe(invite) {
      // returns collection
      const res = await db.acceptInvite(invite);
      if (res.isSuccess) {
        this.addCollection(res.data);
        this.removeInvite(invite.id);
      } else {
        this.$eventBus.$emit("open-snackbar", res.message, "error");
      }
    },
    async reject(invite) {
      const res = await db.rejectInvite(invite.id);
      if (res.isSuccess) {
        this.removeInvite(invite.id);
      } else {
        this.$eventBus.$emit("open-snackbar", res.message, "error");
      }
    }
  },
  updated() {
    if (this.user() && this.isLoading) {
      this.collections = this.user().collections;
      this.isLoading = false;
    }
  },
  created() {
    if (this.user() && this.isLoading) {
      this.collections = this.user().collections;
      this.isLoading = false;
    }
  }
};
</script>
<style scoped>
.v-list {
  width: 100%;
  background-color: rgb(51, 57, 65);
}
.v-list-item__title {
  color: white;
}
.v-list-item__subtitle {
  color: grey !important;
}
.arrow {
  margin-top: -0.3rem;
}
</style>
