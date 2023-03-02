using Microsoft.EntityFrameworkCore;

using TDLS.Models;

namespace TDLS.Data;

public class TdlsContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public TdlsContext(DbContextOptions options) : base(options)
    {
    }
}
