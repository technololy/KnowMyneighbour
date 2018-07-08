using KnowMyneighbour.Controllers;
using KnowMyneighbour.Models;
using NUnit.Framework;

namespace KnowMyNeighbour.Tests
{
    [TestFixture]
    class VehicleControllerTests
    {
        private VehiclesController _controller = new VehiclesController();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Index()
        {
            var result = _controller.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void Details()
        {
            var result = _controller.Details(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_on_a_GET_request()
        {
            var result = _controller.Create();
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_on_a_POST_request()
        {
            var result = _controller.Create(new Vehicle());
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_on_a_GET_request()
        {
            var result = _controller.Edit(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_on_a_POST_request()
        {
            var result = _controller.Edit(new Vehicle());
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_on_a_GET_request()
        {
            var result = _controller.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_on_a_POST_request()
        {
            var result = _controller.Delete(1);
            Assert.IsNotNull(result);
        }
    }
}
