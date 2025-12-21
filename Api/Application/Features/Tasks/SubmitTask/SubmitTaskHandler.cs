using Api.Application.Features.Photos.UploadPhoto;
using Client.Models.Models.Enums;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Tasks.SubmitTask;

public class SubmitTaskHandler(
    IUsersTasksTable tasks,
    IMetricsTable metrics,
    IMediator mediator,
    HttpClient httpClient,
    ILogger<SubmitTaskHandler> logger,
    IConfiguration configuration)
    : IRequestHandler<SubmitTaskQuery, List<string>>
{
    public async Task<List<string>> Handle(SubmitTaskQuery request, CancellationToken cancellationToken)
    {
        var uploadedNames = new List<string>();
        var username = request.Username;
        var taskId = request.TaskId;
        var files = request.Files;
        foreach (var file in files)
        {
            var name = await mediator.Send(new UploadPhotoCommand(file), cancellationToken);
            uploadedNames.Add(name);
        }

        await tasks.SetPhotos(taskId, uploadedNames.ToArray());

        await tasks.ChangeModerationStatus(taskId, ModerationStatus.Waiting);

        await metrics.AddRecord(username, MetricType.Submit);

        var moderationHost = configuration["MODERATION_HOST"]?.Trim()
                             ?? throw new InvalidOperationException("MODERATION_HOST не настроен в .env");

        var moderatorIdsEnv = configuration["MODERATOR_IDS"]?.Trim()
                              ?? throw new InvalidOperationException("MODERATOR_IDS не настроен в .env");

        var moderatorIds = moderatorIdsEnv
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(id => int.Parse(id.Trim()))
            .ToArray();

        logger.LogInformation("Отправляем уведомления модераторам");
        
        var payload = new { moderator_ids = moderatorIds };
        var response = await httpClient.PostAsJsonAsync(
            $"{moderationHost}/api/moderation/notify", payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        return uploadedNames;
    }
}