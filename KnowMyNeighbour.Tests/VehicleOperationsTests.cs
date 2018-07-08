using KnowMyneighbour.Logic.Contracts;
using NUnit.Framework;
using System.Linq;

namespace KnowMyNeighbour.Tests
{
    [TestFixture]
    public class VehicleOperationsTests
    {
        private IVehicleOperations _vehicleOperations;
        private IVehicle _vehicle;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AllVehiclesTests()
        {
            Assert.IsTrue(_vehicleOperations.AllVehicles().Count() > 0);
        }

        [Test]
        public void CreateVehicle()
        {
            Assert.DoesNotThrow(() => _vehicleOperations.CreateVehicle(_vehicle));
        }

        [Test]
        public void DeleteVehicle()
        {
            Assert.DoesNotThrow(() => _vehicleOperations.DeleteVehicle(1));
        }

        [Test]
        public void EditVehicle()
        {
            Assert.DoesNotThrow(() => _vehicleOperations.EditVehicle(_vehicle));
        }
        [Test]
        public void VehicleDetails()
        {
            Assert.IsInstanceOf<IVehicle>(_vehicleOperations.VehicleDetails(1));
        }
    }
}
