using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Models.Category
{
    public class CreateCategoryInput
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title must have at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must have a maximum of 50 characters")]
        public string? Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Color is required")]
        [StringLength(7, MinimumLength = 3, ErrorMessage = "Color must have 7 characters")]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", 
            ErrorMessage = "Color value must be a hexadecimal format")]
        public string? Color { get; set; }
    }
}
