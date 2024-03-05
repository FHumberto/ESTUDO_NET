using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private HrDatabaseContext _hrDatabaseContext;
    private readonly string _userId;
    private readonly Mock<IUserService> _userServiceMock;

    public HrDatabaseContextTests()
    {
        //? prepara a infraestrutura do banco
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _userId = "00000000-0000-0000-0000-000000000000";
        _userServiceMock = new Mock<IUserService>();
        _userServiceMock.Setup(m => m.UserId).Returns(_userId);

        _hrDatabaseContext = new HrDatabaseContext(dbOptions, _userServiceMock.Object);
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