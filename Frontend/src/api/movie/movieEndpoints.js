import {HttpMethod, generateURL, defaultOptions, safeFetch } from '../apiUtils.js'; 
import API_URL from '../ticketSalesApiConfig.js';

const base = () => {
    return generateURL(API_URL, "/Pelicula");
};

const byId = (id) => {
    return `${base()}/${id}`;
};
  
async function updateMovie(id)
{
    const url = byId(id);
    const options = defaultOptions(HttpMethod.PATCH); 
    return await safeFetch(url, options);
};

async function getMovieById(id)
{
    const url = byId(id);
    const options = defaultOptions(HttpMethod.GET); 
    return await safeFetch(url, options);
};

const MovieEndpoints = {
    getMovieById,
    updateMovie
};

export default MovieEndpoints;