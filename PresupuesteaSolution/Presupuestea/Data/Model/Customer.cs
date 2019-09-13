using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presupuestea.Data.Model
{
    public class Customer 
        {
             
              [Key]
              [ForeignKey("ApplicationUser")]
              public string CustomerId { get; set; }
              public string CustomerType { get; set; }
              public virtual ApplicationUser Usuario { get; set; }
              public string isHappy { get; set; }
    }
    
}
