import MovieDetails from "./movieDetailsView.js";
import MovieRepository from "../../api/movie/movieRepository.js";
import ShowRepository from "../../api/show/showRepository.js";

document.addEventListener('DOMContentLoaded', async () => 
{
    let urlParams = new URLSearchParams(window.location.search);
    let movieId = urlParams.get("movieId"); 
    let movie = await MovieRepository.getMovieById(movieId);
    movie.funciones.forEach(show => {
        show["pelicula"] = movie
    });
    MovieDetails.render(movie);
});