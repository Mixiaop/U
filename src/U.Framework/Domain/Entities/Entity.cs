using System;

namespace U.Domain.Entities
{
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}