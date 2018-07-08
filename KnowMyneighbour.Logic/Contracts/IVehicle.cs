using System.Collections.Generic;

namespace KnowMyneighbour.Logic.Contracts
{
    public interface IVehicle
    {
        int Id { get; set; }
        string VehicleType { get; set; }
        string VehicleExample { get; set; }
        ICollection<ICarMake> CarMakes { get; set; }
    }
}
