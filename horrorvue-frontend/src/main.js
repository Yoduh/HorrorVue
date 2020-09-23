import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
// const gauthOption = {
//   clientId: '959351066294-krib2tgeut36qsakcjd4c7tvivr5bfal.apps.googleusercontent.com',
//   scope: 'profile email',
//   prompt: 'select_account'
// }
// Vue.use(GAuth, gauthOption)

// Import the Auth0 configuration
import { domain, clientId } from "../auth_config.json";
// Import the plugin here
import { Auth0Plugin } from "./auth";
// Install the authentication plugin here
Vue.use(Auth0Plugin, {
  domain,
  clientId,
  onRedirectCallback: appState => {
    router.push(
      appState && appState.targetUrl
        ? appState.targetUrl
        : window.location.pathname
    );
  }
});

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
