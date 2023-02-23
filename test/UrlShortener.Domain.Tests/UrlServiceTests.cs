using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NumeralSystemConversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models;
using UrlShortener.Data.Repositories.Interfaces;
using UrlShortener.Domain.Services;
using Xunit;

namespace UrlShortener.Domain.Tests
{
    public class UrlServiceTests
    {
        private readonly Mock<IUrlRepository> _repositoryMock = new Mock<IUrlRepository>();
        private readonly UrlService _sut;
        ulong _last = 5ul;
        public UrlServiceTests()
        {
            _repositoryMock.Setup(r => r.GetAllUrlPairsAsync())
                .ReturnsAsync(GetUrlPairs());
            _repositoryMock.Setup(r => r.CreateShortUrlAsync(It.IsAny<UrlPairDomainModel>(), It.IsAny<ulong>()));

            var loggerMock = new Mock<ILogger<UrlService>>();

            var configMock = new Mock<IConfiguration>();

            _sut = new UrlService(loggerMock.Object, _repositoryMock.Object, configMock.Object);
        }

        [Fact]
        public async Task CreateShortUrlAsync_NewValid_CreateAndReturnShort()
        {
            UrlPair pairFromDatabase = null;
            _repositoryMock.Setup(r => r.GetLastNumericId())
                .ReturnsAsync(_last);
            _repositoryMock.Setup(r => r.GetUrlPairByLongUrlAsync(It.IsAny<string>())).ReturnsAsync(pairFromDatabase);
            var result = await _sut.CreateShortUrlAsync("whatever");

            var shortCodeExpected = Converter.ConvertFromDecimal(_last + 1);
            Assert.Equal(shortCodeExpected, result);
        }

        [Fact]
        public async Task CreateShortUrlAsync_ExistingValid_ReturnShort()
        {
            UrlPair pairFromDatabase = GetUrlPair();
            _repositoryMock.Setup(r => r.GetLastNumericId())
                .ReturnsAsync(_last);
            _repositoryMock.Setup(r => r.GetUrlPairByLongUrlAsync(It.IsAny<string>())).ReturnsAsync(pairFromDatabase);
            var result = await _sut.CreateShortUrlAsync("whatever");

            var shortCodeExpected = pairFromDatabase.ShortUrlCode;
            Assert.Equal(shortCodeExpected, result);
        }

        [Fact]
        public async Task CreateShortUrlAsync_OutOfRange_ThrowArgumentOutOfRangeException()
        {
            ulong last = ulong.MaxValue - 1;
            UrlPair pairFromDatabase = null;
            _repositoryMock.Setup(r => r.GetLastNumericId())
                .ReturnsAsync(last);
            _repositoryMock.Setup(r => r.GetUrlPairByLongUrlAsync(It.IsAny<string>())).ReturnsAsync(pairFromDatabase);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.CreateShortUrlAsync("whatever"));
        }

        [Fact]
        public async Task GetAllUrlPairsAsync_ReturnsFromRepo()
        {
            IEnumerable<UrlPairDomainModel> result = await _sut.GetAllUrlPairsAsync();
            UrlPairDomainModel? pair = result.FirstOrDefault();

            Assert.Single(result);
            Assert.Equal("aa", pair.LongUrl);
            Assert.Equal("bb", pair.ShortUrlCode);
        }

        [InlineData("aa", true)]
        [InlineData("aaBB", true)]
        [InlineData("aa0", false)]
        [InlineData("0000", false)]
        [InlineData(",.;'[]", false)]
        [Theory]
        public void IsCodeStructureValid_ValidAndInvalid(string shortUrlCode, bool expected)
        {
            var result = _sut.IsCodeStructureValid(shortUrlCode);
            Assert.Equal(expected, result);
        }

        private IEnumerable<UrlPair> GetUrlPairs()
        {
            var list = new List<UrlPair> { GetUrlPair() };
            return list;
        }

        private UrlPair GetUrlPair()
        {
            return new UrlPair
            {
                LongUrl = "aa",
                ShortUrlCode = "bb"
            };
        }
    }
}
