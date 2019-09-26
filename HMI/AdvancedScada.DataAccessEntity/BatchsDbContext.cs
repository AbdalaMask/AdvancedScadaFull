using System.Data.Entity;


namespace AdvancedScada.DataAccessEntity.Models
{
    public  class BatchsDbContext: DbContext
    {
        public BatchsDbContext() : base("name=Sqlcon")
        {

        }
       

        public  DbSet<Batchs> Batchs { get; set; }
        public  DbSet<Tanks> Tanks { get; set; }
        public  DbSet<BatchFinal> BatchFinal { get; set; }
        public  DbSet<BatchsDetails> BatchsDetails { get; set; }
        public  DbSet<BatchWeight> BatchWeight { get; set; }
        public  DbSet<NameTankFinal> NameTankFinal { get; set; }
    }
}
