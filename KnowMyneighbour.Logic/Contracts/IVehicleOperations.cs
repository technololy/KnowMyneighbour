using System.Collections.Generic;

namespace KnowMyneighbour.Logic.Contracts
{
    public interface IVehicleOperations
    {
        void DeleteVehicle(int id);
        void EditVehicle(IVehicle vehicle);
        void CreateVehicle(IVehicle vehicle);
        IVehicle VehicleDetails(int id);
        IEnumerable<IVehicle> AllVehicles();
    }
}
