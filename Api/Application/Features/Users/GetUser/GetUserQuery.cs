using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Users.GetUser;

public record GetUserQuery(string Username) : IRequest<User>;