using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Models
{
    [Table("biblioteca")]
    public class Library
    {
        public int Id { get; set; }

        public DateTime AquisitionDate { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }
    }
}
