const state = {
  rankings: null
};

const getters = {
  rankings: state => state.rankings
};

const actions = {
  setRankings: ({ commit }, rankings) => {
    commit("setRankings", rankings);
  },
  updateRanking: ({ commit }, ranking) => {
    commit("updateRankings", ranking);
  }
};

const mutations = {
  setRankings: (state, rankings) => {
    state.rankings = [...rankings];
  },
  updateRanking: (state, ranking) => {
    state.rankings = ranking;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
