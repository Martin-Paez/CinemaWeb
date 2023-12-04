import SearchResultView from "./searchResultsView.js";
import ShowRepository from "../../api/show/showRepository.js";

document.addEventListener('DOMContentLoaded', async () => 
{
    let urlParams = new URLSearchParams(window.location.search);
    let genre = urlParams.get("genre"); 
    let query = formatQueryPara(urlParams.get("query")); 
    let date = urlParams.get("date"); 
    let genreName = genre ? formatQueryPara(urlParams.get("genreName")) : ""; 
    let shows = await ShowRepository.getFilteredShows(
        query,
        date,
        genre
    );
    SearchResultView.render(
        query,
        date,
        genreName,
        createDtos(shows),
        shows
    );
});

function formatQueryPara(query)
{
    return (query === 'null') ? null : query;
}

function formatGenreParam(genre)
{
    return genre == -1 ? null : genre;
}

function createMovieCardDto(show)
{
    return {
        id: show.pelicula.peliculaId,
        img : show.pelicula.poster,
        title : show.pelicula.titulo,
        description : show.pelicula.genero.nombre
    }
}

function createDtos(shows) 
{   
    let uniqueValues = {};
    let dtos = [];
    shows
        ?.forEach(show => 
        {
            let dto = createMovieCardDto(show);
            if (! uniqueValues[dto.id])
            {
                uniqueValues[dto.id] = true;
                dtos.push(dto);
            }
        });
    return dtos;
}