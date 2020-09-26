<template>
<div>
    <div v-if="$auth.loading">
        Please wait...
    </div>
    <div v-else-if="!error">
        Loading collections...
    </div>
    <div v-else>
        There was an error logging you in. Please try again later.
    </div>
</div>
</template>

<script>
import db from '@/api/db'
import store from '@/store';
import { mapActions } from 'vuex';

export default {
    name: 'AuthHandler',
    methods: {
        ...mapActions(['finalizeLogin', 'setCollections'])
    },
    data() {
        return {
            isLoading: this.$auth.loading,
            error: null
        }
    },
    async updated() {
        if (this.$auth.isAuthenticated) {
            this.finalizeLogin(this.$auth.user);
            await db.getUser(store.getters.user.id);
            console.log('push');
            this.$router.push('/');
        }
    },
    async created() {
        if (this.$auth.isAuthenticated) {
            this.finalizeLogin(this.$auth.user);
            await db.getUser(store.getters.user.id);
            this.$router.push('/');
        }
    }
}
</script>

<style scoped>
</style>