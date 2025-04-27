namespace ClassLibrary1.Service;

public interface IExternalUserService
{
    public Task<RootObjectUser?> GetUserByIdAsync(int userId);
    public Task<IEnumerable<Data>> GetAllUsersAsync();
}