using System;

namespace Api_CSharp.Models {
  public class User {
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string SurName { get; set; }

    public int? Age { get; set; }

    public DateTime CreationDate { get; set; }
  }
}