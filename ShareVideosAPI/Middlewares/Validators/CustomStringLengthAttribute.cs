using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShareVideosAPI.Services.Database;
using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Middlewares.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomStringLengthAttribute : ValidationAttribute
    {
        public uint MaximumLength { get; }
        public uint MinimumLength { get; }

        public CustomStringLengthAttribute(uint maximumLength = uint.MaxValue, uint minimumLength = uint.MinValue)
        {
            MaximumLength = maximumLength;
            MinimumLength = minimumLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? strValue = (string?)value;
            if (!string.IsNullOrEmpty(strValue))
            {
                if (strValue.Length > MaximumLength)
                {
                    return new ValidationResult($"{validationContext.DisplayName} " +
                        $"excedeed {MaximumLength} characters.");
                }
                if (strValue.Length < MinimumLength)
                {
                    return new ValidationResult($"{validationContext.DisplayName} must " +
                        $"have at least {MinimumLength} characters.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
