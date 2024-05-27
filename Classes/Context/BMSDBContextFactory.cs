using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Context
{
    public class BMSDBContextFactory : IDesignTimeDbContextFactory<BMSDBContext>
    {
        public BMSDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BMSDBContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Code\Web\C#\BussinessMangerSystem\DataBase\DataBase.mdf;Integrated Security=True;Connect Timeout=30");

            return new BMSDBContext(optionsBuilder.Options);
        }
    }
}
