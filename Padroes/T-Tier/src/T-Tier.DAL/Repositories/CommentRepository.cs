using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class CommentRepository(AppDbContext context) : GenericRepository<Comment>(context), ICommentRepository
{
    
}
