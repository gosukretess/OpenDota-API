using System.Linq;
using Xunit;

namespace OpenDotaApi.Test.ApiTests
{
    public class PublicMatchTests: IClassFixture<OpenDotaTestsFixtures>
    {
        private readonly OpenDota _openDota;

        public PublicMatchTests(OpenDotaTestsFixtures fixtures) => _openDota = fixtures.OpenDota;

        [Fact]
        public async void GetListPublicMatchesAsync()
        {
            var data = await _openDota.PublicMatches.GetListPublicMatchesAsync();
            Assert.NotEmpty(data);

            var data1 = await _openDota.PublicMatches.GetListPublicMatchesAsync(1);
            Assert.Equal(11, data1.First().AvgRankTier);
            
            var data2 = await _openDota.PublicMatches.GetListPublicMatchesAsync(1,4500,6163720706);
            Assert.Equal(11, data2.First().AvgRankTier);
        }
    }
}