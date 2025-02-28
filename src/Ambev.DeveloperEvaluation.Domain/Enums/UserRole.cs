using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public enum EnumUserRole
{
    [Description("None")]
    None = 0,
    [Description("Customer")]
    Customer,
    [Description("Manager")]
    Manager,
    [Description("Admin")]
    Admin,
}