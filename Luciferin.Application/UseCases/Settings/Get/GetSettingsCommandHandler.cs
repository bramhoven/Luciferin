namespace Luciferin.Application.UseCases.Settings.Get;

using Abstractions.Repositories;
using Core.Abstractions;
using MediatR;

public class GetSettingsCommandHandler : IRequestHandler<GetSettingsCommand, ICollection<ISetting>>
{
    private readonly ISettingRepository _settingRepository;

    public GetSettingsCommandHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }

    public async Task<ICollection<ISetting>> Handle(GetSettingsCommand request, CancellationToken cancellationToken)
    {
        return await _settingRepository.GetAllAsync();
    }
}