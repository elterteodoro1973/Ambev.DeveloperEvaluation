using MediatR;
using System.Xml.Linq;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Command for retrieving a Customer by their ID
/// </summary>
public record GetCustomerCommand : IRequest<GetCustomerResult>
{
    /// <summary>
    /// The unique identifier of the Customer to retrieve
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of GetCustomerCommand
    /// </summary>
    /// <param name="id">The ID of the Customer to retrieve</param>
    public GetCustomerCommand(string name)
    {
        Name = name;
    }
}
