using Api.Application.Common.Exceptions;
using Client.Models.Models.DTO.Response;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Auth.Register;

public class RegisterUserHandler(IUserTable users, IPasswordHasher hasher, ILogger<RegisterUserHandler> logger)
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        RegisterUserValidation.Validate(command);

        var username = command.Username;

        var hash = hasher.Hash(command.Password);

        var createdUser = await users.RegisterUser(username, hash)
                          ?? throw new ApiException(StatusCodes.Status500InternalServerError,
                              $"Ошибка создания пользователя: {username}");

        var displayName = string.IsNullOrWhiteSpace(command.DisplayName)
            ? null
            : command.DisplayName.Trim();

        if (displayName is not null && displayName != createdUser.DisplayName)
        {
            var oldUsername = createdUser.DisplayName;
            await users.ChangeDisplayName(createdUser.DisplayName, displayName);
            logger.LogInformation("Имя пользователя было измененно с {OldUsername} на {NewUsername}", oldUsername,
                displayName);
            createdUser.DisplayName = displayName;
        }

        return new RegisterUserResponse(true);
    }
}