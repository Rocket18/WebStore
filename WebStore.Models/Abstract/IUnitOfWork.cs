using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Models.Entities;

namespace WebStore.Models.Abstract
{
    public interface IUnitOfWork
    {

        IRepository<Products> Products { get; }
        IRepository<Users> Users { get; }
        IRepository<UsersRole> UsersRole { get; }
        IRepository<Categories> Categories { get; }
        IRepository<Status> Status { get; }
        void Save();
    }
}
