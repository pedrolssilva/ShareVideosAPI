using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Services.Entities
{
    public class Video
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Title { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string? Description { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Url(ErrorMessage ="url must to have a valid format")]
        public string? Url { get; set; }
    }
}
