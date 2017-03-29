namespace Mobile_RSS_Reader.Converters
{
    /// <summary>
    /// Describe requirements for html to plain text converter instance.
    /// </summary>
    public interface IHtmlToPlainTextConverter
    {
        /// <summary>
        /// Converts HTML text to plain text by simplifying structure.
        /// </summary>
        /// <param name="html">HTML text for converting</param>
        /// <returns>Plain text or empty string in case of failure</returns>
        string ConvertToPlainText(string html);
    }
}