using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presupuestea.Data.Model
{
    public class Contractor
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ContractorID { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string ContractorType { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
