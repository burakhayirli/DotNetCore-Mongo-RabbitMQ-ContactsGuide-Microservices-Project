using Autofac;
using Autofac.Extras.DynamicProxy;
using Contact.Api.Controllers;
using Contact.Repository;
using ContactGuide.Shared.Utilities.Interceptors.Autofac;

namespace Contact.Api.DependencyResolvers.Autofac
{
    public class AutofacApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();            

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
