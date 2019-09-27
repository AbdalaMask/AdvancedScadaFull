
using System.ComponentModel.DataAnnotations;

namespace AdvancedScada.DataAccessEntity.Models
{
    public class Batchs
    {

        [Key]
        public int BatchID { get; set; }
        public string BatchName { get; set; }

        public virtual BatchsDetails BatchsDetails { get; set; }

    }

}