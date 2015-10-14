using System;
using System.Diagnostics;
using WebStore.Models.Abstract;
using WebStore.Models.Entities;


namespace WebStore.Models.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private WebStoreContext context = new WebStoreContext();

        private IRepository<Products> productRepository;
        private IRepository<Users> userRepository;
        private IRepository<UsersRole> userRoleRepository;
        private IRepository<Categories> categoryRepository;
        private IRepository<Status> statusRepository;

        public IRepository<Products> Products
        {
            get
            {

                if (productRepository == null)
                {
                    productRepository = new EFRepository<Products>(context);//EFRepository<Products>(context);
                }
                return productRepository;
            }
        }
        public IRepository<Users> Users
        {
            get
            {

                if (userRepository == null)
                {

                    userRepository = new EFRepository<Users>(context);
                }
                return userRepository;
            }
        }
        public IRepository<UsersRole> UsersRole
        {
            get
            {

                if (userRoleRepository == null)
                {
                    userRoleRepository = new EFRepository<UsersRole>(context);
                }
                return userRoleRepository;
            }
        }
        public IRepository<Categories> Categories
        {
            get
            {

                if (categoryRepository == null)
                {
                    categoryRepository = new EFRepository<Categories>(context);
                }
                return categoryRepository;
            }
        }

        public IRepository<Status> Status
        {
            get
            {

                if (statusRepository == null)
                {
                    statusRepository = new EFRepository<Status>(context);
                }
                return statusRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
