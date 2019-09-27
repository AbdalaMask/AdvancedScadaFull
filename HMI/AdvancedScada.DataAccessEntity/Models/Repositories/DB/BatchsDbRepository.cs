using System.Collections.Generic;
using System.Linq;

namespace AdvancedScada.DataAccessEntity.Models.Repositories
{
    public class BatchsDbRepository : IBatchRepository<Batchs>
    {
        BatchsDbContext db;
        public BatchsDbRepository(BatchsDbContext _db)
        {
            db = _db;
        }
        public void Add(Batchs entity)
        {
            entity.BatchID = db.Batchs.Max(b => b.BatchID) + 1;
            db.Batchs.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var batchs = Find(id);
            db.Batchs.Remove(batchs);
            db.SaveChanges();
        }

        public Batchs Find(int id)
        {
            var batchs = db.Batchs.SingleOrDefault(a => a.BatchID == id);
            return batchs;

        }

        public IList<Batchs> List()
        {
            return db.Batchs.ToList();
        }

        public List<Batchs> Search(string term)
        {
            return db.Batchs.Where(a => a.BatchName.Contains(term)).ToList();
        }

        public void Update(int id, Batchs entity)
        {
            //db.Update(newAuthor);
            db.SaveChanges();
        }
    }
}
