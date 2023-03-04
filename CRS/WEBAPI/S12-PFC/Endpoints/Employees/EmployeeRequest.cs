namespace S12_PFC.Endpoints.Employees;

public record class EmployeeRequest(string Email, string Password, string Name, string EmployeeCode);