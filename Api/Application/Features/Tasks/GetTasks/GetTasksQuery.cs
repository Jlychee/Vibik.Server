using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Tasks.GetTasks;

public record GetTasksQuery(string Username) : IRequest<List<TaskModel>>;