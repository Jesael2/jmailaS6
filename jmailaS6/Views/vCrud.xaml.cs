using System.Collections.ObjectModel;
using jmailaS6.Models;
using Newtonsoft.Json;

namespace jmailaS6.Views;

public partial class vCrud : ContentPage
{

    private const string URL = "http://localhost:5250/api/estudiante"; 
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiante> _estudianteTem;

    public vCrud()
    {
        InitializeComponent();
        mostrarEstudiante();
    }

    public async void mostrarEstudiante()
    {
        try
        {
            var conten = await cliente.GetStringAsync(URL);
            var lista = JsonConvert.DeserializeObject<List<Estudiante>>(conten);

            _estudianteTem = new ObservableCollection<Estudiante>(lista);
            lvEstudiantes.ItemsSource = _estudianteTem;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener la lista de estudiantes.\n{ex.Message}", "OK");
        }
    }

    private  void Button_Clicked(object sender, EventArgs e)
    {
         mostrarEstudiante();
    }
}
