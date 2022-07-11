namespace Domain;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
}