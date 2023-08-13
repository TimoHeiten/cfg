using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using cfg.api.ConfigurationData;
using cfg.api.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace cfg.api.Tests;

public sealed  class MutateConfigTests : SingleDatabasePerTest
{
    private static readonly string BaseUrl = "/api/mutateconfig";
    private static ConfigurationValueModel ApiModel(string key = "apiSecret", string value = "abcaffeschnee") => new(key, value);
    private readonly IConfigurationDataProvider _dataProvider;
    public MutateConfigTests()
    {
        _dataProvider = TestServer.Services.GetRequiredService<IConfigurationDataProvider>();
    }

    private async Task<int> CurrentCountAsync()
    {
        return (await _dataProvider.GetAllAsync()).Count();
    }
    
    [Fact]
    public async Task CreateAddsAnd_Returns_Ok()
    {
        // Arrange
        var model = ApiModel("newKey");
        var count = await CurrentCountAsync();

        // Act
        var response = await Client.PostAsJsonAsync(BaseUrl, model);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ConfigValue>();
        result.Should().BeEquivalentTo(model.ToValue());

        var newAll = await CurrentCountAsync();
        newAll.Should().Be(count + 1);
    }

    [Fact]
    public async Task UpdateModifiesAnd_Returns_Ok()
    {
        // Arrange
        var model = ApiModel(value: "newValue");
        var count = await CurrentCountAsync();

        // Act
        var response = await Client.PutAsJsonAsync(BaseUrl, model);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ConfigValue>();
        result.Should().BeEquivalentTo(model.ToValue());
        var newAll = await CurrentCountAsync();
        newAll.Should().Be(count);
    }
    
    [Fact]
    public async Task Delete_Removes_Returns_Ok()
    {
        // Arrange
        var key = ApiModel().Key;
        var count = await CurrentCountAsync();

        // Act
        var response = await Client.DeleteAsync(BaseUrl + "/" + key);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<DeleteMessage>();
        result.Should().BeEquivalentTo(new { Message = $"Deleted {key}"});
        var newAll = await CurrentCountAsync();
        newAll.Should().Be(count - 1);
    }

    private sealed record DeleteMessage(string Message);
    
    [Fact]
    public async Task Create_WithExistingKey_Returns_BadRequest()
    {
        // Arrange
        var model = ApiModel();

        // Act
        var response = await Client.PostAsJsonAsync(BaseUrl, model);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Update_NoKeyFound_Returns_BadRequest()
    {
        // Arrange
        var model = ApiModel("key not existing");

        // Act
        var response = await Client.PutAsJsonAsync(BaseUrl, model);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Delete_NoKeyFound_Returns_BadRequest()
    {
        // Arrange
        var model = ApiModel("key does not exist");

        // Act
        var response = await Client.DeleteAsync(BaseUrl + "/" + model.Key);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}