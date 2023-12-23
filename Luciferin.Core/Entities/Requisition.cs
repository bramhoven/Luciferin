namespace Luciferin.Core.Entities;

using Abstractions;
using Enums;

public class Requisition : IEntity
{
    public ICollection<Account> Accounts { get; set; }

    public bool AccountSelection { get; set; }

    public string Agreement { get; set; }

    public DateTime Created { get; set; }

    public string InstitutionId { get; set; }

    public string Link { get; set; }

    public string Redirect { get; set; }

    public string Reference { get; set; }

    public string Ssn { get; set; }

    public ImportAccountStatus Status { get; set; }

    public string UserLanguage { get; set; }

    public bool IsSuspended { get; set; }

    public bool IsRevoked { get; set; }

    public string Id { get; set; }
}