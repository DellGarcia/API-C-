using System;
using System.ComponentModel.DataAnnotations;

namespace Api_CSharp.Models {
  public class User {
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string SurName { get; set; }

    [Required]
    public int? Age { get; set; }

    public DateTime? CreationDate { get; set; }
  }
}