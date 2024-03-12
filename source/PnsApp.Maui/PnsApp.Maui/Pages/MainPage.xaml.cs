﻿
using PnsApp.Maui.ViewModels;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PnsApp.Maui.Pages;

public partial class MainPage : ContentPage
{
    public List<DetailZakaznikaViewModel> Zakaznici { get; set; }
    
    public MainPage()
	{
        InitializeComponent();
        Zakaznici = new List<DetailZakaznikaViewModel>();
        LoadPageBackground();
    }


    /// <summary>
    /// Metoda pro pridani noveho zakaznika
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Vsup_detailZakaznika(object sender, EventArgs e)
    {
        var page = new DetailZakaznika();
        page.AddedItem += delegate(object sender, ItemEventArgs iea)
        {
            this.Zakaznici.Add(iea.Model);
        };
        await Navigation.PushAsync(page);
    }

    /// <summary>
    /// Aktualizuje obsah ListView po nacteni stranky
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        detailZakaznikaListView.ItemsSource = null;
        detailZakaznikaListView.ItemsSource = Zakaznici;
    }

    /// <summary>
    /// editace
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editButton_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = (Guid)btn.CommandParameter;
        var zakaznik = Zakaznici.Single(x => x.Id == id);

        var page = new DetailZakaznika();
        // prida dalegata detailu, bude vyvolan pouze v pripade pokud dojde k udalosti OnDelete na strance detailu
        page.Deleted += delegate(object sender, ItemEventArgs ea)
        {
            Zakaznici.Remove(ea.Model);
        };
       
        page.Model = zakaznik;
        await Navigation.PushAsync(page);
    }

    /// <summary>
    /// Smaze zaznam v listu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void smazatButton_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = (Guid)btn.CommandParameter;
        var zakaznik = Zakaznici.Single(x => x.Id == id);
        Zakaznici.Remove(zakaznik);
        OnAppearing(); 

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

        this.BackgroundColor = Color.Parse(color);
        this.SaveCurrentPageBackground();
    }

    /// <summary>
    /// Uklada nastaveni barvy pozadi do aplikace 
    /// </summary>
    private void SaveCurrentPageBackground()
    {
        Preferences.Set("pb", this.BackgroundColor.ToHex());
    }

   

    /// <summary>
    /// Nacita barvu pozadi po spusteni aplikace
    /// </summary>
    private void LoadPageBackground()
    {
        //this.BackgroundColor = Color.Parse(Preferences.Get("pb", this.BackgroundColor.ToHex()));

        string strColor = Preferences.Get("pb", null);
        if (strColor != null)
        {
            this.BackgroundColor = Color.Parse(strColor);
        }
    }

  


}

