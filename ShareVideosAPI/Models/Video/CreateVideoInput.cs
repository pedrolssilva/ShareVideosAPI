using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Models.Video
{
    public class CreateVideoInput
    {
        /// <summary>
        /// Title
        /// </summary>
        [Required(AllowEmptyStrings= false, ErrorMessage ="Title is required")]
        [MinLength(2, ErrorMessage = "Title must have at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must have a maximum of 50 characters")]
        public string? Title { get; set; }

        /// <summary>
        /// Descriptin
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        [MinLength(5, ErrorMessage = "Description must have at least 5 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Url(ErrorMessage = "Url format is invalid")]
        public string? Url { get; set; }
    }
}
