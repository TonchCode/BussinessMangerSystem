using Classes.Entities;
using Classes.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Classes.Context
{
    public class BMSDBContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<WorkJobs> WorkJobs { get; set; }

        public BMSDBContext()
        {
            this.People = this.Set<Person>();
            this.Jobs = this.Set<Job>();
            this.Workplaces = this.Set<Workplace>();
            this.WorkJobs = this.Set<WorkJobs>();
        }
        public BMSDBContext(DbContextOptions options) : base(options)
        {
            this.People = this.Set<Person>();
            this.Jobs = this.Set<Job>();
            this.Workplaces = this.Set<Workplace>();
            this.WorkJobs = this.Set<WorkJobs>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Code\Web\C#\BussinessMangerSystem\DataBase\DataBase.mdf;Integrated Security=True;Connect Timeout=30", b => b.MigrationsAssembly("BMAPI"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Job>().ToTable("Jobs");
            modelBuilder.Entity<Workplace>().ToTable("Workplaces");
            modelBuilder.Entity<WorkJobs>().ToTable("WorkToJobs");


            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 1,
                Title = "Programmer",
                MinSalary = 1050.50,
                MinHours = 4,
                IsRemoteAvailable = true,
                Description = "You write code...",
                JobCreationDate = DateTime.Now
            }); 
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 2,
                Title = "Janitor",
                MinSalary = 360.49,
                MinHours = 8,
                IsRemoteAvailable = false,
                Description = "Cleaning offices and halls",
                JobCreationDate = DateTime.Now.AddDays(1)
            });
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 3,
                Title = "Secretary",
                MinSalary = 639.69,
                MinHours = 6,
                IsRemoteAvailable = true,
                Description = "You do office work for the boss",
                JobCreationDate = DateTime.Now.AddDays(4)
            });
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 4,
                Title = "Guard",
                MinSalary = 1255.55,
                MinHours = 8,
                IsRemoteAvailable = false,
                Description = "You guard all the computers in the office",
                JobCreationDate = DateTime.Now.AddDays(-5)
            });
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 5,
                Title = "Boss",
                MinSalary = 4200.69,
                MinHours = 4,
                IsRemoteAvailable = true,
                Description = "You are the boss",
                JobCreationDate = DateTime.Now.AddDays(-200)
            });
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 6,
                Title = "UI Designer",
                MinSalary = 1050.50,
                MinHours = 4,
                IsRemoteAvailable = true,
                Description = "You write code... But in the front end",
                JobCreationDate = DateTime.Now
            });
            modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 7,
                Title = "Database Maintainer",
                MinSalary = 1050.50,
                MinHours = 8,
                IsRemoteAvailable = true,
                Description = "You maintain the database",
                JobCreationDate = DateTime.Now.AddDays(-15)
            });
            modelBuilder.Entity<Workplace>().HasData(
            new Workplace
            {
                Id = 1,
                Name = "Sibiz",
                City = "Plovdiv",
                RatingWithStars = 4.6,
                DateCreated = DateTime.Now,
                Holder = "Rhody Dervishev"
            });
            modelBuilder.Entity<Workplace>().HasData(
            new Workplace
            {
                Id = 2,
                Name = "Prime Holding",
                City = "Plovdiv",
                RatingWithStars = 4.1,
                DateCreated = DateTime.Now,
                Holder = "Ivan Draganov"
            });
            modelBuilder.Entity<Workplace>().HasData(
            new Workplace
            {
                Id = 3,
                Name = "Broadcom",
                City = "Plovdiv",
                RatingWithStars = 3.8,
                DateCreated = DateTime.Now,
                Holder = "Petur Arnaudov"
            });

            Random rand = new Random();
            List<string> firstNames = new List<string> { "Toncho", "Cvetelina", "Ivan", "Maria", "Georgi", "Stefan", "Elena", "Dimitar", "Viktoria", "Nikolay" };
            List<string> lastNames = new List<string> { "Nikolov", "Stoilova", "Ivanov", "Petrova", "Georgiev", "Stefanov", "Elenova", "Dimitrov", "Viktorova", "Nikolaev" };
            List<string> emails = new List<string> { "toncho@gmail.com", "cvety@gmail.com", "ivan@gmail.com", "maria@gmail.com", "georgi@gmail.com", "stefan@gmail.com", "elena@gmail.com", "dimitar@gmail.com", "viktoria@gmail.com", "nikolay@gmail.com" };

            for (int i = 1; i <= 10; i++)
            {
                int randomIndex = rand.Next(firstNames.Count);
                string randomFirstName = firstNames[randomIndex];
                string randomLastName = lastNames[randomIndex];
                string randomEmail = emails[randomIndex];
                DateTime randomBirthDate = new DateTime(rand.Next(1980, 2003), rand.Next(1, 13), rand.Next(1, 29));

                modelBuilder.Entity<Person>().HasData(
                    new Person
                    {
                        Id = i,
                        FName = randomFirstName,
                        LName = randomLastName,
                        Sex = i % 2 == 0 ? "Male" : "Female",
                        Age = DateTime.Now.Year - randomBirthDate.Year,
                        BirthDate = randomBirthDate,
                        Email = randomEmail,
                        Salary = 1200,
                        JobId = rand.Next(1, 8), // Random job id between 1 and 7
                        WorkId = rand.Next(1, 4) // Random work id between 1 and 3
                    });
            }

            // Assign all jobs to each workplace
            for (int jobId = 1; jobId <= 7; jobId++)
            {
                for (int workId = 1; workId <= 3; workId++)
                {
                    modelBuilder.Entity<WorkJobs>().HasData(new WorkJobs
                    {
                        Id = (jobId - 1) * 3 + workId,
                        JobId = jobId,
                        WorkId = workId
                    });
                }
            }
        }
    }
}
