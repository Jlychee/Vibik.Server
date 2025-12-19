using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Tasks.ChangeTask;

public record ChangeTaskQuery(string Username, int TaskId) : IRequest<TaskModel>;