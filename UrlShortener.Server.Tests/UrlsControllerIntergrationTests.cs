using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net.Http.Json;
using UrlShortener.Data.Models;
using UrlShortener.Domain.Services.Interfaces;
using UrlShortener.Server.Controllers;
using UrlShortener.Shared.SharedModels;

namespace UrlShortener.Server.Tests
{
    public class UrlsControllerIntergrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string apiUrl = "api/urls";

        public UrlsControllerIntergrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllOnEmptyDatabase_ReturnEmpty()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<UrlPairDTO[]>();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllOnDatabase_ReturnCount()
        {
            var customFactory = _factory.WithWebHostBuilder(
            (IWebHostBuilder hostBuilder) =>
            {
                hostBuilder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<IUrlService>();
                    services.AddSingleton<IUrlService, FakeUrlService>();
                });
            });

            HttpClient client = customFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<UrlPairDTO[]>();
            Assert.Single(result);
        }
    }

    internal class FakeUrlService : IUrlService
    {
        public Task<string> CreateShortUrlAsync(string longUrl)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UrlPairDomainModel>> GetAllUrlPairsAsync()
        {
            UrlPairDomainModel[] pairs = new UrlPairDomainModel[]
            {
                new UrlPairDomainModel()
            };
            return Task.FromResult((IEnumerable<UrlPairDomainModel>)pairs);
        }

        public Task<string> GetLongUrlAsync(string shortUrlCode)
        {
            throw new NotImplementedException();
        }

        public bool IsCodeStructureValid(string shortUrlCode)
        {
            throw new NotImplementedException();
        }
    }
}