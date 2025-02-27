using Microsoft.EntityFrameworkCore;

namespace ScalarApi.Data;

public class PersonaSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>().HasData(
            new Persona
            {
                Id = 1,
                Name = "Alice",
                Age = 30
            },
            new Persona
            {
                Id = 2,
                Name = "Bob",
                Age = 40
            },
            new Persona
            {
                Id = 3,
                Name = "Charlie",
                Age = 50
            }
        );
    }
}