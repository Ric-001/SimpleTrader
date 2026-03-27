namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500,
    }

    public class MajorIndex
    {
        public string IndexName => Type switch
        {
            MajorIndexType.DowJones => "Dow Jones",
            MajorIndexType.Nasdaq => "Nasdaq",
            MajorIndexType.SP500 => "S&P 500",
            _ => "Índice Desconocido"
        };

        public double Price { get; set; }
        public double Changes { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
