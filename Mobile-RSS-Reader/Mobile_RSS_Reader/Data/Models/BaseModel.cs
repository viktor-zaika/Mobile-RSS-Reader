namespace Mobile_RSS_Reader.Data.Models
{   
    /// <summary>
    /// Base storage model.
    /// </summary>
    public class BaseModel
    {   
        /// <summary>
        /// Model id.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id value of model.</param>
        public BaseModel(string id)
        {
            Id = id;
        }
    }
}