using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class ListCustomerResponse
{
    /// <summary>
    /// The unique identifier of the Customer
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The Customer's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The Customer's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The Customer's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
