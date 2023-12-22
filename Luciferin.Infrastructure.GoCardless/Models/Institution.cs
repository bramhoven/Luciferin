namespace Luciferin.Infrastructure.GoCardless.Models;

public class Institution
{
    public string Bic { get; set; }
    public ICollection<string> Countries { get; set; }
    public string Id { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public int TransactionTotalDays { get; set; }
}