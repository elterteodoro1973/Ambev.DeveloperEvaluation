﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Models;

/// <summary>
/// Cliente
/// </summary>
[Table("Customer", Schema = "DeveloperEvaluation")]
public partial class Customer
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual ICollection<Sale> Sale { get; set; } = new List<Sale>();
}