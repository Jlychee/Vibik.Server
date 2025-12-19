using Api.Application.Common.Exceptions;
using Client.Models.Models.Entities;
using Infrastructure.Interfaces;
using MediatR;

namespace Api.Application.Features.Users.GetUser;

public class GetUserHandler(IUserTable users) : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await users.GetUser(request.Username);
        return user ?? throw new ApiException(StatusCodes.Status404NotFound, "Пользователь не найден");
    }
}