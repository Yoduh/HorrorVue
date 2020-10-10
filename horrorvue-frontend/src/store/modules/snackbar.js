const state = {
  snackbar: false
};

const getters = {
  snackbar: state => state.snackbar
};

const actions = {
  toggleSnackbar: ({ commit }) => {
    commit("setSnackbar", !state.snackbar);
  }
};

const mutations = {
  setSnackbar: (state, value) => {
    state.snackbar = value;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
