import ShowEndpoints from "./showEndpoints.js";
import { defaultApiResponseManager } from "../apiUtils.js";

async function getFilteredShows(query, date, genre)
{
    let result = await ShowEndpoints.getFilteredShows(query, date, genre);
    return defaultApiResponseManager(result);
};

async function getById(id)
{
    let result = await ShowEndpoints.getById(id);
    return defaultApiResponseManager(result);
};

async function createShow(show)
{
    let result = await ShowEndpoints.createShow(show);
    return defaultApiResponseManager(result);
};

async function deleteShow(id)
{
    let result = await ShowEndpoints.deleteShow(id);
    return defaultApiResponseManager(result);
};

async function getShowTickets(id)
{
    let result = await ShowEndpoints.getShowTickets(id);
    return defaultApiResponseManager(result);
};

async function addShowTickets(id, dto)
{
    let result = await ShowEndpoints.addShowTickets(id, dto);
    return defaultApiResponseManager(result);
};

const ShowRepository = {
    getFilteredShows,
    getById,
    createShow,
    deleteShow,
    getShowTickets,
    addShowTickets
};

export default ShowRepository;