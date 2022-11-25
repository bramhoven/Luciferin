using System;

namespace Luciferin.BusinessLayer.Exceptions
{
    public class AccountSuspendedException : Exception
    {
        public AccountSuspendedException(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; }
    }
}