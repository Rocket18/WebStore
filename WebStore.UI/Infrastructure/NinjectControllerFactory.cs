using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using WebStore.Models.Abstract;
using WebStore.Models.DAL;
using WebStore.UI.Controllers;
using WebStore.Models.Entities;

namespace WebStore.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }


    }
}