namespace Luciferin.Application.UseCases.Settings.Set;

using Core.Abstractions;
using MediatR;

public record SetAllSettingsCommand(ICollection<ISetting> Settings) : IRequest;