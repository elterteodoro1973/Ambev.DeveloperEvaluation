using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<GetUserResponse, GetUserResult>();
    }
}