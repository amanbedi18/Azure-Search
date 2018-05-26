namespace AzureSearchServices.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AzureSearchServices.Contracts;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;

    /// <summary>
    /// The Document Service
    /// </summary>
    /// <seealso cref="AzureSearchServices.Services.AzureSearchService" />
    /// <seealso cref="AzureSearchServices.Contracts.IDocumentService" />
    public class DocumentService : AzureSearchService, IDocumentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="azureSearchConfiguration">The azure search configuration.</param>
        public DocumentService(IAzureSearchConfiguration azureSearchConfiguration)
            : base(azureSearchConfiguration)
        {
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        /// <exception cref="Exception">Failed to create some documents</exception>
        public async Task<bool> CreateAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    var btch = IndexBatch.Upload<T>(documents);
                    await client.Documents.IndexAsync(btch).ConfigureAwait(false);
                    return true;
                }
            }
            catch (IndexBatchException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        /// <exception cref="Exception">Failed to delete some documents</exception>
        public async Task<bool> DeleteAsync<T>(string indexName, Dictionary<string, string> documents)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    var deleteIndexActions = new List<IndexAction>();
                    documents.ToList().ForEach(d => deleteIndexActions.Add(IndexAction.Delete(d.Value, d.Key)));
                    var btch = IndexBatch.New(deleteIndexActions);
                    await client.Documents.IndexAsync(btch).ConfigureAwait(false);
                    return true;
                }
            }
            catch (IndexBatchException ex)
            {
                return false;
            }
        }

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
        /// <exception cref="Exception">Failed to search some documents</exception>
        public async Task<IEnumerable<T>> SearchDocuments<T>(string indexName, string searchParameters, string filter = null, string scoringProfile = null)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    var sp = new SearchParameters
                    {
                        QueryType = QueryType.Full,
                        SearchMode = SearchMode.All,
                    };

                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        sp.Filter = filter;
                    }

                    if (!string.IsNullOrWhiteSpace(scoringProfile))
                    {
                        sp.ScoringProfile = scoringProfile;
                    }

                    var response = await client.Documents.SearchAsync<T>(searchParameters, sp).ConfigureAwait(false);

                    if (response.Results != null)
                    {
                        return response.Results.Select(p => p.Document).ToList();
                    }
                    else
                    {
                        return default(List<T>);
                    }
                }
            }
            catch (IndexBatchException ex)
            {
                return default(List<T>);
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        /// <exception cref="Exception">Failed to update some documents</exception>
        public async Task<bool> UpdateAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    var btch = IndexBatch.Merge<T>(documents);
                    await client.Documents.IndexAsync(btch).ConfigureAwait(false);
                    return true;
                }
            }
            catch (IndexBatchException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update or insert to search.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="documents">The documents.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        /// <exception cref="Exception">Failed to Update or insert to search some documents</exception>
        public async Task<bool> UpsertAsync<T>(string indexName, IEnumerable<T> documents)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    var btch = IndexBatch.MergeOrUpload<T>(documents);
                    await client.Documents.IndexAsync(btch).ConfigureAwait(false);
                    return true;
                }
            }
            catch (IndexBatchException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the by key asynchronous.
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="key">The key.</param>
        /// <param name="selectedFields">The selected fields.</param>
        /// <returns>
        /// Returns Task of bool
        /// </returns>
        /// <exception cref="Exception">Failed to retrieve document</exception>
        public async Task<T> GetByKeyAsync<T>(string indexName, string key, IEnumerable<string> selectedFields = null)
            where T : class
        {
            try
            {
                using (var serviceClient = this.GetAdminSearchServiceClient())
                {
                    var client = serviceClient.Indexes.GetClient(indexName);
                    if (selectedFields != null && selectedFields.ToList().Count != 0)
                    {
                        return (await client.Documents.GetAsync<T>(key, selectedFields).ConfigureAwait(false)) as T;
                    }

                    return (await client.Documents.GetAsync<T>(key).ConfigureAwait(false)) as T;
                }
            }
            catch (IndexBatchException ex)
            {
                return null;
            }
        }
    }
}
