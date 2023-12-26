namespace Luciferin.Infrastructure.GoCardless.Providers;

using Abstractions;
using Application.Abstractions.Providers;
using AutoMapper;
using Core.Entities;
using Models;

public sealed class GoCardlessRequisitionProvider : IRequisitionProvider
{
    private readonly IGoCardlessService _goCardlessService;
    private readonly IMapper _mapper;

    public GoCardlessRequisitionProvider(IMapper mapper, IGoCardlessService goCardlessService)
    {
        _mapper = mapper;
        _goCardlessService = goCardlessService;
    }

    /// <inheritdoc />
    public async Task<Requisition> GetByIdAsync(string id)
    {
        var requisition = await _goCardlessService.GetRequisitionAsync(id);
        return _mapper.Map<Requisition>(requisition);
    }

    /// <inheritdoc />
    public async Task<List<Requisition>> GetAllAsync()
    {
        var requisitions = await _goCardlessService.GetRequisitionsAsync();
        return requisitions.Select(_mapper.Map<Requisition>).ToList();
    }

    /// <inheritdoc />
    public async Task<string> AddRequisitionAsync(string name, string institutionId, string returnUrl)
    {
        var institution = await _goCardlessService.GetInstitutionAsync(institutionId);
        var endUserAgreement = await _goCardlessService.CreateEndUserAgreementAsync(institution);
        var requisition = await _goCardlessService.CreateRequisitionAsync(institution, name, endUserAgreement, returnUrl);
        return requisition.Link;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteRequisitionAsync(string accountId)
    {
        return await _goCardlessService.DeleteRequisitionAsync(accountId);
    }

    /// <inheritdoc />
    public async Task<ICollection<Account>> GetAccountsForRequisition(Requisition requisition)
    {
        var gcRequisition = await _goCardlessService.GetRequisitionAsync(requisition.Id);
        var accounts = new List<GCAccount>();
        foreach (var accountId in gcRequisition.Accounts)
        {
            var account = await _goCardlessService.GetAccountAsync(accountId);
            accounts.Add(account);
        }
        return accounts.Select(_mapper.Map<Account>).ToList();
    }
}