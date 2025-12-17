using Client.Models.Models.DTO.Response;
using MediatR;

namespace Api.Application.Features.Auth.Register;

public record RegisterUserCommand(
    string Username,
    string? DisplayName,
    string Password
) : IRequest<RegisterUserResponse>;