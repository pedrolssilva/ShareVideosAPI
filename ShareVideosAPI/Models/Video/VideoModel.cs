using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPI.Models.Video
{
    public class VideoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Descriptin
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string? Url { get; set; }
    }
}
