using TRSB.Domain.Interfaces;

namespace TRSB.Infrastructure.Security;

public class ConfigurablePasswordPolicy : IPasswordPolicy
{
    private readonly int _minLength;
    private readonly int _minSpecialChars;
    private readonly char[] _specialChars;

    public ConfigurablePasswordPolicy(int minLength, int minSpecialChars, char[] specialChars)
    {
        _minLength = minLength;
        _minSpecialChars = minSpecialChars;
        _specialChars = specialChars;
    }

    public void Validate(string password)
    {
        if (password.Length < _minLength)
            throw new ArgumentException($"Password too short. Minimun of {_minLength} characters are required.");

        if(_specialChars.Length > 0)
        {
            var count = password.Count(c => _specialChars.Contains(c));
            if (count < _minSpecialChars)
                throw new ArgumentException($"Not enough special characters. Minimun of {_minSpecialChars} special characters are required. Special Characters accepted: {string.Join(", ", _specialChars)}");
        }        
    }
}
