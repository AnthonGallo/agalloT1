namespace agalloT1.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnEnviar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoConfirmar.Text))
            {
                DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            if (!txtIdentificacion.Text.All(char.IsDigit))
            {
                DisplayAlert("Error", "La identificación debe contener solo números.", "OK");
                return;
            }

            if (txtNombres.Text.Any(char.IsDigit) || txtApellidos.Text.Any(char.IsDigit))
            {
                DisplayAlert("Error", "Nombres y apellidos no deben contener números.", "OK");
                return;
            }

            if (txtTelefono.Text.Length != 10 || !txtTelefono.Text.All(char.IsDigit))
            {
                DisplayAlert("Error", "El número de teléfono debe tener exactamente 10 dígitos numéricos.", "OK");
                return;
            }

            if (!IsValidEmail(txtCorreo.Text))
            {
                DisplayAlert("Error", "El correo electrónico no es válido.", "OK");
                return;
            }

            if (txtCorreo.Text != txtCorreoConfirmar.Text)
            {
                DisplayAlert("Error", "Los correos electrónicos no coinciden.", "OK");
                return;
            }

            DisplayAlert("Éxito", "Formulario enviado correctamente.", "OK");

        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Ha ocurrido un error: " + ex.Message, "OK");
        }
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}