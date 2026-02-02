namespace TRSB.Domain.Interfaces;

public interface IPasswordPolicy
{
    void Validate(string password);
}
