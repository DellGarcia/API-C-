using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class LibResponse
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public DateTime AquisitionDate { get; set; }
    }
}
