using System.Collections.Generic;

namespace AdvancedScada.DataAccessEntity.Models.Repositories
{
    public interface IBatchRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);
    }
}
