using Ninject.Modules;
using Store.Services;
using Store.Services.Abstractions;

namespace Store.ConsoleApp
{
    /// <summary>
    /// Class ServiceModule for setuping dependencies.
    /// Implements the <see cref="NinjectModule" />
    /// </summary>
    /// <seealso cref="NinjectModule" />
    internal class ServiceModule : NinjectModule
    {
        /// <summary>
        /// Binds interface and its implementation.
        /// </summary>
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
