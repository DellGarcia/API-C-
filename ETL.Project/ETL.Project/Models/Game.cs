﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Models
{
    [Table("jogo")]
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
