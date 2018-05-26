namespace AzureSearchServices.Contracts
{
    /// <summary>
    /// The Azure search configuration
    /// </summary>
    public interface IAzureSearchConfiguration
    {
        /// <summary>
        /// Gets the admin key.
        /// </summary>
        /// <value>
        /// The admin key.
        /// </value>
        string AdminKey { get; }

        /// <summary>
        /// Gets the query key.
        /// </summary>
        /// <value>
        /// The query key.
        /// </value>
        string QueryKey { get; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        string ServiceName { get; }
    }
}
