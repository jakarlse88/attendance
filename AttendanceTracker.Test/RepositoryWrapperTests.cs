using System;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using AttendanceTracker.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AttendanceTracker.Test
{
    public class RepositoryWrapperTests
    {
        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions() 
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void TestRepositoryWrapperClassSessionAddNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repoWrapper = new RepositoryWrapper(context);
                
                Assert.Empty(context.ClassSession);
                
                // Act
                repoWrapper.ClassSession.Create(null);
                
                repoWrapper.Save();
            }
            
            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Empty(context.ClassSession);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestRepositoryWrapperClassSessionAddNonNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            
            var testEntity = new ClassSession();

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repoWrapper = new RepositoryWrapper(context);
                
                Assert.Empty(context.ClassSession);
                
                // Act
                repoWrapper.ClassSession.Create(testEntity);
                
                repoWrapper.Save();
            }
            
            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.ClassSession);

                context.Database.EnsureDeleted();
            }
        }
    }
}