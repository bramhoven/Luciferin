namespace Luciferin.Infrastructure.GoCardless.Converters.Helper;

using Abstractions;
using Enums;
using Microsoft.Extensions.Options;
using Settings;

public class ConverterHelper
{
    private readonly IOptionsSnapshot<LuciferinSettings> _options;

    public ConverterHelper(IOptionsSnapshot<LuciferinSettings> options)
    {
        _options = options;
    }

    /// <summary>
    ///     Gets a transaction converter based on bank type.
    /// </summary>
    /// <param name="institutionType">The bank type.</param>
    /// <returns></returns>
    public ITransactionConverter GetTransactionConverter(InstitutionType institutionType)
    {
        switch (institutionType)
        {
            case InstitutionType.ING:
                return new IngConverter(_options);
            case InstitutionType.SNS:
                return new SnsConverter(_options);
            case InstitutionType.N26:
                return new N26Converter(_options);
            default:
                return new DefaultConverter(_options);
        }
    }
}