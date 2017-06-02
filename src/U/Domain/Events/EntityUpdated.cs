using U.Domain.Entities;

namespace U.Domain.Events
{
    /// <summary>
    /// 表示更新一个容器的【实体】事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityUpdated<T> 
    {
        public EntityUpdated(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; private set; }

    }
}
