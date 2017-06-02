using System;

namespace U.Events.Bus.Entities
{
    /// <summary>
    /// 用于供事件之间传输的实体对象，当实体发生 (created, updated or deleted) 改变时 (<see cref="IEntity"/>)使用
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
