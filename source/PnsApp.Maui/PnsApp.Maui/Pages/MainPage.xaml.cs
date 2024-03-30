
using DotNet.RestApi.Client;
using Newtonsoft.Json;
using PnsApp.Dto;
using PnsApp.Maui.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using PnsApp.Maui.Extension;
using static PnsApp.Maui.Extension.RestApiExtension;
using System;

namespace PnsApp.Maui.Pages;

public partial class MainPage : ContentPage
{
    private readonly RestApiClient client;
    Random random = new Random();
    private bool Disko { get; set; } = false;
    public MainPage()
    {
        InitializeComponent();
        client = new RestApiClient(new HttpClient());
        
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
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await LoadData();
            DiskoParty();
            LoadPageBackground();
        }
        catch (Exception ex)
        {

        }
    }

    IDispatcherTimer timer;

    private void DiskoParty()
    {
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(10);
        timer.Tick += (sender, e) =>
        {
            string color = String.Format("#{0:X6}", random.Next(0x1000000));
            this.BackgroundColor = Color.Parse(color);
        };
        
    }


    /// <summary>
    /// metoda, která načte z databáze mssql seznam zákazníků a zobrazí je v listview, případně občerství seznam dle db
    /// </summary>
    private async Task LoadData()
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
        await LoadData();
    }

    /// <summary>
    /// Nastavi vybranou barvu pozadi a spusti metodu pro ulozeni nastaveni
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ColorButton_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var color = btn.CommandParameter;
        

        //generator nahodnych barev

       
        this.SaveCurrentPageBackground(int.Parse(color.ToString()));
    }

    /// <summary>
    /// Uklada nastaveni barvy pozadi do aplikace 
    /// </summary>
    private async void SaveCurrentPageBackground(int color)
    {
        await client.ApiPutAsync(DotazPut.UpravitBarvu, color, false);
        LoadPageBackground();
    }



    /// <summary>
    /// Nacita barvu pozadi po spusteni aplikace
    /// </summary>
    private async void LoadPageBackground()
    {
        var barvaId = await client.ApiGetAsync<int>(DotazGet.NacistBarvu);

        switch(barvaId)
        {
            case 1:
                this.BackgroundColor = Color.Parse("#F88091");
                break;
            case 2:
                this.BackgroundColor = Color.Parse("#41C27A");
                break;
            case 3:
                this.BackgroundColor = Color.Parse("#A1A8F6");
                break;
            case 4:
                this.BackgroundColor = Colors.White;
                break;
            default:
                this.BackgroundColor = Colors.White;
                break;
        }
    }

    private void DiscoSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            timer.Start();
        }
        else
        {
            timer.Stop();
            //Task.Delay(1000);
            LoadPageBackground();
        }
    }
}

