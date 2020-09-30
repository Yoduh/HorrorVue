import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import AddMovies from "@/views/AddMovies";
import AuthHandler from "@/components/AuthHandler";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
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
  }
];

const router = new VueRouter({
  mode: "history",
  // eslint-disable-next-line no-undef
  base: process.env.BASE_URL,
  routes
});

export default router;
