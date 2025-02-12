namespace T_Tier.DAL.Contracts;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; }
    void SoftDelete();
    void Restore();
}