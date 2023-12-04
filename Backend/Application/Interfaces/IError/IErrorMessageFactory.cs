namespace Application.Interfaces.IError
{
    public interface IErrorMessageFactory
    {
        public string InvalidTicketsAmount();
        public string InsuficientSeats(int occupiedSeats);
        public string NoFilterResults();
        public string OverlappingShows();
        public string ElapsedDate();
        public string ElapsedTime();
        public string Empty(string thePropoerty);
        public string InvalidLength(
            string thePropoerty,
            string currentLength
        );
        public string InvalidIdFormat<Entity>();
        public string NotFoundById<Entity>();
        public string InvalidTimeFormat();
        public string InvalidDateFormat();
        public string unavailableTitle();
    }
}
