using System.Collections.Generic;

namespace KnowMyneighbour.Logic.Contracts
{
    public interface ICarMake
    {
        int Id { get; set; }
        string CarMaker { get; set; }
        int? VehicleTypeId { get; set; }
        IVehicle Vehicle { get; set; }
        ICollection<ICarModel> CarModels { get; set; }
    }
}
