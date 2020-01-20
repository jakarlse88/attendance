using System.Threading.Tasks;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using AttendanceTracker.Repositories;
using Xunit;

namespace AttendanceTracker.Test
{
    public class StudentRepositoryTests
    {
        [Fact]
        public async Task TestAddNullEntity()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new StudentRepository(context);
                
                Assert.Empty(context.Student);
                
                // Act
                repository.Add(null);

                context.SaveChanges();
            }
            
            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Empty(context.Student);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestAddNonNullEntity()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();
            
            var testEntity = new Student();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new StudentRepository(context);
                
                Assert.Empty(context.Student);
                
                // Act
                repository.Add(testEntity);
                
                context.SaveChanges();
            }
            
            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.Student);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            const int testId = 0;

            Student result;
            
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
            
                var repository = new StudentRepository(context);
                
                // Act
                result = 
                    await repository
                        .GetByIdAsync(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestGetByIdAsyncIdValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            const int testId = 1;

            Student result;
            
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                context.Student.Add(
                    new Student
                    {
                        Id = 1,
                    });

                context.SaveChanges();
            
                var repository = new StudentRepository(context);
                
                // Act
                result = 
                    await repository
                        .GetByIdAsync(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
        }
    }
}