using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Tasks.GetCompletedTasks;

public record GetCompletedTasksQuery(string Username) : IRequest<List<TaskModel>>;