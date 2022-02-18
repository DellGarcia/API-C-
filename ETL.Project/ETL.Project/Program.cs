using ETL.Project.Database;
using Microsoft.EntityFrameworkCore;
using ETL.Project.Utils;
 

namespace ETL.Project
{
    internal class Program
    {
        public static void Main()
        {
            var context = new ClientDBContext();
            context.Database.Migrate();

            DataGenerator.Generate(context);
        }
    }
}
