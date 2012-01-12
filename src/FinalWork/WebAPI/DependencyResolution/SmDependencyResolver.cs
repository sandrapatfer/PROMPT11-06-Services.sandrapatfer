using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StructureMap;

namespace DependencyResolution
{
    public class SmDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public SmDependencyResolver(IContainer container)
        {
            _container = container;
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            try
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return _container.TryGetInstance(serviceType);
                }
                else
                {
                    return _container.GetInstance(serviceType);
                }
            }
            catch
            {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }

        #endregion
    }
}
