namespace Luciferin.Core.Entities.Importable;

public class ImportableRequisition
{
    public Requisition Requisition { get; set; }
    public ICollection<ImportableAccount> ImportableAccounts { get; set; }
}