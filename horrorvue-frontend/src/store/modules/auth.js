//example imgur login response
//http://localhost:8080/oauth2/callback#access_token=e355fe58e2acf5e86758e17e08a29e0a076e43ec&expires_in=315360000&token_type=bearer&refresh_token=c1d68b5256447d5f32f90ca17272a4f562fbced6&account_username=Yoduh88&account_id=33329788

const state = {
    // token: window.localStorage.getItem('google_token'),
    user: null,
    userId: null
};

const getters = {
    isAuthenticated: state => !!state.user,
    user: state => state.user
};

const actions = {
    login: ({ commit }, user) => {
        commit('setUser', user);
    },

    finalizeLogin: ({ commit }, user) => {
        const div = user.sub.indexOf('|');
        const id = user.sub.slice(div + 1);
        user.id = id;
        commit('setUser', user);
        // commit('setToken', null);
        // window.localStorage.setItem('google_token', query.access_token);
    },

    logout: ({ commit }) => { 
        // commit('setToken', null);
        commit('setUser', null);
        // window.localStorage.removeItem('google_token');
    },
};

const mutations = {
    setToken: (state, token) => {
        state.token = token;
    },
    setUser: (state, user) => {
        state.user = user;
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};