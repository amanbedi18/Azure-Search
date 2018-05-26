namespace AzureSearchServices.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AzureSearchServices.Contracts;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// The Index Service
    /// </summary>
    /// <seealso cref="AzureSearchServices.Services.AzureSearchService" />
    /// <seealso cref="AzureSearchServices.Contracts.IIndexService" />
    public class IndexService : AzureSearchService, IIndexService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexService"/> class.
        /// </summary>
        /// <param name="azureSearchConfiguration">The azure search configuration.</param>
        public IndexService(IAzureSearchConfiguration azureSearchConfiguration)
            : base(azureSearchConfiguration)
        {
        }

        /// <summary>
        /// Creates the or update asynchronous.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <returns>Returns a task</returns>
        public async Task CreateOrUpdateAsync(Index definition)
        {
            using (var searchService = this.GetAdminSearchServiceClient())
            {
                await searchService.Indexes.CreateOrUpdateAsync(definition).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the index asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a task</returns>
        public async Task<Index> GetIndexAsync(string name)
        {
            Index result = null;
            using (var searchService = this.GetAdminSearchServiceClient())
            {
                try
                {
                    result = await searchService.Indexes.GetAsync(name).ConfigureAwait(false);
                }
                catch (CloudException ex)
                {
                    if (ex.Response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all indexes asynchronous.
        /// </summary>
        /// <returns>Returns a task of list of index</returns>
        public async Task<IEnumerable<Index>> GetAllIndexesAsync()
        {
            IEnumerable<Index> result = null;
            using (var searchService = this.GetAdminSearchServiceClient())
            {
                var response = await searchService.Indexes.ListAsync().ConfigureAwait(false);
                result = response.Indexes;
            }

            return result;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a task</returns>
        public async Task DeleteAsync(string name)
        {
            using (var searchService = this.GetAdminSearchServiceClient())
            {
                await searchService.Indexes.DeleteAsync(name).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Exists the index asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a task of bool</returns>
        public async Task<bool> ExistsIndexAsync(string name)
        {
            bool result = false;
            using (var searchService = this.GetAdminSearchServiceClient())
            {
                result = await searchService.Indexes.ExistsAsync(name).ConfigureAwait(false);
            }

            return result;
        }
    }
}
