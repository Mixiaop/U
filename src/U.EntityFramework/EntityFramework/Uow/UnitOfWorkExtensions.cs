using System;
using System.Data.Entity;
using U.Domain.Uow;

namespace U.EntityFramework.Uow
{
    /// <summary>
    /// UOW 的扩展方法
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 从已激活的 UOW 中获取 DbContext
        /// 此方法能被当前的 UOW 调用 <see cref="EfUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="TDbContext">DbContext 的类型</typeparam>
        /// <param name="unitOfWork">当前（激活）的工作单元</param>
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}
