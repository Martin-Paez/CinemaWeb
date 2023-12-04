import MovieEndpoints from "./movieEndpoints.js";
import {defaultApiResponseManager} from "../apiUtils.js";

async function updateMovie(id)
{
    let result = await MovieEndpoints.updateMovie(id);
    return defaultApiResponseManager(result);
};

async function getMovieById(id)
{
    let result = await MovieEndpoints.getMovieById(id);
    return defaultApiResponseManager(result);
};

const MovieRepository = {
    getMovieById,
    updateMovie
};

export default MovieRepository;