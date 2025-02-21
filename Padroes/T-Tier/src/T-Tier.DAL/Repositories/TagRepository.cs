using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories
{
    public class TagRepository(AppDbContext context, ILogger<TagRepository> logger)
        : GenericRepository<Tag>(context, logger), ITagRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(q => q.Name == name);
        }

        public async Task<bool> AddTagsToPost(List<int> ids, int postId)
        {
            logger.LogInformation("DAL-REPO: Adicionando {TagsCount} [PostTags] ao [Post]: {PostId}.",
                ids.Count, postId);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var parameters = new object[] { new SqlParameter("@postId", postId) };
                var sqlInClause = new List<string>();

                foreach (var id in ids)
                {
                    sqlInClause.Add($"(@postId, {id})");
                }

                var sql = $"INSERT INTO blog.PostTag (PostId, TagId) VALUES {string.Join(", ", sqlInClause)}";

                logger.LogInformation("DAL-REPO: Executando SQL: {SqlQuery} com PostId = {PostId} e Tags = {Tags}", sql, postId, ids);

                var insertedTags = await _context.Database.ExecuteSqlRawAsync(sql, parameters);

                await transaction.CommitAsync();

                logger.LogInformation("DAL-REPO: {InsertedTags} [PostTags] adicionadas ao [Post]: {PostId}.",
                    insertedTags, postId);

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                logger.LogError("DAL-REPO: Erro ao adicionar {TagsCount} [PostTags] ao [Post]: {PostId}. " +
                                "Exception: {ExceptionType} - {Message}",
                    ids.Count, postId, ex.GetType().Name, ex.Message);

                return false;
            }
        }

        public async Task<bool> DeleteTagsWithPost(List<int> ids, int postId)
        {
            logger.LogInformation("DAL-REPO: Deletando {TagsCount} [PostTags] do [Post]: {PostId}.",
                ids.Count, postId);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Criar parâmetros individuais para cada item da lista
                var parameters = new List<object> { new SqlParameter("@postId", postId) };
                var sqlInClause = new List<string>();

                for (var i = 0; i < ids.Count; i++)
                {
                    var paramName = $"@p{i}";
                    sqlInClause.Add(paramName);
                    parameters.Add(new SqlParameter(paramName, ids[i]));
                }

                // Montar SQL dinâmico corretamente
                var sql = $"DELETE FROM blog.PostTag WHERE PostId = @postId AND TagId IN ({string.Join(", ", sqlInClause)})";

                logger.LogInformation("DAL-REPO: Executando SQL: {SqlQuery} com PostId = {PostId} e Tags = {Tags}", sql, postId, ids);

                // Executar a query com os parâmetros
                var deletedTags = await _context.Database.ExecuteSqlRawAsync(sql, parameters.ToArray());

                await transaction.CommitAsync();

                logger.LogInformation("DAL-REPO: {DeletedTags} [PostTags] deletadas do [Post]: {PostId}.",
                    deletedTags, postId);

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                logger.LogError("DAL-REPO: Erro ao deletar {TagsCount} [PostTags] do [Post]: {PostId}. " +
                                "Exception: {ExceptionType} - {Message}",
                    ids.Count, postId, ex.GetType().Name, ex.Message);

                return false;
            }
        }

        public async Task<List<Tag>> GetByIdsAsync(List<int> ids)
        {
            try
            {
                return await _context.Tags.AsNoTracking().Where(tag => ids.Contains(tag.Id)).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DAL-REPO: Erro ao buscar múltiplas tags com IDs {Ids}.", string.Join(",", ids));
                return [];
            }
        }
    }
}