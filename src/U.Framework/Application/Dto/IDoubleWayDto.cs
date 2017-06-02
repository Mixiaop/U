namespace U.Application.Dto
{
    /// <summary>
    /// This interface can be used to mark a DTO as both of <see cref="IInputDto"/> and <see cref="IOutputDto"/>.
    /// </summary>
    public interface IDoubleWayDto : IInputDto, IOutputDto
    {

    }
}