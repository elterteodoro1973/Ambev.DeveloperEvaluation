﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


[PrimaryKey("SaleId", "CodeProduct")]
[Table("SaleItems", Schema = "DeveloperEvaluation")]
public partial class SaleItems
{
    [Key]
    public Guid SaleId { get; set; }

    [Key]
    public String CodeProduct { get; set; }

    public int Quantities { get; set; }

    [Precision(10, 2)]
    public decimal UnitPrices { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("SaleItems")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("SaleId")]
    [InverseProperty("SaleItems")]
    public virtual Sale Sale { get; set; } = null!;
}