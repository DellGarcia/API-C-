using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class AddressGenerator
    {
        public static async void Generate(ClientDBContext context)
        {
            Address address;
            for (int i = 1; i <= 100; i++)
            {
                address = new Address()
                {
                    Endereco = $"Rua {i}",
                    Complemento = $"complemento {i}",
                    Cidade = $"cidade {i}",
                    Estado = $"estado {i}"
                };
                await context.Address.AddAsync(address);
            }
            context.SaveChanges();
        }

    }
}
