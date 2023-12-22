namespace Luciferin.Core.Exceptions;

public class AccountSuspendedException : Exception
{
    public AccountSuspendedException(string accountId)
    {
        AccountId = accountId;
    }

    public string AccountId { get; }
}