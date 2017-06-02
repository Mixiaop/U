using System.Collections.Generic;

namespace U.Application.Services.Events
{
    public class SubscriptionService : ISubscriptionService
    {
        public IList<IConsumer<T>> GetSubscriptions<T>() {
            return UPrimeEngine.Instance.IocManager.ResolveAll<IConsumer<T>>();
        }
    }
}
