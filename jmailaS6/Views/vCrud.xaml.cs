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
        var content = await cliente.GetStringAsync(URL);
        List<Estudiante> lista = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estudianteTem = new ObservableCollection<Estudiante>(lista);
        lvEstudiantes.ItemsSource = _estudianteTem;
    }

    private  void Button_Clicked(object sender, EventArgs e)
    {
         mostrarEstudiante();
    }

    private void btnAñadir_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vAñadirEstudiante());
    }

    private void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new vActElim(objEstudiante));
    }
}
