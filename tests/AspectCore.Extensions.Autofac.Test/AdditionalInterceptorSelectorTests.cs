using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.Autofac;
using Autofac;
using Xunit;

namespace AspectCoreTest.Autofac
{
   public class AdditionalInterceptorSelectorTests
    {
        [Fact]
        public void ImplementationMethod_Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterDynamicProxy();
            builder.RegisterType<Service>().As<IService>();
            var provider = builder.Build();
            var service = provider.Resolve<IService>();
            var val = service.GetValue("le");
            Assert.Equal("lemon", val);
        }

        public class Intercept : AbstractInterceptorAttribute
        {
            public override Task Invoke(AspectContext context, AspectDelegate next)
            {
                context.Parameters[0] = "lemon";
                return context.Invoke(next);
            }
        }

        public interface IService
        {
            string GetValue(string val);
        }

        public class Service : IService
        {
            [Intercept]
            public string GetValue(string val)
            {
                return val;
            }
        }
    }
}
