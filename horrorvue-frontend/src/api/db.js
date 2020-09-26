import axios from 'axios';
import store from '@/store';

const ROOT_URL = 'https://localhost:5001'
export default {
    newCollection(collection, name) {
        const request = {
            id: 0,
            UserId: store.getters.user.id,
            Name: name,
            Movies: [
                ...collection
            ]
        }
        axios.post(`${ROOT_URL}/api/collection`, request)
        .then(res => {
            if (res.status == 200) {
                console.log('save success');
                console.log(res);
            }
            else {
                console.log('save error');
                console.log(res);
            }
            return;
        });
    },
    // userId: google id
    async getUser(userId) {
        return axios.get(`${ROOT_URL}/api/user/${userId}`)
        .then(res => {
            return res.data;
        })
        .catch(err => {
            if (err.response.status === 404) {
                return this.createUser(store.getters.user);
            } else {
                console.log('err', err.response);
            }
        });
    },
    createUser(user) {
        const div = user.sub.indexOf('|');
        const googleId = user.sub.slice(div + 1);
        const newUser = {
            FirstName: user.given_name,
            LastName: user.family_name,
            GoogleId: googleId,
            Email: user.email
        }
        return axios.post(`${ROOT_URL}/api/user`, newUser)
        .then(res => {
            return res.data.data;
        })
        .catch(err => {
            console.log('create err', err);
            return null;
        });
    }
}