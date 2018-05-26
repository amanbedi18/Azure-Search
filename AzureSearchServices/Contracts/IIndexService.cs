namespace AzureSearchServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.Search.Models;

    /// <summary>
    /// The index service contract
    /// </summary>
    public interface IIndexService
    {
        /// <summary>
        /// Creates the or update asynchronous.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <returns>Returns Task</returns>
        Task CreateOrUpdateAsync(Index definition);

        /// <summary>
        /// Gets the index asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns Task of index</returns>
        Task<Index> GetIndexAsync(string name);

        /// <summary>
        /// Exists the index asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns Task of bool</returns>
        Task<bool> ExistsIndexAsync(string name);

        /// <summary>
        /// Gets all indexes asynchronous.
        /// </summary>
        /// <returns>Returns Task of index</returns>
        Task<IEnumerable<Index>> GetAllIndexesAsync();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns Task</returns>
        Task DeleteAsync(string name);
    }
}
