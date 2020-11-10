import Vue from "vue";
import VueRouter from "vue-router";
import Home from "@/views/Home";
import Dashboard from "@/views/Dashboard.vue";
import AddMovies from "@/views/AddMovies";
import AuthHandler from "@/components/AuthHandler";
import Profile from "@/views/Profile";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/dashboard",
    name: "Dashboard",
    component: Dashboard
  },
  {
    path: "/search",
    name: "AddMovies",
    component: AddMovies,
    props: route => ({ query: route.query.q })
  },
  {
    path: "/auth/callback",
    name: "AuthHandler",
    component: AuthHandler
  },
  {
    path: "/profile",
    name: "Profile",
    component: Profile
  }
];

const router = new VueRouter({
  mode: "history",
  // eslint-disable-next-line no-undef
  base: process.env.BASE_URL,
  routes
});

export default router;
