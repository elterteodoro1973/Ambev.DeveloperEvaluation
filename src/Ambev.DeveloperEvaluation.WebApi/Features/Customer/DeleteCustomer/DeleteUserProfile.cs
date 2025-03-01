using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;

/// <summary>
/// Profile for mapping DeleteCustomer feature requests to commands
/// </summary>
public class DeleteCustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCustomer feature
    /// </summary>
    public DeleteCustomerProfile()
    {
        CreateMap<Guid, Application.Customers.DeleteCustomer.DeleteCustomerCommand>()
            .ConstructUsing(id => new Application.Customers.DeleteCustomer.DeleteCustomerCommand(id));
    }
}
