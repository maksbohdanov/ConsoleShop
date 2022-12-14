using Ninject;
using Ninject.Modules;
using Store.BLL;

namespace Store.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            NinjectModule unitOfWorkModule = new UnitOfWorkModule();
            NinjectModule serviceModule = new ServiceModule();
            var kernel = new StandardKernel(unitOfWorkModule, serviceModule);

            var controller = new Controller(kernel);
            var client = new StoreClient(controller);
            client.Start();            
        }
    }
}
