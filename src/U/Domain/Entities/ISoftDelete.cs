namespace U.Domain.Entities
{
    /// <summary>
    /// 用于规范实体软删除，实体软删除不是真正的删除，
    /// 在数据库中，使用IsDeleted = true来标记
    /// 但是，应用不能获取到此实体
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
