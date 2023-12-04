using Application.Exceptions.Error;

namespace Presentation.API.ErrorHandling.ErrorMessageFactorys
{
    public interface IErrorTemplateParser
    {
        public ErrorTemplate CreateErrorTemplate(
           string sectionName,
           string errorName
        );

        public IConfigurationSection GetSection(
            string sectionName
        );

        public string GetValue(
            string key,
            IConfigurationSection section
        );
    }
}
