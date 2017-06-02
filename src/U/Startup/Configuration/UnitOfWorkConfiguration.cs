using U.Domain.Uow;

namespace U.Startup.Configuration
{
    public class UnitOfWorkConfiguration : IUnitOfWorkConfiguration
    {
        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }

        public UnitOfWorkConfiguration(IUnitOfWorkDefaultOptions options) {
            UnitOfWork = options;
        }
        
    }
}
