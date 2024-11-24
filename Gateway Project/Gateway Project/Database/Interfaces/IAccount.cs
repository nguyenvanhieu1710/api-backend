using Model;

namespace Gateway_Project.Database.Interfaces
{
    public interface IAccount
    {
        AccountModel GetDataById(int id);
        AccountModel GetDataByAccountNameAndPassword(string accountName, string password);
    }
}
