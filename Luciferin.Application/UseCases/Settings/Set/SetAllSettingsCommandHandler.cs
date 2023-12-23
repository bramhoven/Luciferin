namespace Luciferin.Application.UseCases.Settings.Set;

using Abstractions.Repositories;
using MediatR;

public class SetAllSettingsCommandHandler : IRequestHandler<SetAllSettingsCommand>
{
    private readonly ISettingRepository _settingRepository;

    public SetAllSettingsCommandHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }

    public async Task Handle(SetAllSettingsCommand request, CancellationToken cancellationToken)
    {
        foreach (var setting in request.Settings)
        {
            await _settingRepository.UpdateAsync(setting);
        }
    }
}