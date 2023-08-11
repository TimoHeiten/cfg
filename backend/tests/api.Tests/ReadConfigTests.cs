using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using cfg.api.ConfigurationData;
using FluentAssertions;
using Xunit;

namespace cfg.api.Tests;

public sealed class ReadConfigTests : SingleDatabasePerTest
{
    [Fact]
    public async Task GetConfigValue_Per_Key_NotFound_Returns404()
    {
        // Act
        var response = await Client.GetAsync($"api/ReadConfig/does_not_exist");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetConfigValue_Per_Key_Found_Returns_Ok()
    {
        // Act
        var response = await Client.GetAsync($"api/ReadConfig/apiSecret");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var configValue = await response.Content.ReadFromJsonAsync<ConfigValue>();
        configValue!.Value.Should().Be("abcaffeschnee");
    }
}