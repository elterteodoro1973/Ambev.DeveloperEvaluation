using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Profile for mapping between Application and API CreateCustomer responses
/// </summary>
public class CreateCustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCustomer feature
    /// </summary>
    public CreateCustomerProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<CreateCustomerResult, CreateCustomerResponse>();
    }
}
