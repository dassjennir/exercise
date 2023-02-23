using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models;
using UrlShortener.Domain.Services.Interfaces;
using UrlShortener.Server.Controllers;

namespace UrlShortener.Server.Tests
{
    public class UrlsControllerUnitTests
    {
        [Fact]
        public async Task Dupa()
        {
            UrlPairDomainModel[] pairs = new UrlPairDomainModel[]
            {
                new UrlPairDomainModel()
            };

            Mock<IUrlService> mockUrlService = new Mock<IUrlService>();
            mockUrlService.Setup(m => m.GetAllUrlPairsAsync()).ReturnsAsync(pairs);

            var sut = new UrlsController(null, mockUrlService.Object);
            var result = await sut.GetAll();
            Assert.Equal(1, result.Count());
        }
    }
}
