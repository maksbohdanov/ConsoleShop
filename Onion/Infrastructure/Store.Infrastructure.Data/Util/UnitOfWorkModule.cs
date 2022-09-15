using Ninject.Modules;
using Store.Domain.Core.Repositories;
using Store.Infrastructure.Data.Repositories;

namespace Store.Infrastructure.Data.Util
{
    /// <summary>
    /// Class UnitOfWorkModule for setuping dependencies.
    /// Implements the <see cref="NinjectModule" />
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class UnitOfWorkModule : NinjectModule
    {
        /// <summary>
        /// Binds interface and its implementation.
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
