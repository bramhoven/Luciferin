namespace Luciferin.Infrastructure.GoCardless.Converters.Helper;

using Enums;

public static class InstitutionHelper
{
    #region Methods

    #region Static Methods

    public static InstitutionType GetInstitutionType(string institutionId)
    {
        switch (institutionId)
        {
            case "ING_INGBNL2A":
                return InstitutionType.ING;
            case "SNS_BANK_SNSBNL2A":
                return InstitutionType.SNS;
            case "N26_NTSBDEB1":
                return InstitutionType.N26;
            default:
                return InstitutionType.UNKNOWN;
        }
    }

    #endregion

    #endregion
}