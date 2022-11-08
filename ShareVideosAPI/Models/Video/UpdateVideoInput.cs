using ShareVideosAPI.Middlewares.Validators;
using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Models.Video
{
    public class UpdateVideoInput
    {
        /// <summary>
        /// Title
        /// </summary>
        [CustomStringLength(50, 2)]
        public string? Title { get; set; }

        /// <summary>
        /// Descriptin
        /// </summary>
        [CustomStringLength(minimumLength: 5)]
        public string? Description { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Url(ErrorMessage = "Url format is invalid")]
        public string? Url { get; set; }
    }
}
