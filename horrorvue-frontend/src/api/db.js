import axios from 'axios';
import store from '@/store';

const ROOT_URL = 'https://localhost:5001'
export default {
    newCollection(collection, name) {
        console.log('sending collection', collection);
        const request = {
            id: 0,
            UserId: store.getters.user.id,
            Name: name,
            Movies: [
                ...collection
            ]
        }
        console.log(request);
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
    getUser(userId) {
        return axios.get(`${ROOT_URL}/api/user/${userId}`);
    }
}