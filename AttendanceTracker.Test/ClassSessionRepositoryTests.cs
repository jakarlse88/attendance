using System;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using AttendanceTracker.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;

namespace AttendanceTracker.Test
{
    public class ClassSessionRepositoryTests
    {
        [Fact]
        public async Task TestAddNullEntity()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ClassSessionRepository(context);

                Assert.Empty(context.ClassSession);

                // Act
                repository.Add(null);

                context.SaveChanges();
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Empty(context.ClassSession);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestAddNonNullEntity()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            var testEntity = new ClassSession();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ClassSessionRepository(context);

                Assert.Empty(context.ClassSession);

                // Act
                repository.Add(testEntity);

                context.SaveChanges();
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.ClassSession);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            const int testId = 0;

            ClassSession result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ClassSessionRepository(context);

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

            ClassSession result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                context.ClassSession.Add(
                    new ClassSession
                    {
                        Id = 1,
                    });

                context.SaveChanges();

                var repository = new ClassSessionRepository(context);

                // Act
                result =
                    await repository
                        .GetByIdAsync(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetEntityEntryEntityValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            EntityEntry<ClassSession> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                context.ClassSession.Add(
                    new ClassSession
                    {
                        Id = 1
                    });

                context.SaveChanges();

                var repository = new ClassSessionRepository(context);

                var testEntity = 
                    context
                    .ClassSession
                    .FirstOrDefault(
                        cs => cs.Id == 1);
                
                // Act
                result =
                    repository
                        .GetEntityEntry(testEntity);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityEntry<ClassSession>>(result);
        }

        [Fact]
        public void TestGetEntityEntryEntityNull()
        {
            // Arrange
            var repository = 
                new ClassSessionRepository(
                    Mock.Of<ApplicationDbContext>());

            // Act
            void TestAction() => repository.GetEntityEntry(null);

            // Assert
            var ex = Assert.Throws<NullReferenceException>(TestAction);
            Assert.Equal("Cannot get EntityEntry of a null entity.", ex.Message);
        }
    }
}