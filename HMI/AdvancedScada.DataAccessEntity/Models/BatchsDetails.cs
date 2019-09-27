

using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedScada.DataAccessEntity.Models
{
    public class BatchsDetails
    {

        public int BatchID { get; set; }
        public int? TankID { get; set; }
        public string TankName { get; set; }
        public double? MixWeight { get; set; }
        public double? LowWeight { get; set; }
        public double? FreeFallWeight { get; set; }
        public double? HighSpeed { get; set; }
        public double? LowSpeed { get; set; }
        public string Working { get; set; }
        public int? Orders { get; set; }

        [ForeignKey(nameof(BatchID))]
        public virtual Batchs Batchs { get; set; }

    }

}