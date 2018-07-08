namespace KnowMyneighbour.Logic.Contracts
{
    public interface ICarModel
    {
        int Id { get; set; }
        int? CarMaker { get; set; }

        ICarMake CarMake { get; set; }
    }
}
