using Application.Interfaces.IError;
using System.Globalization;
using System.Text;

namespace Application.Validators
{
    public class DateAndTimeValidator
    {
        private readonly IErrorMessageFactory _errorMsgFactory;

        public DateAndTimeValidator(IErrorMessageFactory errorMessageFactory)
        {
            _errorMsgFactory = errorMessageFactory;
        }

        public DateTime? ValidateDate(
            string aDateTime,
            StringBuilder errorBuilder
            )
        {
            DateTime date;
            bool ok = DateTime.TryParseExact(
                aDateTime,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date
            );
            if (ok)
                return date;
            errorBuilder.Append(_errorMsgFactory.InvalidDateFormat());
            return null;
        }

        public TimeSpan? ValidateTime(
            string? aTime,
            StringBuilder errorBuilder
            )
        {
            TimeSpan time;
            var ok = TimeSpan.TryParseExact(
                aTime,
                "hh\\:mm",
                CultureInfo.InvariantCulture,
                out time
            );
            if (ok)
                return time;
            errorBuilder.Append(_errorMsgFactory.InvalidTimeFormat());
            return null;
        }
    }
}
