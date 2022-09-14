using Ninject.Modules;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;

namespace Store.BLL
{
    public class UnitOfWorkModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();            
        }
    }
}
