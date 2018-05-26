namespace AzureSearchServices.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using AzureSearchServices.Contracts;
    using Microsoft.Azure.Search;

    /// <summary>
    /// The Azure Search Service
    /// </summary>
    public abstract class AzureSearchService
    {
        /// <summary>
        /// The admin key
        /// </summary>
        private readonly string adminKey;

        /// <summary>
        /// The query key
        /// </summary>
        private readonly string queryKey;

        /// <summary>
        /// The service name
        /// </summary>
        private readonly string serviceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSearchService"/> class.
        /// </summary>
        /// <param name="azureSearchConfiguration">The azure search configuration.</param>
        protected AzureSearchService(IAzureSearchConfiguration azureSearchConfiguration)
        {
            this.serviceName = azureSearchConfiguration.ServiceName;
            this.adminKey = azureSearchConfiguration.AdminKey;
            this.queryKey = azureSearchConfiguration.QueryKey;
        }

        /// <summary>
        /// Gets the admin search service client.
        /// </summary>
        /// <returns>Returns search service client</returns>
        protected SearchServiceClient GetAdminSearchServiceClient()
        {
            // TODO: Implement client caching
            return new SearchServiceClient(this.serviceName, new SearchCredentials(this.adminKey));
        }

        /// <summary>
        /// Gets the query search service client.
        /// </summary>
        /// <returns>Returns search service client</returns>
        protected SearchServiceClient GetQuerySearchServiceClient()
        {
            return new SearchServiceClient(this.serviceName, new SearchCredentials(this.queryKey));
        }

        /// <summary>
        /// Gets the HTTP client for service.
        /// </summary>
        /// <returns>Returns http client</returns>
        protected HttpClient GetHttpClientForService()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", this.queryKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri($"https://{this.serviceName}.search.windows.net/");
            return client;
        }
    }
}
