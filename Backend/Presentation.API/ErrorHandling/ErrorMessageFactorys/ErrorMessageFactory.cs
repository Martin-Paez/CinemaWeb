using Application.Exceptions.Error;
using Application.Interfaces.IError;

namespace Presentation.API.ErrorHandling.ErrorMessageFactorys
{
    public class ErrorMessageFactory : IErrorMessageFactory
    {
        private readonly IErrorTemplateParser _templateFactory;
        private readonly IDictionary<int, ErrorTemplate> _preloadTemplates;
        private readonly IDictionary<string, string> _entitiesName;

        public ErrorMessageFactory(IErrorTemplateParser templateFactory)
        {
            _templateFactory = templateFactory;
            _preloadTemplates = new Dictionary<int, ErrorTemplate>();
            _entitiesName = new Dictionary<string, string>();
        }

        private string ReadName(string section, string key)
        {
            string name;
            if (!_entitiesName.ContainsKey(key))
            {
                var names = _templateFactory.GetSection(section);
                name = _templateFactory.GetValue(key, names);
                _entitiesName.Add(key, name);
            }
            else
                name = _entitiesName[key];
            return name;
        }

        private string ReadName<Entity>()
        {
            string key = typeof(Entity).Name;
            return ReadName("entity-name", key);
        }

        private string CreateErrorTemplate(
            int code,
            string section,
            string errorName,
            params string[] buildParameters)
        {
            ErrorTemplate template;
            if (!_preloadTemplates.ContainsKey(code))
            {
                template = _templateFactory.CreateErrorTemplate(
                    section,
                    errorName
                );
                _preloadTemplates.Add(code, template);
            }
            else
                template = _preloadTemplates[code];
            return template.Build(buildParameters);
        }

        public string InvalidTicketsAmount()
        {
            return CreateErrorTemplate(
                2001,
                "tickets",
                "invalid-tickets-amount"
           );
        }

        public string InsuficientSeats(int occupiedSeats)
        {
            return CreateErrorTemplate(
                2002,
                "tickets",
                "insuficient-seats",
                occupiedSeats.ToString()
            );
        }

        public string OverlappingShows()
        {
            return CreateErrorTemplate(3001, "shows", "overlapping-shows");
        }

        public string ElapsedDate()
        {
            return CreateErrorTemplate(3002, "shows", "elapsed-date");
        }

        public string ElapsedTime()
        {
            return CreateErrorTemplate(3003, "shows", "elapsed-time");
        }

        public string unavailableTitle()
        {
            return CreateErrorTemplate(3004, "shows", "unavailable-title");
        }

        public string NoFilterResults()
        {
            return CreateErrorTemplate(1001, "common", "no-filter-results");
        }

        public string Empty(string thePropoerty)
        {
            return CreateErrorTemplate(1002, "common", "empty", thePropoerty);
        }

        public string InvalidLength(
            string thePropoerty,
            string currentLength
            )
        {
            return CreateErrorTemplate(
                1003,
                "common",
                "invalid-length",
                thePropoerty,
                currentLength
            );
        }

        public string InvalidIdFormat<Entity>()
        {
            return CreateErrorTemplate(
                1004,
                "common",
                "invalid-id-format",
                ReadName<Entity>()
            );
        }

        public string NotFoundById<Entity>()
        {
            return CreateErrorTemplate(
                1005,
                "common",
                "not-found-by",
                ReadName("entity-name", "id-of") + ReadName<Entity>()
            );
        }
        public string InvalidTimeFormat()
        {
            return CreateErrorTemplate(1006, "common", "invalid-time-format");
        }

        public string InvalidDateFormat()
        {
            return CreateErrorTemplate(1007, "common", "invalid-date-format");
        }
    }
}