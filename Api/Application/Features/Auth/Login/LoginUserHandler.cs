using Api.Application.Common.Exceptions;
using Client.Models.Models.DTO.Response;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Auth.Login;

public class LoginUserHandler(IUserTable users, ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        LoginUserValidation.Validate(command);

        var username = command.Username;

        var loginStatus = await users.LoginUser(username, command.Password);

        if (loginStatus)
            return new LoginUserResponse(tokenService.GenerateAccessToken(username),
                tokenService.GenerateRefreshToken(username));
        throw new ApiException(StatusCodes.Status401Unauthorized, "Неверный логин или пароль");
    }
}