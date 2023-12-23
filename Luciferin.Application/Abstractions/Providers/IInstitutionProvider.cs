namespace Luciferin.Application.Abstractions.Providers;

using Core.Entities;

public interface IInstitutionProvider : IGenericProvider<Institution>
{
    /// <summary>
    ///     Gets a list of all the institutions by country code.
    /// </summary>
    /// <param name="countryCode">The code of the country for which to retrieve the institutions.</param>
    /// <returns></returns>
    Task<ICollection<Institution>> GetInstitutionsByCountryCode(string countryCode);
}