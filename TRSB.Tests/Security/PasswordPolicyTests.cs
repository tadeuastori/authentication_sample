using TRSB.Infrastructure.Security;
using Xunit;

namespace TRSB.Tests.Security;

public class PasswordPolicyTests
{
    [Fact]
    public void Should_accept_valid_password()
    {
        var policy = new ConfigurablePasswordPolicy(6, 1, ['@']);
        policy.Validate("Test@123");
    }

    [Fact]
    public void Should_reject_weak_password()
    {
        var policy = new ConfigurablePasswordPolicy(6, 1, ['@']);

        Assert.Throws<ArgumentException>(() =>
            policy.Validate("12345"));
    }
}
