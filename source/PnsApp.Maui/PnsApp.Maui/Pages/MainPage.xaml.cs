
using DotNet.RestApi.Client;
using PnsApp.Dto;
using PnsApp.Maui.ViewModels;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using PnsApp.Maui.Extension;
using static PnsApp.Maui.Extension.RestApiExtension;

namespace PnsApp.Maui.Pages;

public partial class MainPage : ContentPage
{
    private readonly RestApiClient client;
    public MainPage()
    {
        InitializeComponent();
        client = new RestApiClient(new HttpClient());

        //LoadPageBackground();
    }

    /// <summary>
    /// Metoda pro pridani noveho zakaznika
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Vsup_detailZakaznika(object sender, EventArgs e)
    {
        var page = new DetailZakaznika(null);
        await Navigation.PushAsync(page);
    }

    /// <summary>
    /// Aktualizuje obsah ListView po nacteni stranky
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }
    /// <summary>
    /// metoda, která načte z databáze mssql seznam zákazníků a zobrazí je v listview, případně občerství seznam dle db
    /// </summary>
    private async void LoadData()
    {
        detailZakaznikaListView.ItemsSource = await client.ApiGetAsync<List<ZakaznikDto>>(DotazGet.GetZakaznici);
    }

    /// <summary>
    /// editace
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editButton_Clicked(object sender, EventArgs e)
    {

        var btn = (Button)sender;
        var id = (int)btn.CommandParameter;

        var page = new DetailZakaznika(id);
        await Navigation.PushAsync(page);

    }

    /// <summary>
    /// Smaze zaznam v listu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void smazatButton_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = (int)btn.CommandParameter;
        await client.ApiDeleteAsync(DotazDelete.SmazatZakaznika, id);
        LoadData();
    }

    /// <summary>
    /// Nastavi vybranou barvu pozadi a spusti metodu pro ulozeni nastaveni
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ColorButton_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var color = (string)btn.CommandParameter;

        //this.BackgroundColor = Color.Parse(color);
        //this.SaveCurrentPageBackground();
    }

    /// <summary>
    /// Uklada nastaveni barvy pozadi do aplikace 
    /// </summary>
    private void SaveCurrentPageBackground()
    {
       
    }



    /// <summary>
    /// Nacita barvu pozadi po spusteni aplikace
    /// </summary>
    private void LoadPageBackground()
    {
       
    }




}

