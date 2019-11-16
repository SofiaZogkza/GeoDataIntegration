using System.Linq;
using GeoDataIntegration.Interfaces;
using GeoDataIntegration.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeoDataIntegration.Tests
{
    [TestClass]
    public class GeoDataIntegrationTests
    {
        private readonly IGeoDataFileManager _service;
        public GeoDataIntegrationTests()
        {
            _service = new GeoDataFileManager();
        }

        // TDD
        [TestMethod]
        public void DeserializeTest()
        {
            var response = _service.DeserializeGeoDataFile();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Items);
            Assert.IsTrue(response.Items.Any());
        }
    }
}
