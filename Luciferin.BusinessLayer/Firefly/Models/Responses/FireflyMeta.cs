namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Text.Json.Serialization;

public class FireflyMeta
{
    #region Properties

    [JsonPropertyName("pagination")]
    public FireflyPagination Pagination { get; set; }

    #endregion
}