using Flunt.Notifications;

using Microsoft.EntityFrameworkCore;

using TDLS.Models;

namespace TDLS.Data;

public class TdlsContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public TdlsContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ignora a classe notification de validação, durante a construção do banco
        modelBuilder.Ignore<Notification>();
    }
}
