namespace Parking_System.DbContext
{
    public interface IAppDbContext
    {
        bool AddSystemUser(string email, string password, string name, bool type);
        string FindSystemUser(string email, string password);
    }
}