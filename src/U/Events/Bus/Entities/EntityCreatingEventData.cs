using System;

namespace U.Events.Bus.Entities
{
    /// <summary>
    /// 当实体创建前用于通知的事件类型
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is being created</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
