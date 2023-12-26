namespace Luciferin.Core.Entities.Importable;

public class ImportableAccount
{
    public Account Account { get; set; }

    public ICollection<Transaction> ImportableTransactions { get; set; }
}