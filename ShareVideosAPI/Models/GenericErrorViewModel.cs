namespace ShareVideosAPI.Models
{
    /// <summary>
    /// Generic error object for requests
    /// </summary>
    public class GenericErrorViewModel
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
