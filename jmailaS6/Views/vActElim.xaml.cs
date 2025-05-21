using System.Text;
using jmailaS6.Models;
using Newtonsoft.Json;


namespace jmailaS6.Views;

public partial class vActElim : ContentPage
{
	public vActElim(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.Codigo.ToString();
        txtNombre.Text = datos.Nombre;
        txtApellido.Text = datos.Apellido;
        txtEdad.Text = datos.Edad.ToString();

	}

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out int codigo) ||
         string.IsNullOrWhiteSpace(txtNombre.Text) ||
         string.IsNullOrWhiteSpace(txtApellido.Text) ||
         !int.TryParse(txtEdad.Text, out int edad))
        {
            await DisplayAlert("Error", "Verifica que todos los campos est�n correctamente llenos.", "OK");
            return;
        }

        var estudianteActualizado = new Estudiante
        {
            Codigo = codigo,
            Nombre = txtNombre.Text,
            Apellido = txtApellido.Text,
            Edad = edad
        };

        var json = JsonConvert.SerializeObject(estudianteActualizado);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpClient client = new HttpClient();
        var response = await client.PutAsync($"http://localhost:5250/api/estudiante/{codigo}", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("�xito", "Estudiante actualizado correctamente.", "OK");
            await Navigation.PushAsync(new vCrud());
        }
        else
        {
            await DisplayAlert("Error", $"No se pudo actualizar. C�digo: {(int)response.StatusCode}", "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out int codigo))
        {
            await DisplayAlert("Error", "C�digo inv�lido.", "OK");
            return;
        }

        bool confirmacion = await DisplayAlert("Confirmar", "�Est�s seguro que deseas eliminar este estudiante?", "S�", "No");
        if (!confirmacion) return;

        HttpClient client = new HttpClient();
        var response = await client.DeleteAsync($"http://localhost:5250/api/estudiante/{codigo}");

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("�xito", "Estudiante eliminado correctamente.", "OK");
            await Navigation.PushAsync(new vCrud());
        }
        else
        {
            await DisplayAlert("Error", $"No se pudo eliminar. C�digo: {(int)response.StatusCode}", "OK");
        }
    }
}