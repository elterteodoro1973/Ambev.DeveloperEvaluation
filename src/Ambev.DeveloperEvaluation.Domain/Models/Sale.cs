﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Models;

[Table("Sale", Schema = "DeveloperEvaluation")]
public partial class Sale
{
    [Key]
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    [Precision(12, 2)]
    public decimal? TotalGrossValue { get; set; }

    [Precision(4, 2)]
    public decimal? Discounts { get; set; }

    [Precision(12, 2)]
    public decimal? TotalNetValue { get; set; }

    public bool? cancelled { get; set; }

    public DateTime SaleDate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Sale")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Sale")]
    public virtual ICollection<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
}