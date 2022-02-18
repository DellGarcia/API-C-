using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ETL.Project.Analysis.Models
{
    [Table("user")]
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
