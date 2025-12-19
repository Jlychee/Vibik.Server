using Client.Models.Models.Enums;
using MediatR;

namespace Api.Application.Features.Moderation.ApproveTask;

public record ChangeTaskStatusQuery(int UserTaskId, ModerationStatus Status) : IRequest<bool>;