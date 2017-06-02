using U.Domain.Entities;

namespace U.Domain.Uow
{
    /// <summary>
    /// Standard filters of U.
    /// </summary>
    public static class UDataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// </summary>
        public const string SoftDelete = "SoftDelete";
    }
}