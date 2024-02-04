using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.DataProtection;
using QB.App.Services;

namespace QB.App.Tests;

[TestFixture]
public class TokenProtectorTests
{
    [Test]
    public void Protect_ShouldReturnProtectedToken()
    {
        // Arrange
        var token = "testToken";
        var protectedToken = "dGVzdFRva2Vu";

        // Act
        var result = TokenProtector.Protect(token);

        // Assert
        Assert.AreEqual(protectedToken, result);
    }

    [Test]
    public void Unprotect_ShouldReturnUnprotectedToken()
    {
        // Arrange
        var token = "testToken";
        var protectedToken = "dGVzdFRva2Vu";

        // Act
        var result = TokenProtector.Unprotect(protectedToken);

        // Assert
        Assert.AreEqual(token, result);
    }
}
