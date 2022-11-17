using ShareVideosAPI.Middlewares.Validators;
using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Models.Category
{
    public class UpdateCategoryInput
    {
        [CustomStringLength(50, 2)]
        public string? Title { get; set; }

        [CustomStringLength(50, 2)]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", 
            ErrorMessage = "Color value must be a hexadecimal format")]
        public string? Color { get; set; }
    }
}
