namespace SimpleTrader.WPF.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message = string.Empty;
        public string Message { get => _message; set => SetProperty(ref _message, value, [nameof(HasMessage)]); }
        public bool HasMessage => !string.IsNullOrEmpty(Message);

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
