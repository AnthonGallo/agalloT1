namespace agalloT1.Views;

public partial class vPrincipal : ContentPage
{
    private Dictionary<string, string> formData = new Dictionary<string, string>();

    public vPrincipal()
    {
        InitializeComponent();
    }

    private async void btnEnviar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateForm())
                return;

            formData["Identificación"] = txtIdentificacion.Text;
            formData["Apellidos"] = txtApellidos.Text;
            formData["Nombres"] = txtNombres.Text;
            formData["Teléfono"] = txtTelefono.Text;
            formData["Correo Electrónico"] = txtCorreo.Text;
            formData["Carrera"] = txtCarrera.Text;
            formData["Modalidad"] = txtModalidad.Text;
            formData["Campus"] = txtCampus.Text;

            await DisplayAlert("Éxito", "Formulario enviado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Ha ocurrido un error: " + ex.Message, "OK");
        }
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (formData.Count == 0)
            {
                await DisplayAlert("Advertencia", "No hay datos para exportar. Envíe el formulario primero.", "OK");
                return;
            }

            string contenido = "UNIVERSIDAD ISRAEL\n";
            contenido += "FORMULARIO DE INSCRIPCIÓN\n";
            contenido += $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n";

            contenido += "=== DATOS PERSONALES ===\n";
            contenido += $"Identificación: {formData["Identificación"]}\n";
            contenido += $"Apellidos: {formData["Apellidos"]}\n";
            contenido += $"Nombres: {formData["Nombres"]}\n";
            contenido += $"Teléfono: {formData["Teléfono"]}\n";
            contenido += $"Correo: {formData["Correo Electrónico"]}\n\n";

            contenido += "=== DATOS ACADÉMICOS ===\n";
            contenido += $"Carrera: {formData["Carrera"]}\n";
            contenido += $"Modalidad: {formData["Modalidad"]}\n";
            contenido += $"Campus: {formData["Campus"]}\n\n";

            contenido += $"Exportado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            string fileName = $"Inscripcion_{formData["Identificación"]}_{DateTime.Now:yyyyMMddHHmmss}.txt";

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderPath, fileName);

            await File.WriteAllTextAsync(filePath, contenido);

            bool share = await DisplayAlert("Éxito",
                $"Datos exportados correctamente en:\n{filePath}",
                "Compartir", "Abrir ubicación");

            if (share)
            {
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Exportar Formulario de Inscripción",
                    File = new ShareFile(filePath)
                });
            }
            else
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al exportar: {ex.Message}", "OK");
        }
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
            string.IsNullOrWhiteSpace(txtNombres.Text) ||
            string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtTelefono.Text) ||
            string.IsNullOrWhiteSpace(txtCorreo.Text) ||
            string.IsNullOrWhiteSpace(txtCorreoConfirmar.Text) ||
            string.IsNullOrWhiteSpace(txtCarrera.Text) ||
            string.IsNullOrWhiteSpace(txtModalidad.Text) ||
            string.IsNullOrWhiteSpace(txtCampus.Text))
        {
            DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
            return false;
        }

        if (txtIdentificacion.Text.Length > 13 || !txtIdentificacion.Text.All(char.IsDigit))
        {
            DisplayAlert("Error", "La identificación debe contener solo números hasta 13.", "OK");
            return false;
        }

        if (txtNombres.Text.Any(char.IsDigit) || txtApellidos.Text.Any(char.IsDigit))
        {
            DisplayAlert("Error", "Nombres y apellidos no deben contener números.", "OK");
            return false;
        }

        if (txtCarrera.Text.Any(char.IsDigit) || txtModalidad.Text.Any(char.IsDigit) || txtCampus.Text.Any(char.IsDigit))
        {
            DisplayAlert("Error", "Carrera, Modalidad y Campus no deben contener números.", "OK");
            return false;
        }

        if (txtTelefono.Text.Length != 10 || !txtTelefono.Text.All(char.IsDigit))
        {
            DisplayAlert("Error", "El número de teléfono debe tener exactamente 10 dígitos numéricos.", "OK");
            return false;
        }

        if (!IsValidEmail(txtCorreo.Text))
        {
            DisplayAlert("Error", "El correo electrónico no es válido.", "OK");
            return false;
        }

        if (txtCorreo.Text != txtCorreoConfirmar.Text)
        {
            DisplayAlert("Error", "Los correos electrónicos no coinciden.", "OK");
            return false;
        }

        return true;
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