namespace Application.Interfaces.IUseCases.IMovieServices
{
    public interface IExistsMovieTitleService
    {
        public Task<bool> ExistMovieTitle(string title);
    }
}
