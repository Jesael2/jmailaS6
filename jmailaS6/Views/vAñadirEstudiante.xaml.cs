using System.Net;
using Newtonsoft.Json;
using System.Text;
using jmailaS6.Models;

namespace jmailaS6.Views;

public partial class vAñadirEstudiante : ContentPage
{
	public vAñadirEstudiante()
	{
		InitializeComponent();
	}

    private async void btnAgregarEstudiante_Clicked(object sender, EventArgs e)
    {
        try
        {
            var nuevoEstudiante = new Estudiante
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Edad = int.Parse(txtEdad.Text) // Asegúrate de validar el input
            };

            string json = JsonConvert.SerializeObject(nuevoEstudiante);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5250/api/estudiante", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante agregado correctamente", "OK");
                await Navigation.PushAsync(new vCrud());
            }
            else
            {
                await DisplayAlert("Error", $"Código: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }
}