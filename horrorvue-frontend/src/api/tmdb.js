import axios from 'axios';

const API_KEY = '4570328ac40b548442b90446b646e328';
const ROOT_URL = 'https://api.themoviedb.org'
export default {
    fetchMovies(search) {
        let results = null;
        return axios.get(`${ROOT_URL}/3/search/movie?api_key=${API_KEY}&language=en-US&query=${search}&page=1&include_adult=false`)
        .then(res => {
            if (res.status == 200) {
                results = res.data.results.map(result => {
                    // "id" is already an auto generated key in our database so we have to rename the id on the returned object
                    delete result.id
                    return { ...result, tmdId: result.id}
                });
                return results;
            }
        })
    },

    // uploadImages(images, token) {
    //     const promises = Array.from(images).map(image => { // returns array of promises
    //         const formData = new FormData(); // turns file reference into actual file
    //         formData.append('image', image);
    //         return axios.post(`${ROOT_URL}/3/image`, formData, {
    //             headers: {
    //                 Authorization: `Bearer ${token}`
    //             }
    //         })
    //     });
    //     return Promise.all(promises); // waits for all promises to resolve
    // }
}