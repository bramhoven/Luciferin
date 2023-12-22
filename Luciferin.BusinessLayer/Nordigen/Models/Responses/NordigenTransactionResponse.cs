namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class NordigenTransactionResponse
{
    #region Properties

    [JsonPropertyName("transactions")]
    public NordigenTransactionResponseTransactions Transactions { get; set; }

    #endregion
}

public class NordigenTransactionResponseTransactions
{
    #region Properties

    [JsonPropertyName("booked")]
    public ICollection<NordigenTransaction> Booked { get; set; }

    [JsonPropertyName("pending")]
    public ICollection<NordigenTransaction> Pending { get; set; }

    #endregion
}