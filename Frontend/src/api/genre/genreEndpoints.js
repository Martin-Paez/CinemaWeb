import {HttpMethod, generateURL, defaultOptions, safeFetch } from '../apiUtils.js'; 
import API_URL from '../ticketSalesApiConfig.js';

const base = () => {
    return generateURL(API_URL, "/Genero");
};

async function getAll()
{
    const url = base();
    const options = defaultOptions(HttpMethod.GET); 
    return await safeFetch(url, options);
};

const GenreEndpoints = {
    getAll
};

export default GenreEndpoints;