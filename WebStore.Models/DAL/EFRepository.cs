using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Models.Abstract;
using WebStore.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;


namespace WebStore.Models.DAL
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        internal WebStoreContext context;

        internal DbSet<T> dbSet;

        public EFRepository(WebStoreContext context)
        {
            this.context = context;
          //  context.Database.Initialize(false);
     
            // this.context.Configuration.ProxyCreationEnabled = false;
            dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

           
           
                return query;//.ToList();
          
        }
        public virtual T FindById(int id)
        {
            return dbSet.Find(id);

        }

        public virtual void Insert(T obj)
        {

            dbSet.Add(obj);
        }

        public virtual void Update(T obj)
        {
            //dbSet.Attach(obj);
            //context.Entry(obj).State = EntityState.Modified;

            var entry = this.context.Entry(obj);
            var key = this.GetPrimaryKey(entry);

            if (entry.State == EntityState.Detached)
            {
                var currentEntry = dbSet.Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.context.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(obj);
                }
                else
                {
                    dbSet.Attach(obj);
                    entry.State = EntityState.Modified;
                }
            }


        }

        private int GetPrimaryKey(DbEntityEntry entry)
        {
            var myObject = entry.Entity;
            var property =
                myObject.GetType()
                     .GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            return (int)property.GetValue(myObject, null);
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(T obj)
        {
            if (context.Entry(obj).State == EntityState.Detached)
            {
                dbSet.Attach(obj);
            }
            dbSet.Remove(obj);
        }



    }
}
