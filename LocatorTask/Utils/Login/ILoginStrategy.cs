namespace LocatorTask.Utils.Login;

public interface ILoginStrategy
{
    public void Login(string name, string password);
}