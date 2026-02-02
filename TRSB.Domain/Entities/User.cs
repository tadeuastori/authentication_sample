namespace TRSB.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    protected User() { }

    public User(string username, string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public void UpdateProfile(string name, string userName, string email, string passwordHash)
    {
        Name = name;
        Username = userName;
        Email = email;
        PasswordHash = passwordHash;
    }
}
