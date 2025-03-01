using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Profile for mapping between Customer entity and CreateCustomerResponse
/// </summary>
public class CreateCustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCustomer operation
    /// </summary>
    public CreateCustomerProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CreateCustomerResult>();
    }
}
