using ApklisPaymentCheckerMaui.Interfaces;

namespace ApklisPaymentCheckerMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly IApklisPaymentChecker _paymentChecker;

        public MainPage(IApklisPaymentChecker paymentChecker)
        {
            InitializeComponent();
            _paymentChecker = paymentChecker;
        }

        private async void OnCheckPaymentClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener el nombre del paquete del input
                string packageName = PackageNameEntry.Text?.Trim();

                // Validar que se haya introducido un nombre de paquete
                if (string.IsNullOrWhiteSpace(packageName))
                {
                    await DisplayAlert("Error", "Por favor, introduzca el nombre del paquete de la aplicación.", "OK");
                    return;
                }

                // Validar formato básico del nombre del paquete
                if (!IsValidPackageName(packageName))
                {
                    await DisplayAlert("Error", "El formato del nombre del paquete no es válido.\nEjemplo: com.yourcompany.appname", "OK");
                    return;
                }

                // Deshabilitar el botón mientras se verifica
                CheckPaymentBtn.IsEnabled = false;
                CheckPaymentBtn.Text = "Verificando...";

                // Verificar el pago usando la clase ApklisPaymentChecker
                var (isPaid, userName) = _paymentChecker.GetPaymentInfo(packageName);

                // Mostrar los resultados
                DisplayResults(isPaid, userName, packageName);

                // Mostrar el frame de resultados
                ResultFrame.IsVisible = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al verificar el pago:\n{ex.Message}", "OK");
            }
            finally
            {
                // Rehabilitar el botón
                CheckPaymentBtn.IsEnabled = true;
                CheckPaymentBtn.Text = "Verificar Pago";
            }
        }

        private void DisplayResults(bool isPaid, string userName, string packageName)
        {
            // Mostrar estado de pago
            if (isPaid)
            {
                PaymentStatusLabel.Text = "✅ Estado: APLICACIÓN PAGADA";
                PaymentStatusLabel.TextColor = Colors.Green;
                ResultFrame.BackgroundColor = Color.FromArgb("#E8F5E8");
            }
            else
            {
                PaymentStatusLabel.Text = "❌ Estado: APLICACIÓN NO PAGADA";
                PaymentStatusLabel.TextColor = Colors.Red;
                ResultFrame.BackgroundColor = Color.FromArgb("#FFE8E8");
            }

            // Mostrar nombre de usuario
            if (!string.IsNullOrWhiteSpace(userName))
            {
                UserNameLabel.Text = $"👤 Usuario: {userName}";
                UserNameLabel.TextColor = Colors.Black;
                UserNameLabel.IsVisible = true;
            }
            else
            {
                UserNameLabel.Text = "👤 Usuario: No disponible";
                UserNameLabel.TextColor = Colors.Gray;
                UserNameLabel.IsVisible = true;
            }

            // Mostrar paquete verificado
            PackageCheckedLabel.Text = $"📦 Paquete verificado: {packageName}";
        }

        private bool IsValidPackageName(string packageName)
        {
            // Validación básica: debe contener al menos un punto y no estar vacío
            return !string.IsNullOrWhiteSpace(packageName) &&
                   packageName.Contains('.') &&
                   packageName.Length > 3 &&
                   !packageName.StartsWith('.') &&
                   !packageName.EndsWith('.');
        }
    }
}