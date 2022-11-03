namespace ShareVideosAPI.Models
{
    /// <summary>
    /// Model for missing fields errors in requests
    /// </summary>
    public class FieldValidatorViewModelOutput
    {
        /// <summary>
        /// Data validation errors.
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Instantiate FieldValidatorViewModelOutput
        /// </summary>
        /// <param name="errors"></param>
        public FieldValidatorViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
