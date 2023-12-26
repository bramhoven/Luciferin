namespace Luciferin.Core.Entities;

using Abstractions;
using Enums;
using Helpers;
using Newtonsoft.Json;

public class Transaction : IEntity
{
    #region Properties

    public string Amount { get; set; }

    public DateTime? BookDate { get; set; }

    public string ExternalId { get; set; }

    public string CurrencyCode { get; set; }

    public int? CurrencyDecimalPlaces { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public string DestinationIban { get; set; }

    public string DestinationName { get; set; }

    public string Id { get; set; }

    public string InternalReference { get; set; }

    public string? Notes { get; set; }

    public string OriginalSource { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? ProcessDate { get; set; }

    public string RequisitionIban { get; set; }

    public string SourceIban { get; set; }

    public string SourceName { get; set; }

    public ICollection<string> Tags { get; set; }

    public TransactionType Type { get; set; }

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    ///     Gets the hash of this object to use in comparisons.
    /// </summary>
    /// <returns></returns>
    public string GetHashString()
    {
        return HashHelper.CalculateSha512(GetCompareString());
    }

    /// <summary>
    ///     Gets the hash code of this object to use in comparisons.
    /// </summary>
    /// <returns></returns>
    public ulong GetConsistentHash()
    {
        return (SourceIban, DestinationIban, Amount).GetConsistentHash();
    }

    /// <summary>
    ///     Gets the comparable string. This string contains the most important information in this transaction that can be
    ///     used in duplication checking.
    /// </summary>
    /// <returns></returns>
    public string GetCompareString()
    {
        return $"{SourceIban}-{DestinationIban}-{Amount}";
    }

    #endregion
}