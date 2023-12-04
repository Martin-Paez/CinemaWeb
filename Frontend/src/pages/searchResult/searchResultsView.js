import HeaderController from "../../components/headerComponent/headerController.js";
import CardController from "../../components/cardComponent/cardController.js";
import BuyModalController from "../../components/buyModalComponent/buyModalController.js";
import DateSelectorController from "../../components/dateSelector/dateSelectorController.js";

function render(query, date, genre, movieCardDtos, shows) 
{
    let header = document.querySelector("#mainHeader");
    HeaderController.render(header, query===null||query==="null"?"":query, date, genre);
    renderTitle(query, date, genre, movieCardDtos);
    let container = document.querySelector(`body > section`);
    BuyModalController.render(shows, date, container);
    updateSearchResult(movieCardDtos, shows, date);
};

function renderTitle(query, date, genre, movieCardDtos)
{
    let msgs = [];
    if(genre)
        msgs.push(`de ${genre}`);
    if(date)
        msgs.push(`que se pueden ir a ver el día ${convertirFormatoFecha(date)}`);
    if(query)
        msgs.push(`cuyo titulo contiene o es contenido en la frase "${query}"`);

    let title = "  ";// dos espacios
    for(let i=0 ; i < msgs.length - 1 ; i++)
        title += msgs[i] + ', ';
    title = title.substring(0,title.length - 2);
    if(msgs.length == 1)
        title = `esultados de busqueda para peliculas ` + msgs[msgs.length-1];
    else if(msgs.length == 2)
        title = `esultados de busqueda para peliculas ` + title + " " + msgs[msgs.length-1];
    else if(msgs.length > 2)
        title = `esultados de busqueda para peliculas ` + title + ' y ' + msgs[msgs.length-1];
    else
        title = `peliculas para ver en el cine`;
    
    if(movieCardDtos.length == 0)
    {
        if(msgs.length > 0)
            title = "No se hay r" + title;
        else
            title = "No hay " + title;
    }
    else
    {
        if(msgs.length > 0)
            title = "R" + title;
        else
            title = 'Todas las ' + title;
    }
    document
        .getElementById('searchResultTitle')
        .innerHTML = title + ".";
}

function convertirFormatoFecha(fecha) {
    var date = new Date(fecha);
    var offsetMinutos = +3 * 60; 
    date.setMinutes(date.getMinutes() + offsetMinutos);
    var mes = date.getDate().toString().padStart(2, '0');
    var dia = (date.getMonth() + 1).toString().padStart(2, '0');
    var anio = date.getFullYear();
    var fechaFormateada = mes + '-' + dia + '-' + anio;
    return fechaFormateada;
  }

function updateSearchResult(movieCardDtos, shows, date)
{
    let container = document.querySelector("#searchResult")
    container.innerHTML = "";
    movieCardDtos.forEach((dto) => {
        return renderCard(dto, container);
    });
    let modalContainer = document.querySelector(`body > section`);
    let cardBody = container
    .querySelectorAll('.card-body')
    cardBody.forEach(body => {
        let link = body.querySelector("a");
        link.addEventListener('click', event => {
            document.getElementById("buyError").style.display = "none";
            let movieTitle = body.querySelector(".card-title").innerHTML;
            let modalTitle = document.querySelector(`#buyModalLabel`);
            modalTitle.innerHTML = `Conseguí tus Entradas para ver ${movieTitle}`;
            DateSelectorController.setMovieTitle(movieTitle);
            DateSelectorController.initDateSelector(shows, date);
        });
    });
    //DateSelectorController.render(shows, date, modalContainer);
}

function renderCard(movieCardDto, container)
{
    let cardClassNames = "movieCard";
    let cardButtonLabel = "Comprar";
    CardController.render (
        movieCardDto,
        cardButtonLabel,
        container,
        cardClassNames, 
    );
}

const SearchResultView = {
    render : render
};

export default SearchResultView;