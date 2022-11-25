using System;

namespace Luciferin.BusinessLayer.Exceptions
{
    public class AccountFailureException : Exception
    {
        public AccountFailureException(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; }
    }
}