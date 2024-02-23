
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        List<LeaveType> leaveTypes = new ()
        {
            new() { Id = 1, DefaultDays = 10, Name = "Test Vacation" },
            new() { Id = 2, DefaultDays = 15, Name = "Test Sick" },
            new() { Id = 3, DefaultDays = 15, Name = "Test Maternity" }
        };

        //? cria uma cópia do método
        Mock<ILeaveTypeRepository> mockRepo = new ();

        //? quando usa o get
        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

        //? quando usa o create
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType); // adiciona a lista do leavetypes 4
                return Task.CompletedTask;
            });

        return mockRepo;
    }
}
