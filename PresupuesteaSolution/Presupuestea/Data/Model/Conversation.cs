using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presupuestea.Data.Model
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public string Text { get; set; }
        public string CustomerId { get; set; }
        public string ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Customer Customer { get; set; }



    }
}
