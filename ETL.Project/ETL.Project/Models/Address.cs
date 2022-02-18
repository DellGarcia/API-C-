using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Models
{
    [Table("endereco")]
    public class Address
    {
        public int Id { get; set; }

        public string Endereco { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public User User { get; set; }
    }
}
