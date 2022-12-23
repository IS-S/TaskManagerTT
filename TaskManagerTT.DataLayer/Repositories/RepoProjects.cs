using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.DataAccessLayer.Repositories
{
    // Class inherited from Base Repositpry class to have possibility of including linked Tasks to each Project
    public class RepoProjects : RepoBase<SiProject> 
    {
        public RepoProjects(TaskManagerDBContext context) : base(context)
        {
            
        }

        public override IEnumerable<SiProject> GetAll() // Get All Project instances (overrided to include linked Tasks to each Project)
        {
            IQueryable<SiProject> qry = this._entitiySet.AsQueryable<SiProject>().Include(x => x.Tasks);
            
            return qry.AsEnumerable();

        }
        public override IQueryable<SiProject> GetAllQuery() // Get All Project instances (overrided to include linked Tasks to each Project)
        {
            var qry = base.GetAllQuery();
            qry = qry.Include(x => x.Tasks);
            return qry;
        }
        public override IQueryable<SiProject> GetEntity(int id) // Get Single Project instance (overrided to include linked Tasks to Project)
        {
            var res = base.GetEntity(id);
            res = res.Include(x => x.Tasks);
            return res;
        }

    }
}
