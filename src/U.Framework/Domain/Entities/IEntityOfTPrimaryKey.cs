namespace U.Domain.Entities
{
    /// <summary>
    /// 定义实体类型的接口，系统中所有的实体都必须继承此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键的类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查实体是否为transient
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
