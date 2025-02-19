using T_Tier.DAL.Entities;

public interface IUserRepository
{
    Task<bool> DeleteUserWithDependenciesAsync(string userId);
    Task<bool> SoftDeleteAsync(User user);
}