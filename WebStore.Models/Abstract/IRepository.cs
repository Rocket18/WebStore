using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Models.Entities;
using System.Linq.Expressions;

namespace WebStore.Models.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,string includeProperties = "");
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(int id);
        T FindById(int id);
    }
}
