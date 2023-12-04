import HeaderController from "../../components/headerComponent/headerController.js";
import YoutubeAPI from "./youtubeApi.js";
import DateSelectorController from "../../components/dateSelector/dateSelectorController.js";
import BuyModalController from "../../components/buyModalComponent/buyModalController.js";

const state = {
    title : ""
}

function render(movie) 
{
    let header = document.querySelector("#mainHeader");
    HeaderController.render(header);
    renderMovieDetails(movie);
    YoutubeAPI.render(movie.trailer);
    state.title = movie.titulo;
};

function renderMovieDetails(movie)
{
    let container = document.getElementById('movieDetails');
    container.innerHTML = `
        <div id="movieDetailBody">
            <div id="youtubePlayer">
            </div>
            <div id="descriptionColumn">
                <div id="buyIcon">
                    <div class="container" data-bs-toggle="modal" data-bs-target="#buyModal">
                        <div class="card_box text-white">
                            <div class="container">
                                <div class="left-side">
                                    <div class="card">
                                        <div class="card-line"></div>
                                        <div class="buttons"></div>
                                    </div>
                                    <div class="post">
                                        <div class="post-line"></div>
                                        <div class="screen">
                                            <div class="dollar">$</div>
                                        </div>
                                        <div class="numbers"></div>
                                        <div class="numbers-line2"></div>
                                    </div>
                                </div>
                            </div>
                            <span></span><!--No quitar el span-->
                        </div>
                    </div>
                </div>
                <div id="movieDescription">
                    <h1>${movie.titulo}</h1>
                    <span>${movie.genero.nombre}</span>
                    <p>${movie.sinopsis}</p>            
                </div>
            </div>
        </div>
    `;
    BuyModalController.render(movie.funciones, "", container);
    document
        .getElementById("buyIcon")
        .addEventListener('click', event => {
            document.getElementById("buyError").style.display = "none";
            let modalTitle = document.querySelector(`#buyModalLabel`);
            modalTitle.innerHTML = `Conseguí tus Entradas para ver ${state.title}`;
            DateSelectorController.setMovieTitle(state.title);
            DateSelectorController.initDateSelector(movie.funciones, "");
    });
}

const MovieDetails = {
    render : render
}

export default MovieDetails;