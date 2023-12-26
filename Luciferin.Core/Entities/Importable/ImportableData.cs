namespace Luciferin.Core.Entities.Importable;

public class ImportableData
{
    public DateTime ImportStart { get; set; }

    public DateTime TransactionStart { get; set; }
    public DateTime TransactionEnd { get; set; }

    public ICollection<ImportableRequisition> ImportableRequisitions { get; set; }
}