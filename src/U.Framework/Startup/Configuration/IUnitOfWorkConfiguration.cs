using U.Domain.Uow;

namespace U.Startup.Configuration
{
    public interface IUnitOfWorkConfiguration
    {
        IUnitOfWorkDefaultOptions UnitOfWork { get; }
    }
}
