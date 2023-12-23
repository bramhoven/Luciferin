namespace Luciferin.Application.UseCases.Settings.Get;

using Core.Abstractions;
using MediatR;

public record GetSettingsCommand : IRequest<ICollection<ISetting>>;