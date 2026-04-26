using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.Views
{
    /// <summary>
    /// Lógica de interacción para SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {
        public ICommand SelectedAssetChangedCommand
        {
            get { return (ICommand)GetValue(SelectedAssetChangedCommandProperty); }
            set { SetValue(SelectedAssetChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAssetChangeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAssetChangedCommandProperty =
            DependencyProperty.Register(nameof(SelectedAssetChangedCommand), typeof(ICommand), typeof(SellView), new PropertyMetadata(null));


        public SellView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAssets.SelectedItem != null) 
                SelectedAssetChangedCommand?.Execute(null);
        }
    }
}
