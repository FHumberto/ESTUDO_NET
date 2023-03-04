using Dapper;

using Microsoft.Data.SqlClient;

using S12_PFC.Endpoints.Categories;

namespace S12_PFC.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // QUERY
    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var dataBase = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")); // FAZ A CONEXÃO COM O BANCO

        var query =
        @"select Email, ClaimValue as Name
        from AspNetUsers u inner
        join AspNetUserClaims c
        on u.id = c.UserId and claimtype = 'Name'
        order by name
        OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY"; // PAGINAÇÃO

        // CONVERTE A QUERY EM EMPLOYEE RESPONSE (COMO NÃO TEM NAME, TEM Q UE COLOCAR APELIDO (as)
        return dataBase.Query<EmployeeResponse>(
            query,
            new { page, rows } // PASSA OS PARÂMETROS QUE IRÃO SER USADOS NA QUERY
        );
    }
}
