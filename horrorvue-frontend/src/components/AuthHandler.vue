<template>
    <div>
        Please wait...
    </div>
</template>

<script>
import { mapActions } from 'vuex';
export default {
    name: 'AuthHandler',
    methods: {
        ...mapActions(['finalizeLogin'])
    },
    created() {
        console.log('window.location.hash', window.location.hash);
        this.finalizeLogin(window.location.hash);
        this.$router.push('/');
    }
}
</script>

<style scoped>
</style>