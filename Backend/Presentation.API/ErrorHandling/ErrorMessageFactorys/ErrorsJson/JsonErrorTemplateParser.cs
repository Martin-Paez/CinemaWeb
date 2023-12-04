using Application.Exceptions.Error;
using System.Text;

namespace Presentation.API.ErrorHandling.ErrorMessageFactorys.ErrorsJson
{
    internal class ErrorJsonFileCorrupted : Exception { }

    public class JsonErrorTemplateParser : IErrorTemplateParser
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILogger<JsonErrorTemplateParser> _logger;
        const int NO_CODE_FOUND = -1;

        public JsonErrorTemplateParser(
            ILogger<JsonErrorTemplateParser> logger
            )

        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./ErrorHandling/ErrorMessageFactorys/ErrorsJson/errors.json")
                .Build();
            _logger = logger;
        }

        public ErrorTemplate CreateErrorTemplate(
            string sectionName,
            string errorName
            )
        {
            ErrorTemplate template;
            try
            {
                var error = GetError(sectionName, errorName);
                template = new ErrorTemplate(
                    GetCode(errorName, error),
                    GetMessage(errorName, error)
                );
            }
            catch (ErrorJsonFileCorrupted)
            {
                var msg = new StringBuilder()
                    .Append(sectionName)
                    .Append(":")
                    .Append(errorName)
                    .ToString();
                template = new ErrorTemplate(NO_CODE_FOUND, msg);
            }
            return template;
        }

        private IConfigurationSection GetError(
            string sectionName,
            string errorName
            )
        {
            var section = GetSection(sectionName);
            return GetSection(
                section,
                errorName
            );
        }

        public IConfigurationSection GetSection(
            string sectionName
            )
        {
            return GetSection(_configuration, sectionName);
        }

        private IConfigurationSection GetSection(
            IConfiguration configuration,
            string sectionName
            )
        {
            var section = configuration.GetSection(sectionName);
            if (section.Exists())
                return section;
            _logger.LogError(
                "ErrorFactory no encontro la " +
                "definicion de " + sectionName
            );
            throw new ErrorJsonFileCorrupted();
        }

        private int GetCode(
            string errorName,
            IConfigurationSection section
            )
        {
            int errorCode;
            if (int.TryParse(section["code"], out errorCode))
                return errorCode;
            _logger.LogError(
                    "ErrorFactory no encontro el codigo " +
                    "del error " + errorName
                );
            throw new ErrorJsonFileCorrupted();
        }

        private string GetMessage(
            string name,
            IConfigurationSection section
            )
        {
            string? errorMessage = section["message"];
            if (!string.IsNullOrEmpty(errorMessage))
                return errorMessage;
            _logger.LogError(
                    "ErrorFactory no encontro el " +
                    "mensaje del error " + name
                );
            throw new ErrorJsonFileCorrupted();
        }

        public string GetValue(
            string key,
            IConfigurationSection section
            )
        {
            string? errorMessage = section[key];
            if (!string.IsNullOrEmpty(errorMessage))
                return errorMessage;
            _logger.LogError(
                    "ErrorFactory no encontro la clave " + key
                );
            throw new ErrorJsonFileCorrupted();
        }
    }
}
