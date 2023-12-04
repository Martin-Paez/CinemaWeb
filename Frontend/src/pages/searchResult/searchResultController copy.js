import SearchResultView from "./searchResultsView.js";
import ShowRepository from "../../api/show/showRepository.js";

const DATE_SELECTOR_ID = "dateSelector";
const GENRE_SELECTOR_ID = "genreSelector";
const NO_SELECTED_OPTION = -1;

const state = {
  allShows: [],
  currentShows: [],
  filters: {}
}

document.addEventListener('DOMContentLoaded', async () => 
{
    let urlParams = new URLSearchParams(window.location.search);
    let date = urlParams.get("query");
    let genre = urlParams.get("genero");
    state.allShows = await ShowRepository.getAllShows();
    state.currentShows = state.allShows;
    SearchResultView.render(
      createDtos(state.allShows, createMovieCardDto),
      createDtos(state.allShows, createGenreOptionDto),
      NO_SELECTED_OPTION
    );
    initOnDateFilterChange(DATE_SELECTOR_ID, filterByDate);
    initOnFilterChange(GENRE_SELECTOR_ID, filterByGenre);
});

function initOnDateFilterChange(htmlSelectId, filterFunction)
{
  document
    .getElementById(htmlSelectId)
    .addEventListener('change', (event) => 
    {
      let value = event.target.value;
      if (value) {
        state.filters[htmlSelectId] = {
          filter: filterFunction,
          criteria: value
        };
      } else {
        delete state.filters[htmlSelectId];
      }
      filterSearchResult();
    });
}

function addFilters(date, genre, )
{
  state.filters[DATE_SELECTOR_ID] = {
    filter: filterByDate,
    criteria: value
  };
}

function initOnFilterChange(htmlSelectId, filterFunction)
{
  document
    .getElementById(htmlSelectId)
    .addEventListener('change', (event) => 
    {
      let value = event.target.selectedOptions[0].value;
      if(value == NO_SELECTED_OPTION)
        delete state.filters[htmlSelectId];
      else 
        state.filters[htmlSelectId] = {
          filter: filterFunction,
          criteria: value
        };
      filterSearchResult();
    });
}

function filterSearchResult()
{
  state.currentShows = state.allShows
  for (let k in state.filters) {
    if (state.filters.hasOwnProperty(k)) {
      let f = state.filters[k]
      state.currentShows = f.filter(state.currentShows, f.criteria);
    }
  }    
  IndexView.updateSearchResult(
    createDtos(state.currentShows, createMovieCardDto)
  );
}

function filterByDate(shows, dateValue)
{
  return shows?.filter( show => {
    return show.fecha.substring(0, 10) === dateValue.substring(0, 10);
  });
}

function filterByGenre(shows, genreId)
{
  return shows?.filter( show => {
    return show.pelicula.genero.id == genreId;
  });
}

function createDateOptionDto(show)
{
    return {
        label: show.fecha.substring(0,10),
        id: show.fecha
    }
}

function createGenreOptionDto(show)
{
    return {
        label: show.pelicula.genero.nombre,
        id: show.pelicula.genero.id
    }
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

function createDtos(shows, createDto) 
{   
    let uniqueValues = {};
    let dtos = [];
    shows
        ?.forEach(show => 
        {
            let dto = createDto(show);
            if (! uniqueValues[dto.id])
            {
                uniqueValues[dto.id] = true;
                dtos.push(dto);
            }
        });
    return dtos;
}