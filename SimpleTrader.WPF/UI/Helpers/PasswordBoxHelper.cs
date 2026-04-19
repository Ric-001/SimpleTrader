using System.Windows;
using System.Windows.Controls;

namespace SimpleTrader.WPF.UI.Helpers
{
    public static class PasswordBoxHelper
    {
        // ── 1. Guardia anti-recursión ─────────────────────────────────────────-
        private static bool _isUpdating = false;

        // ── 2. La Attached Property ───────────────────────────────────────────
        public static readonly DependencyProperty BoundPasswordProperty = DependencyProperty.RegisterAttached(
                "BoundPassword",                                            // nombre de la propiedad
                typeof(string),                                             // tipo del valor
                typeof(PasswordBoxHelper),                                  // clase propietaria
                new FrameworkPropertyMetadata(                              // metadatos
                    string.Empty,                                           // valor por defecto
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,  // Garantiza que WPF sepa que la propiedad es bidireccional por defecto y propague correctamente el valor del PasswordBox al ViewModel.
                    OnBoundPasswordChanged));                               //   callback al cambiar

        // ── 3. Getter y setter estáticos (obligatorios por convención WPF) ────
        public static string GetBoundPassword(DependencyObject d) => (string)d.GetValue(BoundPasswordProperty);

        public static void SetBoundPassword(DependencyObject d, string value) => d.SetValue(BoundPasswordProperty, value);

        // ── 4. Callback: el ViewModel actualizó la propiedad ─────────────────
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox box) return;

            // Nos suscribimos al evento del control la primera vez
            box.PasswordChanged -= OnPasswordChanged;
            box.PasswordChanged += OnPasswordChanged;

            // Sincronizamos el control si el cambio vino del ViewModel
            if (!_isUpdating)
                box.Password = (string)e.NewValue;
        }

        // ── 5. Callback: el usuario escribió en el PasswordBox ────────────────
        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox box) return;

            _isUpdating = true;
            box.SetCurrentValue(BoundPasswordProperty, box.Password); // actualiza la DP → WPF lo propaga al VM
            _isUpdating = false;
        }
    }
}