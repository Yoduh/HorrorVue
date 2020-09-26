const state = {
    collections: null
};

const getters = {
    collections: state => state.collections
};

const actions = {
    setCollections: ({ commit }, collections) => {
        console.log(collections);
        collections.forEach(c => {
            c.movies = c.movies.map(m => {
                return { ...m, show: false };
            })
        });
        console.log(collections);
        commit('setCollections', collections);
    }
};

const mutations = {
    setCollections: (state, collections) => {
        state.collections = collections;
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};
