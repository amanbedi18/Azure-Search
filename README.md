# Azure Search

## Introduction
**_AzureSearchServices_** is a library which provides common index and document management operations in an Azure Search service. It provides seamless & managed way to perform CRUD operations on Azure Search index & documents within an existing index. The library also implements best practices to work with Azure Search queries (both lucene syntax and full syntax) along-with options to use existing scoring profiles in the index and filter the results via Search Filters.

## Architecture
Azure Search solution has two components.
* **_AzureSearchServices_** : Common Library for managing documents and index in an Azure Search service.
* **_AzureSearch_** : Test Project demonstrating the capabilities of the of the **_AzureSearchServices_** Common Library.

## Common Library
**_AzureSearchServices_** communicates with an Existing Azure Search service. It provides functionality to:
* Intialize an Azure Search Service client
* Check if an index exist in an Azure Search service
* Create a new index in an Azure Search service
* Get all indexes in an Azure Search service
* Delete index from Azure Search service
* Create/Update/Upsert/Delete documents in an Azure Search service
* Search for documents within an index with Search Query & optionally : scoring profiles & search filters
* Get a document by Id within an index

The components of library are as shown below:

### AzureSearchService
The recommendation is to use one **_CloudTableClient_** per App Domain with appropriate settings of preferred locations and consistency Level. To support this, library provides **_CosmosDbContext_**. It is recommended to register the context as singleton in appropriate dependency containers. Below are the methods in Cosmos DB Context.

| Method                                                                                       | Description   |
| -------------------------------------------------------------------------------------------- |---------------|
| SearchServiceClient GetAdminSearchServiceClient()                                            | Gets a new instance of **_SearchServiceClient_** initialized with admin key (usually used for CRUD operations on index and documents in index). |
| SearchServiceClient GetQuerySearchServiceClient()                                            | Gets a new instance of **_SearchServiceClient_** initialized with query key (usually used for search operations on index).|
| HttpClient GetHttpClientForService()                                                         | Gets a new instance of **_HttpClient_** which can be used in custom REST API request against Azure Search service.|

### IndexService
Specifies the methods to interact with Azure Search service index. Following are the methods exposed.

| Method                                                                                         | Description   |
| --------------------------------------------------------------------------------------------   |---------------|
| Task CreateOrUpdateAsync(Index definition)                                                     | Creates a new Azure Search index in the configured Azure Search service. |
| Task<Index> GetIndexAsync(string name)                                                         | Gets an existing index in the configured Azure Search service. |
| Task<bool> ExistsIndexAsync(string name)                                                       | Checks if the provided index already exists in the configured Azure Search service. |
| Task<IEnumerable<Index>> GetAllIndexesAsync()                                                  | Gets all indexes in the configured Azure Search service. |
| Task DeleteAsync(string name)                                                                  | Deletes an existing index from the configured Azure Search service. |

### DocumentService
Specifies the methods to handle documents within an Azure Search service index. Following are the methods exposed.

| Method                                                                                         | Description   |
| --------------------------------------------------------------------------------------------   |---------------|
| Task<bool> CreateAsync<T>(string indexName, IEnumerable<T> documents)                          | Creates a document in the provided index in the configured Azure Search service. |
| Task<bool> DeleteAsync<T>(string indexName, Dictionary<string, string> documents)              | Deletes an existing document in the provided index in the configured Azure Search service. |
| Task<bool> UpdateAsync<T>(string indexName, IEnumerable<T> documents)                          | Updates an existing document in the provided index in the configured Azure Search service. |
| Task<bool> UpsertAsync<T>(string indexName, IEnumerable<T> documents)                          | Upserts a document in the provided index in the configured Azure Search service. |
| Task<T> GetByKeyAsync<T>(string indexName, string key, IEnumerable<string> selectedFields = null) | Gets an existing document / certain fields of the document if selected Fields contains list of desired fields to be fetched in the provided index in the configured Azure Search service. |
| Task<IEnumerable<T>> SearchDocuments<T>(string indexName, string searchParameters, string filter = null, string scoringProfile = null)| Searches for documents in the provided index in the configured Azure Search service via the provided Search Query and optionally : Scoring Profile & Search filter. |

**_The DocumentService & IndexService inherit from AzureSearchService and chains their constructors to the base class (AzureSearchService) to pass on the incoming IAzureSearchConfiguration object._**

## IAzureSearchConfiguration
This interface is defined to get the values for the following properties to initialize **_AzureSearchClient_** & also to enable the consumer to obtain and initialize these configurations from any configuration provider.
* **_AdminKey_**: The admin key for Azure Search Service
* **_QueryKey_**: Level: The query key for Azure Search Service
* **_ServiceName_**: Level: The name of the Azure Search Service

### DocumentEntity
Specifies the document schema to initialize the Azure Search index with & further use the same contract to communicate with Azure Search service. Following are the properties for the sample document entity class.

| Method                                                                                         | Description   |
| --------------------------------------------------------------------------------------------   |---------------|
| string Id                                                                                      | The mandatory primary key of the document. Marked as **_IsFilterable_** to enable it to be used in filters. |
| string Type                                                                                    | The type of the document. Marked as **_IsSearchable_** **_IsFilterable_** **_IsSortable_** **_IsFacetable_** & **_IsRetrievable_** to enable it to be used for searching, filtering, sorting, faceting & retrieving the same. |
| DateTimeOffset? PublishedDate                                                                  | The published date of the document. Marked as **_IsFilterable_** **_IsSortable_** **_IsFacetable_** & **_IsRetrievable_** to enable it to be used for filtering, sorting, faceting & retrieving the same. |
| string Title                                                                                   | Title of the document. Marked as **_IsSearchable_** **_IsFilterable_** **_IsSortable_** **_IsFacetable_** & **_IsRetrievable_** to enable it to be used for searching, filtering, sorting, faceting & retrieving the same.|
| string[] AdditionalProperties                                                                  | Additional custom properties for a given document. Marked as **_IsSearchable_** **_IsFilterable_** **_IsFacetable_** & **_IsRetrievable_** to enable it to be used for searching, filtering, faceting & retrieving the same. |

## Test Project
The Project **_AzureSearch_** is a console application that has reference implementation of using the **_AzureSearchServices_** library. The project uses the **_DocumentEntity_** defined in **_AzureSearchServices_** project as reference document contract for defining index schema and interacting with Azure Search Service. 

It also provides with an approach to define **_AzureSearchConfiguration_** (which implements **_IAzureSearchConfiguration_** interface) to initialize the requisite properties of **_IAzureSearchConfiguration_** interface & initialize the **_DocumentService_** / **_IndexService_** with the provided configuration to **_IAzureSearchConfiguration_**. This ensures that valid **_SearchServiceClient_** is initialized from the provided **_IAzureSearchConfiguration_** which is loosely bound to **_AzureSearchService_**.

**_Ensure to replace "{Search Service Admin Key}", "{Search Service Query Key}" & "{Search Service Name}" with actual values of Azure Search service admin key, query key & service name respectively._**

The **_AzureSearch_** console application demonstrates the following:
* Creates **_AzureSearchConfiguration_** using provided configuration.
* Initializes the **_DocumentService_** & **_IndexService_** with the configured **_AzureSearchConfiguration_**.
* Checks if an index with the name **_"documents"_** already exist in the configured Azure Search Service.
* Creates a new index with the name **_"documents"_** using the **_DocumentEntity_** in the configured Azure Search Service.
* Gets index definition for the index with the name **_"documents"_** from the configured Azure Search Service & displays the details.
* Populates the index with the name **_"documents"_** with a batch of **_DocumentEntity_** documents in the configured Azure Search Service.
* Deletes documents from the index with the matching Id values and key name from the configured Azure Search Service.
* Updates documents in the index with the matching Id value with updated properties in **_DocumentEntity_** instance from the configured Azure Search Service.
* Upserts documents in the index with provided batch of **_DocumentEntity_** documents in the configured Azure Search Service.
* Gets document by given key (Id) in the index, selecting only the **_Title_** & **_Type_** properties from the fetched **_DocumentEntity_** document in the configured Azure Search Service.
* Searches for documents in the index by given search query & displays the fetched documents from the configured Azure Search Service.
* Deletes the index with the name **_"documents"_** from the configured Azure Search Service.
