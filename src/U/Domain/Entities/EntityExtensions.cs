namespace U.Domain.Entities
{
    public static class EntityExtensions
    {
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        public static bool IsNullOrEmpty(this Entity entity)
        {
            return entity == null || (entity != null && entity.Id == 0);
        }
    }
}