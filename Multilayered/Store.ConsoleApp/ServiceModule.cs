using Ninject.Modules;
using Store.BLL.Services;
using Store.BLL.Interfaces;

namespace Store.ConsoleApp
{
    public class ServiceModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
