﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("UserStatus", Schema = "DeveloperEvaluation")]
public partial class UserStatus : BaseEntity
{
    //[Key]
    //public int Id { get; set; }

    [StringLength(50)]
    public string Description { get; set; }

    [InverseProperty("StatusUser")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}