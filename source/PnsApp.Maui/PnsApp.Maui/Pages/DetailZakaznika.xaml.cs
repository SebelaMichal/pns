
using DotNet.RestApi.Client;
using PnsApp.Dto;
using PnsApp.Maui.Extension;
using PnsApp.Maui.ViewModels;
using static PnsApp.Maui.Extension.RestApiExtension;

namespace PnsApp.Maui.Pages;

public partial class DetailZakaznika : ContentPage
{
    /// <summary>
    /// Promena pro model
    /// </summary>
    private DetailZakaznikaViewModel _model;

    private readonly RestApiClient client;

    /// <summary>
    /// Vlastnos pro model
    /// </summary>
    public DetailZakaznikaViewModel Model
    {
        get { return _model; }
        set
        {
            _model = value;
        }
    }

    private int? _dbId = null;
    /// <summary>
    /// Konstruktor DetailuZákazníka
    /// </summary>
    public DetailZakaznika(int? dbId)
    {
        client = new RestApiClient(new HttpClient());
        _dbId = dbId;
        InitializeComponent();

        Nacti(dbId);

    }

    private async void Nacti(int? id = null)
    {
        if (id == null) 
        {
            this.BindingContext = _model = new DetailZakaznikaViewModel();
        }
        else    //editace záznamu
        {
            this.BindingContext = _model = await client.ApiGetAsync<DetailZakaznikaViewModel>(DotazGet.GetZakaznik, id);
            
        }
    }

    /// <summary>
    /// Obnovy data pred nactenim page
    /// </summary>
    protected override void OnAppearing()
    {
        this.BindingContext = _model;
    }


    /// <summary>
    /// Netoda pro ulozeni zakaznika. vyvola udalost OnAddItem a zavre stranku
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ulozitZakaznika_btn_Clicked(object sender, EventArgs e)
    {
       if (_dbId == null)
        {
            await client.ApiPostAsync(DotazPost.PridatZakaznika, _model);
        }
        else
        {
            await client.ApiPutAsync(DotazPut.UpravitZakaznika, _model);
        }
        await Navigation.PopAsync();
    }

    /// <summary>
    /// Metoda pro tlaèitko zavrit, pouze se zepta jestli chcete opustit bez ulozeni
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void detailZakaznika_btn_Clicked(object sender, EventArgs e)
    {
        if (await DisplayAlert("Upozornìní", "Vážnì chcete opustit stránku bez uložení?", "Ano", "Ne"))
        {
            await Navigation.PopAsync();
        }
    }

    /// <summary>
    /// Metoda vyvola event pro smazani OnDeleted a zavre akno
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void detailZakaznikaSmazat_Clicked(object sender, EventArgs e)
    {
        await client.ApiDeleteAsync(DotazDelete.SmazatZakaznika, _model.Id);
        await Navigation.PopAsync();
    }

   

   

  
}

/// <summary>
///  Vlastní argumenty pro event
/// </summary>
public class ItemEventArgs : EventArgs
{
    public DetailZakaznikaViewModel Model { get; set; }
}

/// <summary>
/// Deklarace pro ItemeventHandler - musi byt, jinak nebude fungovat
/// </summary>
/// <param name="sender"></param>
/// <param name="iea"></param>
public delegate void ItemEventHandler(object sender, ItemEventArgs iea);