using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using AttendanceTracker.Repositories;
using Xunit;

namespace AttendanceTracker.Test
{
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    public class RepositoryWrapperTests
    {
        [Fact]
        public void TestClassSession()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            IClassSessionRepository repo;
            
            using (var context = new ApplicationDbContext(options))
            {
                var repoWrapper = new RepositoryWrapper(context);

                // Act
                repo = repoWrapper.ClassSession;
            }

            // Assert
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IClassSessionRepository>(repo);
        }

        [Fact]
        public void TestStudentClass()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            IStudentClassRepository repo;
            
            using (var context = new ApplicationDbContext(options))
            {
                var repoWrapper = new RepositoryWrapper(context);

                // Act
                repo = repoWrapper.StudentClass;
            }

            // Assert
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IStudentClassRepository>(repo);
        }

        [Fact]
        public void TestStudent()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            IStudentRepository repo;
            
            using (var context = new ApplicationDbContext(options))
            {
                var repoWrapper = new RepositoryWrapper(context);

                // Act
                repo = repoWrapper.Student;
            }

            // Assert
            Assert.NotNull(repo);
            Assert.IsAssignableFrom<IStudentRepository>(repo);
        }

        [Fact]
        public async Task TestSaveAsync()
        {
            // Arrange
            var options = TestUtilities.BuildTestDbOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                Assert.Empty(context.ClassSession);

                context.ClassSession.Add(new ClassSession());
                
                var repoWrapper = new RepositoryWrapper(context);
                
                // Act
                await repoWrapper.SaveAsync();
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.ClassSession);

                context.Database.EnsureDeleted();
            }
        }

    }
}