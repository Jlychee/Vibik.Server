using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Moderation.GetNextForModeration;

public record GetNextForModerationQuery : IRequest<ModerationTask?>;