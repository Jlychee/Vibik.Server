using Client.Models.Models.DTO.Response;
using MediatR;

namespace Api.Application.Features.Auth.Refresh;

public record RefreshCommand(
    string Username
) : IRequest<RefreshResponse>;