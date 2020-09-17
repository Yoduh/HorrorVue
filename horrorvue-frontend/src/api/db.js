import axios from 'axios';
import store from '@/store';

const ROOT_URL = 'https://localhost:5001'
export default {
    newCollection(collection, name) {
        console.log('sending collection', collection);
        console.log('user', store.getters.user.getId());
        const request = {
            createdOn: new Date(),
            updatedOn: new Date(),
            id: 0,
            UserId: store.getters.user.getId(),
            Name: name,
            ...collection
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
    }
}