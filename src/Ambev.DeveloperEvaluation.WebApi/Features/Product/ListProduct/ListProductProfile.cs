﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<Product, ListProductResponse>();
        CreateMap<ListProductResponse, Product>();
    }
}
