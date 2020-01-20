using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;
using AttendanceTracker.Repositories;
using AttendanceTracker.Services;
using Moq;
using Xunit;

namespace AttendanceTracker.Test
{
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    public class ClassSessionServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task TestGetByIdAsyncIdBad(int testId)
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                // Act
                // Assert
                var ex = await Assert.ThrowsAsync<Exception>(
                    () => service.GetByIdAsync(testId));
                Assert.Equal("Bad Id", ex.Message);
            }
        }

        [Fact]
        public async Task TestGetByIdAsyncIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            const int testId = 1;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                // Act
                // Assert
                var ex = await Assert.ThrowsAsync<Exception>(
                    () => service.GetByIdAsync(testId));

                Assert.Equal(
                    $"ClassSession with Id {testId} could not be found.",
                    ex.Message);

                context.Database.EnsureDeleted();
            }
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

                context.ClassSession.Add(new ClassSession());

                context.SaveChanges();

                Assert.Single(context.ClassSession);

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                // Act
                result = await service.GetByIdAsync(testId);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.NotNull(result);
                Assert.IsAssignableFrom<ClassSession>(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestCreateAsyncDtoNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                // Act
                // Assert
                var ex = await Assert.ThrowsAsync<ArgumentException>(
                    () => service.CreateAsync(null));

                Assert.Equal("ClassSession DTO cannot be null.",
                    ex.Message);
            }
        }

        [Fact]
        public async Task TestCreateAsyncDtoDateMissing()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                var testDto = new ClassSessionDto();

                // Act
                // Assert
                var ex = await Assert.ThrowsAsync<ArgumentException>(
                    () => service.CreateAsync(testDto));

                Assert.Equal("ClassSession DTO cannot be null.",
                    ex.Message);
            }
        }

        [Fact]
        public void TestEditAsyncIdInvalid()
        {
            // Arrange
            var service =
                new ClassSessionService(
                    Mock.Of<IRepositoryWrapper>());

            // Act
            async void TestAction() =>
                await service.EditAsync(0, null);

            // Assert
            var ex = Assert.Throws<ArgumentException>(
                () => (Action) TestAction);

            Assert.Equal("No ClassSession with the supplied id was found.", ex.Message);
        }

        [Fact]
        public async Task TestEditAsyncDtoNull()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                ClassSessionDto testDto = null;

                // Act
                async void TestAction() =>
                    await service.EditAsync(1, testDto);

                // Assert
                var ex = Assert.Throws<ArgumentException>(
                    () => (Action) TestAction);

                Assert.Equal("DTO is null.", ex.Message);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestEditAsyncDtoStudentClassIdInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                var testDto = new ClassSessionDto
                {
                    StudentClassId = -10
                };

                // Act
                async void TestAction() =>
                    await service.EditAsync(1, testDto);

                // Assert
                var ex = Assert.Throws<ArgumentException>(
                    () => (Action) TestAction);

                Assert.Equal(
                    "No StudentClass with the supplied Id was found.",
                    ex.Message);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestEditAsyncDtoStudentIdsContainingInvalid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                var testDto = new ClassSessionDto
                {
                    StudentClassId = 1,
                    StudentIds = new HashSet<int> {-666}
                };

                // Act
                async void TestAction() =>
                    await service.EditAsync(1, testDto);

                // Assert
                var ex = Assert.Throws<ArgumentException>(
                    () => (Action) TestAction);

                Assert.Equal("No Student with the supplied Id was found.", ex.Message);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestEditAsyncIdValidDtoValid()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            ClassSession result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                context
                    .ClassSession
                    .Add(new ClassSession
                        {
                            Id = 1,
                            Date = new DateTime(2020, 7, 4),
                        }
                    );

                context.SaveChanges();

                Assert.Single(context.ClassSession);

                var repoWrapper = new RepositoryWrapper(context);

                var service = new ClassSessionService(repoWrapper);

                var testDto = new ClassSessionDto
                {
                    StudentClassId = 1,
                    Date = new DateTime(2020, 7, 7)
                };

                // Act
                result = await service.EditAsync(1, testDto);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.NotNull(result);
                Assert.Single(context.ClassSession);
                Assert.Equal(
                    new DateTime(2020, 7, 7),
                    context.ClassSession.First().Date);

                context.Database.EnsureDeleted();
            }
        }
    }
}