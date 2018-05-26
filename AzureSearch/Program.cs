namespace AzureSearch
{
    using AzureSearch.Configuration;
    using AzureSearchServices.Entities;
    using AzureSearchServices.Services;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static AzureSearchConfiguration azureSearchConfiguration;
        static DocumentService documentService;
        static IndexService indexService;

        static void Main(string[] args)
        {
            azureSearchConfiguration = new AzureSearchConfiguration("{Search Service Admin Key}", "{Search Service Query Key}", "{Search Service Name}");
            documentService = new DocumentService(azureSearchConfiguration);
            indexService = new IndexService(azureSearchConfiguration);

            CheckIndex().GetAwaiter().GetResult();
            CreateIndex().GetAwaiter().GetResult();
            GetIndex().GetAwaiter().GetResult();
            PopulateIndex().GetAwaiter().GetResult();
            DeleteDocument().GetAwaiter().GetResult();
            UpdateDocument().GetAwaiter().GetResult();
            UpsertDocument().GetAwaiter().GetResult();
            GetIndexById().GetAwaiter().GetResult();
            SearchIndex().GetAwaiter().GetResult();
            DeleteIndex().GetAwaiter().GetResult();

            Console.ReadLine();
        }

        static async Task GetIndexById()
        {
            Console.WriteLine("Getting document by Id");

            var document = await documentService.GetByKeyAsync<DocumentEntity>("documents", "1", new string[] { "Title", "Type" });

            Console.WriteLine($"Found document with title : {document.Title} & type : {document.Type}");
        }

        static async Task SearchIndex()
        {
            Console.WriteLine("Searching for documents in the index with sample query");

            var documents = await documentService.SearchDocuments<DocumentEntity>("documents", "(Type:json)");

            Console.WriteLine($"Found {documents.ToList().Count} elemtns, iterating over them now");

            foreach (var document in documents)
            {
                Console.WriteLine($"Document : {document.Id}, Title : {document.Title}, Type : {document.Type}");
            }

            Console.WriteLine("Search Complete");
        }

        static async Task CheckIndex()
        {
            Console.WriteLine("Checking if Index exists");

            var index = await indexService.ExistsIndexAsync("documents");

            if (!index)
            {
                Console.WriteLine($"Index does not exist");
            }
        }

        static async Task GetIndex()
        {
            Console.WriteLine("Getting Index");

            var index = await indexService.GetIndexAsync("documents");

            Console.WriteLine($"Found index {index.Name} with {index.Fields.Count} fields");
        }

        static async Task UpdateDocument()
        {
            Console.WriteLine("Updating documents in Index");

            var res = await documentService.UpdateAsync<DocumentEntity>("documents", new List<DocumentEntity>()
            { new DocumentEntity()
                {
                     Id = "4",
                     PublishedDate = DateTime.Now,
                     Title = "Document 4",
                     Type = "json",
                     AdditionalProperties = new string[]{"Property 1", "Property 2" },
                }
            });

            Console.WriteLine("Updated documents in Index");
        }

        static async Task DeleteDocument()
        {
            Console.WriteLine("Deleting documents from Index");

            var res = await documentService.DeleteAsync<DocumentEntity>("documents", new Dictionary<string, string>()
            {
                {"5", "id" },
                {"6", "id" }
            });

            Console.WriteLine("Deleted documents from Index");
        }

        static async Task UpsertDocument()
        {
            Console.WriteLine("Upserting documents in Index");

            var res = await documentService.UpsertAsync<DocumentEntity>("documents", new List<DocumentEntity>()
            { new DocumentEntity()
                {
                     Id = "4",
                     PublishedDate = DateTime.Now,
                     Title = "Document 4",
                     Type = "xml",
                     AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                 new DocumentEntity()
                {
                     Id = "5",
                     PublishedDate = DateTime.Now,
                     Title = "Document 5",
                     Type = "json",
                     AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                  new DocumentEntity()
                {
                     Id = "6",
                     PublishedDate = DateTime.Now,
                     Title = "Document 6",
                     Type = "xml",
                     AdditionalProperties = new string[]{"Property 1", "Property 2" },
                }
            });

            Console.WriteLine("Upserted documents in Index");
        }

        static async Task PopulateIndex()
        {
            Console.WriteLine("Populating Index with Documents");

            var documentEntities = new List<DocumentEntity>()
            {
                new DocumentEntity()
                {
                    Id = "1",
                    PublishedDate = DateTime.Now,
                    Title = "Document 1",
                    Type = "json",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                new DocumentEntity()
                {
                    Id = "2",
                    PublishedDate = DateTime.Now,
                    Title = "Document 2",
                    Type = "json",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                new DocumentEntity()
                {
                    Id = "3",
                    PublishedDate = DateTime.Now,
                    Title = "Document 3",
                    Type = "xml",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                new DocumentEntity()
                {
                    Id = "4",
                    PublishedDate = DateTime.Now,
                    Title = "Document 4",
                    Type = "xml",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                new DocumentEntity()
                {
                    Id = "5",
                    PublishedDate = DateTime.Now,
                    Title = "Document 5",
                    Type = "json",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                },
                new DocumentEntity()
                {
                    Id = "6",
                    PublishedDate = DateTime.Now,
                    Title = "Document 6",
                    Type = "xml",
                    AdditionalProperties = new string[]{"Property 1", "Property 2" },
                }
            };

            await documentService.CreateAsync<DocumentEntity>("documents", documentEntities);

            Console.WriteLine("Populated Index with Documents");
        }

        static async Task CreateIndex()
        {
            Console.WriteLine("Creatring the index");

            var definition = new Index()
            {
                Name = "documents",
                Fields = FieldBuilder.BuildForType<DocumentEntity>()
            };

            await indexService.CreateOrUpdateAsync(definition);
            Console.WriteLine("Created Index");
        }

        static async Task DeleteIndex()
        {
            Console.WriteLine("Deleting the index");

            await indexService.DeleteAsync("documents");

            Console.WriteLine("Deleted Index");
        }
    }
}
