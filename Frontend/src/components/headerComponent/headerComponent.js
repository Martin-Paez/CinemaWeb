import SelectOptionsController from "../selectOptionsComponent/selectOptionsController.js";

function getSearchIcon()
{
    return `
        <span class="input-group-text" id="basic-addon1">
            <svg width="20" height="20" class="" viewBox="0 0 20 20">
                <path
                    d="M14.386 14.386l4.0877 4.0877-4.0877-4.0877c-2.9418 2.9419-7.7115 2.9419-10.6533 0-2.9419-2.9418-2.9419-7.7115 0-10.6533 2.9418-2.9419 7.7115-2.9419 10.6533 0 2.9419 2.9418 2.9419 7.7115 0 10.6533z"
                    stroke="currentColor" fill="none" fill-rule="evenodd" stroke-linecap="round"
                    stroke-linejoin="round"></path>
            </svg>
        </span>
    `;
}

function getSearchInput(defaultSearchText, query)
{
    let a = `value=${query}`;
    return `
        <input type="text" class="form-control" placeholder="${defaultSearchText}" aria-label="searchbar" aria-describedby="search:${defaultSearchText}">
    `;
}

function getSearchBar(id, defaultSearchText, query)
{
    return `
        <div id=${id} class="headerForm input-group">
            ${getSearchIcon()}
            ${getSearchInput(defaultSearchText, query)}
        </div>
    `;
}

function render(genreOptionDtos, noSelectionValue, container, query, date, genre) 
{   
    container.innerHTML += `   
        <div id="searchBar" class="input-group">
            ${getSearchBar("searchMovie", "Buscar pelicula", query)}
            <input ${date?"value=${date}":""} id="dateSelector" type="date" class="form-control headerForm" placeholder="Value" aria-label="Value" aria-describedby="basic-addon1">
        </div>
    `;
    let searchBar = container.querySelector("#searchBar");
    let commonOptions = {
        noSelectionValue : noSelectionValue,
        container: searchBar,
        selectCssClassNames : "headerForm"
    }
    SelectOptionsController.renderForFilters({
        ... commonOptions,
        dtos : genreOptionDtos,
        defaultLabel : "Genero",
        selectedOption : genre,
        selectId : "genreSelector",
    });
    searchBar.innerHTML += `
        <button id="searchButton" class="headerForm">Buscar</button>
    `;
}

const HeaderComponent = {
    render : render
};

export default HeaderComponent;