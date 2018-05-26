using AzureSearchServices.Contracts;

namespace AzureSearch.Configuration
{

    public class AzureSearchConfiguration : IAzureSearchConfiguration
    {
        /// <summary>
        /// Gets the admin key.
        /// </summary>
        /// <value>
        /// The admin key.
        /// </value>
        public string SearchAdminKey { get; set; }

        /// <summary>
        /// Gets the query key.
        /// </summary>
        /// <value>
        /// The query key.
        /// </value>
        public string SearchQueryKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the search service.
        /// </summary>
        /// <value>
        /// The name of the search service.
        /// </value>
        public string SearchServiceName { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSearchConfiguration"/> class.
        /// </summary>
        /// <param name="searchAdminKey">The search admin key.</param>
        /// <param name="searchQueryKey">The search query key.</param>
        /// <param name="searchServiceName">Name of the search service.</param>
        public AzureSearchConfiguration(string searchAdminKey, string searchQueryKey, string searchServiceName)
        {
            this.SearchAdminKey = searchAdminKey;
            this.SearchQueryKey = searchQueryKey;
            this.SearchServiceName = searchServiceName;
        }

        /// <summary>
        /// Gets the admin key.
        /// </summary>
        /// <value>
        /// The admin key.
        /// </value>
        public string AdminKey => this.SearchAdminKey;

        /// <summary>
        /// Gets the query key.
        /// </summary>
        /// <value>
        /// The query key.
        /// </value>
        public string QueryKey => this.SearchQueryKey;

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName => this.SearchServiceName;
    }
}
