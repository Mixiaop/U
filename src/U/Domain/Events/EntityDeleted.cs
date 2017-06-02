using U.Domain.Entities;

namespace U.Domain.Events
{
    /// <summary>
    /// 表求你从容器删除一个【实体】的事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleted<T>
    {
        public EntityDeleted(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    }
}
