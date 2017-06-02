using System.Data.Entity.ModelConfiguration;

namespace U.EntityFramework.Mapping
{
    public abstract class UEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected UEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
