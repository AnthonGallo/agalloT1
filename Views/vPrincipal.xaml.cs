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
                DisplayAlert("Error", "La identificaci�n debe contener solo n�meros.", "OK");
                return;
            }

            if (txtNombres.Text.Any(char.IsDigit) || txtApellidos.Text.Any(char.IsDigit))
            {
                DisplayAlert("Error", "Nombres y apellidos no deben contener n�meros.", "OK");
                return;
            }

            if (txtTelefono.Text.Length != 10 || !txtTelefono.Text.All(char.IsDigit))
            {
                DisplayAlert("Error", "El n�mero de tel�fono debe tener exactamente 10 d�gitos num�ricos.", "OK");
                return;
            }

            if (!IsValidEmail(txtCorreo.Text))
            {
                DisplayAlert("Error", "El correo electr�nico no es v�lido.", "OK");
                return;
            }

            if (txtCorreo.Text != txtCorreoConfirmar.Text)
            {
                DisplayAlert("Error", "Los correos electr�nicos no coinciden.", "OK");
                return;
            }

            DisplayAlert("�xito", "Formulario enviado correctamente.", "OK");

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