using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using DomainModel;
using DomainModel.Repository;

namespace DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Scan(scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                    });
                    x.For<IPostManager>().HttpContextScoped().Use<PostManagerImpl>();
                    x.For<IPostRepository>().HttpContextScoped().Use<PostRepositoryImpl>();
                });
            return ObjectFactory.Container;
        }

        public static IPostManager GetPostManager()
        {
            return ObjectFactory.GetInstance<IPostManager>();
        }
    }
}
