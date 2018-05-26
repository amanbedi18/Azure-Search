namespace AzureSearchServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The document service contract
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Searches the documents.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="searchParameters">The search parameters.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="scoringProfile">The scoring profile.</param>
        /// <returns>
        /// Returns Task of list of T
        /// </returns>
        Task<IEnumerable<T>> SearchDocuments<T>(string indexName, string searchParameters, string filter = null, string scoringProfile = null)
            where T : class;

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        Task<bool> CreateAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class;

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        Task<bool> DeleteAsync<T>(string indexName, Dictionary<string, string> documents)
            where T : class;

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        Task<bool> UpdateAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class;

        /// <summary>
        /// Update or insert to search.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        Task<bool> UpsertAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class;

        /// <summary>
        /// Gets the by key asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="key">The key.</param>
        /// <param name="selectedFields">The selected fields.</param>
        /// <returns>
        /// Returns Task of T
        /// </returns>
        Task<T> GetByKeyAsync<T>(string indexName, string key, IEnumerable<string> selectedFields = null)
            where T : class;
    }
}
