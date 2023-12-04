import HeaderComponent from "./headerComponent.js";
import GenreRepository from "../../api/genre/genreRepository.js"

const NO_SELECTED_OPTION = null;

async function render(container, query, date, genre)
{
    let genres = await GenreRepository.getAll();
    HeaderComponent.render(
        createDtos(genres), 
        NO_SELECTED_OPTION, 
        container,
        query, 
        date, 
        genre
    );
    initSearchEvents();
}

function createDtos(genres) 
{   
    let dtos = [];
    genres
        ?.forEach(genre => 
        {
            dtos.push({
                label: genre.nombre,
                id: genre.id
            });
        });
    return dtos;
}

function initSearchEvents()
{
    let input = document.querySelector("#searchMovie input");
    input.addEventListener("keypress", function(event) {
        if (event.key === "Enter")
          goSearch(event.target);
      });
    document
      .getElementById("searchButton")
      .addEventListener("click", function() {
          goSearch(input);
      });
}

function goSearch(input) {
    var date = document
        .getElementById("dateSelector")
        .value;        
    var genre = document
        .getElementById("genreSelector")
        .selectedOptions[0];
    let url = "http://127.0.0.1:5500/pages/searchResult/searchResult.html";
    url += "?date=" + encodeURIComponent(date);
    if(genre.value !== "null")
    {
        url += "&genre=" + encodeURIComponent(genre.value);
        url += "&genreName=" + encodeURIComponent(genre.label);
    }
    url += "&query=" + encodeURIComponent(input.value);
    window.location.href = url;
}

const HeaderController = {
    render : render
};

export default HeaderController;