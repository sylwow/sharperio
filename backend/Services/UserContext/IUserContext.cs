namespace Sharperio.Services.UserContext
{
    public interface IUserContext
    {
        string Id { get; }
        string Name { get; }
        string Role { get; }
    }
}