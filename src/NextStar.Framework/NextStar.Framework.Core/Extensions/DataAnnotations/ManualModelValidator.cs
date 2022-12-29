namespace System.ComponentModel.DataAnnotations;

public static class ManualModelValidator
{
    public static IEnumerable<ValidationResult> Validate<T>(T model) where T : class, new()
    {
        model = model ?? new T();

        var validationContext = new ValidationContext(model);

        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}