using Api.Application.Features.Photos.UploadPhoto;
using Client.Models.Models.Enums;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Tasks.SubmitTask;

public class SubmitTaskHandler(IUsersTasksTable tasks, IMetricsTable metrics, IMediator mediator, HttpClient httpClient, ILogger<SubmitTaskHandler> logger)
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
        
         var payload = new { moderator_ids = new[] { 1181814783,1338914722,875877003, 946887384} };
         var response = await httpClient.PostAsJsonAsync("/api/moderation/notify", payload, cancellationToken);
         response.EnsureSuccessStatusCode();
        
        return uploadedNames;
    }
}