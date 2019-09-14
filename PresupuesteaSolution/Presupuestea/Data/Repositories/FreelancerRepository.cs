using Presupuestea.Data.Interfaces;
using Presupuestea.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presupuestea.Data.Repositories
{
    public class FreelancerRepository : Repository<Contractor>, IFreelancersRepository
    {
        public FreelancerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
