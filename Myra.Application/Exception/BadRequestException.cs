using FluentValidation.Results;

namespace Myra.Application.Exception
{
    public class BadRequestException : IOException
    {
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
        public BadRequestException(string prop, string message) : this()
        {
            Errors.Add(new ValidationFailure(prop, message));
        }

        public BadRequestException(ValidationResult validationResult) : this()
        {
            Errors = validationResult.Errors;
        }

        public BadRequestException() : base("Invalid requisition, check your data and try again.")
        {
        }

    }
}
