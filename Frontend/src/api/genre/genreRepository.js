import GenreEndpoints from "./genreEndpoints.js";
import {defaultApiResponseManager} from "../apiUtils.js";

async function getAll()
{
    let result = await GenreEndpoints.getAll();
    return defaultApiResponseManager(result);
};

const GenreRepository = {
    getAll
};

export default GenreRepository;