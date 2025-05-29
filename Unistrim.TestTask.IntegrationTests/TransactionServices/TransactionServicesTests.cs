using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using TestTask.Unistrim.Api.Dto;
using Xunit.Abstractions;

namespace Unistrim.TestTask.IntegrationTests.TransactionServices;

public sealed class TransactionServicesTests: IClassFixture<TestApplication>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;
    
    public TransactionServicesTests(TestApplication testApplication, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = testApplication.CreateClient();
    }

    [Fact]
    public async Task PingTest()
    {
        var response = await _httpClient.GetAsync("/Transaction/GetTransactionOptions");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.Should().Be("text/plain");
    }

    [Fact]
    public async Task GetTransactions_WhenTransactionsExist_ShouldReturnTransactionsList()
    {
        var response = await _httpClient.GetAsync("/Transaction/GetTransactions");
        
        var content = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine($"Status Code: {response.StatusCode}");
        _testOutputHelper.WriteLine($"Content: {content}");
        
        var result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Transaction>>();

        result.Should().NotBeEmpty();
    }
}