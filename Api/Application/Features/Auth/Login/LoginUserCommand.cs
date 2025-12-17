using Client.Models.Models.DTO.Response;
using MediatR;

namespace Api.Application.Features.Auth.Login;

public record LoginUserCommand(
    string Username,
    string Password
) : IRequest<LoginUserResponse>;