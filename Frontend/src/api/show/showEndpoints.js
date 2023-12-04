import { HttpMethod,  generateURL, defaultOptions, safeFetch } from '../apiUtils.js'; 
import TICKET_SALES_API_URL from '../ticketSalesApiConfig.js';

const base = () => {
    return generateURL(TICKET_SALES_API_URL, "/Funcion");
};
    
const byId = (id) => {
    return `${base()}/${id}`;
};

const tickets = (id) => {
    return `${byId(id)}/tickets`;
};

const filter = (query, date, genre) => {
    let url = `${base()}`;
    if(query||date||genre)
    {
        url += '?'
        if(date)
        {
            url += `fecha=${date}`;
            if(query||genre)
                url += '&';
        }
        if(query)
        {
            url += `titulo=${query}`;
            if(genre)
                url += '&';
        }
        if(genre)
            url += `genero=${genre}`;
    }
    return url;
}

async function getFilteredShows(query, date, genre)
{
    const url = filter(query, date, genre);
    const options = defaultOptions(HttpMethod.GET); 
    return await safeFetch(url, options);
};

async function getById(id)
{
    const url = byId(id);
    const options = defaultOptions(HttpMethod.GET); 
    return await safeFetch(url, options);
};

async function createShow(show)
{
    const url = base();
    const options = defaultOptions(HttpMethod.POST, show); 
    return await safeFetch(url, options);
};

async function deleteShow(id)
{
    const url = byId(id);
    const options = defaultOptions(HttpMethod.DELET); 
    return await safeFetch(url, options);
};

async function getShowTickets(id)
{
    const url = tickets(id);
    const options = defaultOptions(HttpMethod.GET); 
    return await safeFetch(url, options);
};

async function addShowTickets(id, dto)
{
    const url = tickets(id);
    const options = defaultOptions(HttpMethod.POST, dto); 
    return await safeFetch(url, options);
};

const ShowEndpoints = {
    getFilteredShows,
    getById,
    createShow,
    deleteShow,
    getShowTickets,
    addShowTickets
};

export default ShowEndpoints;