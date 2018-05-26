namespace AzureSearchServices.Entities
{
    using System;
    using Microsoft.Azure.Search;
    using Newtonsoft.Json;

    /// <summary>
    /// The Card Document
    /// </summary>
    public class DocumentEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        /// <value>
        /// The type of the card.
        /// </value>
        [IsSearchable]
        [IsFilterable]
        [IsSortable]
        [IsFacetable]
        [IsRetrievable(true)]

        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        /// <value>
        /// The published date.
        /// </value>
        [IsFilterable]
        [IsSortable]
        [IsFacetable]
        [IsRetrievable(true)]

        public DateTimeOffset? PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [IsSearchable]
        [IsFilterable]
        [IsSortable]
        [IsFacetable]
        [IsRetrievable(true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Additional Properties.
        /// </summary>
        /// <value>
        /// The Additional Properties.
        /// </value>
        [IsSearchable]
        [IsFilterable]
        [IsFacetable]
        [IsRetrievable(true)]
        public string[] AdditionalProperties { get; set; }
    }
}
