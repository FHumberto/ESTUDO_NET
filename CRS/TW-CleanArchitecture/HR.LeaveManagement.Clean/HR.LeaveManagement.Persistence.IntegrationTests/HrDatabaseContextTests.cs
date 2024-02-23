using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        //? prepara a infraestrutura do banco
        DbContextOptions<HrDatabaseContext> dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        //? dados in�ciais (arrange)
        LeaveType leaveType = new LeaveType { Id = 1, DefaultDays = 10, Name = "Test Vacation" };

        //? a��o (act)
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        //! condi��o de sucesso (assert)
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        //? dados in�ciais (arrange)
        LeaveType leaveType = new LeaveType { Id = 1, DefaultDays = 10, Name = "Test Vacation" };

        //? a��o (act)
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        //! condi��o de sucesso (assert)
        leaveType.DateModified.ShouldNotBeNull();
    }
}