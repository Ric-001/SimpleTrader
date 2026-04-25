using System.ComponentModel;

namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel : INotifyPropertyChanged
    {
        string ErrorMessage { set; }
        string SearchResultSymbol { get; }
        double StockPrice { get; set; }
        string Symbol { get; set; }
        
    }
}