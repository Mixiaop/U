using System;
using System.Collections.Generic;
using System.Linq;
using U.Reflection;

namespace U.UPrimes
{
    internal class DefaultUPrimeFinder : IUPrimeFinder
    {
        private readonly ITypeFinder _typeFinder;

        public DefaultUPrimeFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(UPrime.IsUPrime).ToList();
        }
    }
}
