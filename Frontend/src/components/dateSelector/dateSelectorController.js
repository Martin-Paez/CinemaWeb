import DateSelectorComponent from "./dateSelectorComponent.js";

const state = {
    allShows: [],
    movie: ""
}

function setMovieTitle(title)
{
    state.movie = title;
}

function render(shows, date, container)
{
    DateSelectorComponent.render(container);
    initDateSelector(shows, date);
}

function initDateSelector(shows, date)
{
    const SUGGESTION_RANGE = 5;
    const dateInput = document.getElementById("dateInput");
    const hourInput = document.getElementById("hourInput");
    const suggestionsContainer = document.getElementById("dateSuggestionsContainer");
    const dateSuggestionsSelect = document.getElementById("dateSuggestionsSelect");
    const noFunctionsText = document.getElementById("noFunctionsText");

    hide(noFunctionsText);

    if(date)
    {
        dateInput.value = date;
        dateInput.disabled = true;
    }

    state.allShows = shows;

    dateInput.addEventListener("input", () => {filterShows();});

    function filterShows()
    {
        const selectedDate = dateInput.value;
        let shows = state.allShows.filter(show => show.pelicula.titulo === state.movie);
        let dates = [...new Set(getDates(shows))];
        if (dates.includes(selectedDate))
            dates = [selectedDate];
        else
            dates = updateDateSuggestions(dates, state.movie, selectedDate);
        if (dates.length == 0)
            show(noFunctionsText);
        let filteredShows = filterByShowTime(dates, shows, "", state.movie);
        filteredShows = getFormatedDates(filteredShows);
        filteredShows = [...new Set(filteredShows)];
        dateSuggestionsSelect.innerHTML = "";
        filteredShows.forEach(show => {
            dateSuggestionsSelect.innerHTML += `
                <option value=${show.showId}>${show.date} ${show.time} ${show.screen}</option>
            `;
        });
        
    }

    function updateDateSuggestions(dates, title, selectedDate) {
        let shows = dates.filter(date => {
            return  getDaysBetween(date, selectedDate) <= SUGGESTION_RANGE;
        });
        if (shows.length == 0)
            shows.push(findNearestDate(dates));
        return shows;
    }
}

function getFormatedDates(shows) {
    const dates = shows.map(show => {return { date: new Date(show.fecha), time: show.horario, screen: (show.sala===undefined)?"" : show.sala.nombre, showId: show.funcionId}} );
    dates.sort((a, b) => a.date - b.date);
    dates.forEach(show => show.date = show.date.toLocaleString().replace(',', '').substring(0,10));
    return dates;
  }

function filterByShowTime(dates, shows, selectedTime, movie)
{
    if(dates.length == 0 || selectedTime === "")
    {
        let filteredShows = [];
        dates.forEach(date => {
            shows.forEach( show => {
                if( show.fecha.substring(0,10) === date &&
                    show.pelicula.titulo === movie
                     )
                    filteredShows.push(show);
            })
        });
        return filteredShows;
    }
        
    let time = new Date('2023-01-01 ' + selectedTime);
    let filteredShows = [];
    dates.forEach(date => {
        shows.forEach( show => {
            let t = new Date('2023-01-01 ' + show.horario);
            let diff = Math.abs(time - t) / (1000 * 60 * 60);
            if(show.pelicula.titulo === movie && 
                show.fecha === date &&
                diff <= 5)
                filteredShows.push(show); 
        })
    });
    return filteredShows;
}

function getDates(shows)
{
    let dates = [];
    shows.forEach( show => {
        dates.push(show.fecha.substring(0,10));
    });
    return dates;
}

function show(htmlTag) {
    htmlTag.style.display = "block";
}

function hide(htmlTag) {
    htmlTag.style.display = "none";
}

function formatDate(date) {
    const [year, month, day] = date.substring(0,10).split("-");
    return `${day}/${month}/${year}`;
}

function getDaysBetween(fecha1, fecha2) {
    const date1 = new Date(fecha1);
    const date2 = new Date(fecha2);
    const millis = Math.abs(date2 - date1);
    const days = millis / (1000 * 60 * 60 * 24);
    return days;
}

function findNearestDate(dates, selectedDate) {
    const sortedDates = dates.sort((a, b) => {
        const diffA = Math.abs(new Date(a) - new Date(selectedDate));
        const diffB = Math.abs(new Date(b) - new Date(selectedDate));
        return diffA - diffB;
    });
    return sortedDates[sortedDates.length-1];
}

const DateSelectorController = {
    render : render,
    initDateSelector : initDateSelector,
    setMovieTitle : setMovieTitle
};

export default DateSelectorController;
