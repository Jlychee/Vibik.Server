using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Tasks.GetTask;

public record GetTaskQuery(string Username, int TaskId) : IRequest<TaskModel>;