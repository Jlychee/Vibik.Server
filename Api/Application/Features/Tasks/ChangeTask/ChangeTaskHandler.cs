using Client.Models.Models.Entities;
using Client.Models.Models.Enums;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Tasks.ChangeTask;

public class ChangeTaskHandler(IUsersTasksTable tasks, IUserTable users, IMetricsTable metrics, ITaskEvent taskEvent)
    : IRequestHandler<ChangeTaskQuery, TaskModel>
{
    private const double Coefficient = 0.5;

    public async Task<TaskModel> Handle(ChangeTaskQuery request, CancellationToken cancellationToken)
    {
        var username = request.Username;
        var taskId = request.TaskId;
        //TODO потом перепишу и это надо в service
        var newTask = await taskEvent.ChangeUserTask(taskId);
        var reward = tasks.GetReward(taskId);
        await users.AddMoney(username, -(int)(reward.Result * Coefficient));

        await metrics.AddRecord(username, MetricType.Change);
        return newTask!;
    }
}