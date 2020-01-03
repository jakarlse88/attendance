using System;
using System.Reflection;
using AttendanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        public DbSet<Grade> BeltGrade { get; set; }
        public DbSet<ClassSession> ClassSession { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Grade>()
                .HasData(
                    new Grade
                    {
                        Id = 1,
                        Description = "10. geup"
                    },
                    new Grade
                    {
                        Id = 2,
                        Description = "8.1 geup"
                    },
                    new Grade
                    {
                        Id = 3,
                        Description = "8. geup"
                    },
                    new Grade
                    {
                        Id = 4,
                        Description = "7.1 geup"
                    },
                    new Grade
                    {
                        Id = 5,
                        Description = "7. geup"
                    },
                    new Grade
                    {
                        Id = 6,
                        Description = "6.1. geup"
                    },
                    new Grade
                    {
                        Id = 7,
                        Description = "6.1 geup"
                    },
                    new Grade
                    {
                        Id = 8,
                        Description = "6. geup"
                    },
                    new Grade
                    {
                        Id = 9,
                        Description = "5.1 geup"
                    },
                    new Grade
                    {
                        Id = 10,
                        Description = "5. geup"
                    },
                    new Grade
                    {
                        Id = 11,
                        Description = "4.1 geup"
                    },
                    new Grade
                    {
                        Id = 12,
                        Description = "4. geup"
                    },
                    new Grade
                    {
                        Id = 13,
                        Description = "3. geup"
                    },
                    new Grade
                    {
                        Id = 14,
                        Description = "2. geup"
                    },
                    new Grade
                    {
                        Id = 15,
                        Description = "1. geup"
                    },
                    new Grade
                    {
                        Id = 16,
                        Description = "1. dan"
                    },
                    new Grade
                    {
                        Id = 17,
                        Description = "2. dan"
                    },
                    new Grade
                    {
                        Id = 18,
                        Description = "3. dan"
                    },
                    new Grade
                    {
                        Id = 19,
                        Description = "4. dan"
                    },
                    new Grade
                    {
                        Id = 20,
                        Description = "5. dan"
                    },
                    new Grade
                    {
                        Id = 21,
                        Description = "6. dan"
                    },
                    new Grade
                    {
                        Id = 22,
                        Description = "7. dan"
                    },
                    new Grade
                    {
                        Id = 23,
                        Description = "8. dan"
                    },
                    new Grade
                    {
                        Id = 24,
                        Description = "9. dan"
                    }
                );

            builder.Entity<Instructor>()
                .HasData(
                    new Instructor
                    {
                        Id = 1,
                        FirstName = "Jon",
                        MiddleName = "Arild",
                        LastName = "Karlsen",
                        GradeId = 15,
                    }
                );

            builder.Entity<StudentClass>()
                .HasData(
                    new StudentClass
                    {
                        Id = 1,
                        Description = "Barn Nye",
                        InstructorId = 1
                    },
                    new StudentClass
                    {
                        Id = 2,
                        Description = "Barn Øvede",
                        InstructorId = 1
                    },
                    new StudentClass
                    {
                        Id = 3,
                        Description = "Ungdom",
                        InstructorId = 1
                    },
                    new StudentClass
                    {
                        Id = 4,
                        Description = "Voksen",
                        InstructorId = 1
                    }
                );
        }
    }
}