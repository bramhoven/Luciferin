namespace Luciferin.Infrastructure.GoCardless.Models;

public class EndUserAgreement
{
    public DateTime? Accepted { get; set; }

    public List<string> AccessScope { get; set; }

    public int AccessValidForDays { get; set; }

    public DateTime Created { get; set; }

    public string Id { get; set; }

    public string InstitutionId { get; set; }

    public int MaxHistoricalDays { get; set; }
}