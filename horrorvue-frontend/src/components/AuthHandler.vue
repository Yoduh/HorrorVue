<template>
  <div>
    <div v-if="$auth.loading">Please wait...</div>
    <div v-else-if="!error">Loading collections...</div>
    <div v-else>There was an error logging you in. Please try again later.</div>
  </div>
</template>

<script>
import db from "@/api/db";
import { mapActions } from "vuex";

export default {
  name: "AuthHandler",
  methods: {
    ...mapActions(["finalizeLogin", "setCollections", "setIsLoading"])
  },
  data() {
    return {
      isLoading: this.$auth.loading,
      error: null
    };
  },
  // async updated() {
  //     if (this.$auth.isAuthenticated) {
  //         console.log('auth', this.$auth.user);
  //         const div = this.$auth.user.sub.indexOf('|');
  //         const id = this.$auth.user.sub.slice(div + 1);
  //         console.log('id', id);
  //         const user = await db.getUser(id);
  //         this.finalizeLogin(user);
  //         console.log('push');
  //         this.$router.push('/');
  //     }
  // },
  async created() {
    if (this.$auth.isAuthenticated) {
      this.setIsLoading(true);
      const div = this.$auth.user.sub.indexOf("|");
      const id = this.$auth.user.sub.slice(div + 1);
      const user = await db.getUser(id);
      if (user.collections) this.setCollections(user.collections);
      this.finalizeLogin(user);
      this.$router.push("/");
      this.setIsLoading(false);
    }
  }
};
</script>

<style scoped></style>
