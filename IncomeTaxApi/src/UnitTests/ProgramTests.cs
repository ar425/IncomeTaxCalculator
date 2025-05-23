using FluentAssertions;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests;

public class ProgramTests
{
    [Test]
    public void CanBuildHost()
    {
        var builder = Program.CreateHostBuilder(Array.Empty<string>());

        Func<IHost> act = () => builder.Build();

        act.Should().NotThrow();
    }
}