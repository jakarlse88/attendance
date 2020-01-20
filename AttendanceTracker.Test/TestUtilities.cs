using System;
using AttendanceTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTracker.Test
{
    public static class TestUtilities
    {
        internal static DbContextOptions<ApplicationDbContext> BuildTestDbOptions() 
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}