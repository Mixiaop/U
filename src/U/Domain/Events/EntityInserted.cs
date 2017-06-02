using U.Domain.Entities;

namespace U.Domain.Events
{
    /// <summary>
    /// 表示新插入一个【实体】到容器的事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityInserted<T> 
    {
        public EntityInserted(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    
    }
}
