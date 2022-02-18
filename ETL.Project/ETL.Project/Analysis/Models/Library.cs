using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL.Project.Analysis.Models
{
    [Table("library")]
    public class Library
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public DateTime AquisitionDate { get; set; }

        public User User { get; set; }
    }
}

