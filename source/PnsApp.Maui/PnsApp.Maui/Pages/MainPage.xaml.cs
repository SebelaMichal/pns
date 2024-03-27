
using PnsApp.Dto;
using PnsApp.Maui.ViewModels;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PnsApp.Maui.Pages;

public partial class MainPage : ContentPage
{
    //public List<DetailZakaznikaViewModel> Zakaznici { get; set; }
    
    public MainPage()
	{
        InitializeComponent();
        //Zakaznici = new List<DetailZakaznikaViewModel>();
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
        page.AddedItem += delegate(object sender, ItemEventArgs iea)
        {
            //this.Zakaznici.Add(iea.Model);
        };
        await Navigation.PushAsync(page);
    }

    /// <summary>
    /// Aktualizuje obsah ListView po nacteni stranky
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //detailZakaznikaListView.ItemsSource = null;
        //detailZakaznikaListView.ItemsSource = Zakaznici;

        LoadData();
    }
    /// <summary>
    /// metoda, která načte z databáze mssql seznam zákazníků a zobrazí je v listview, případně občerství seznam dle db
    /// </summary>
    private void LoadData()
    {
        /*
        AppDbContextFactory factory = new AppDbContextFactory();
        using (var db = factory.CreateDbContext(null))
        {
            detailZakaznikaListView.ItemsSource = ZakaznikMapper.ToViewModel(db.Zakaznik).ToList();
        }*/

        //nacteni dat z webapi
        var client = new HttpClient();
        var response = client.GetAsync("https://localhost:7187/Pns/GetZakaznici").Result;
        var json = response.Content.ReadAsStringAsync().Result;
        Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();
        var zakaznik = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DetailZakaznikaViewModel>>(json);

        //var zakaznici = JsonSerializer.Deserialize<List<DetailZakaznikaViewModel>>(json);
        detailZakaznikaListView.ItemsSource = zakaznik;



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

        //todo: loaddata
    }

    /// <summary>
    /// Smaze zaznam v listu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void smazatButton_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = (int)btn.CommandParameter;

        //AppDbContextFactory factory = new AppDbContextFactory();
        //using (var db = factory.CreateDbContext(null))
        //{
        //    var dbZaznamZakaznika = db.Zakaznik.Find(id);
        //    db.Zakaznik.Remove(dbZaznamZakaznika);
        //    db.SaveChanges();
        //}

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

        this.BackgroundColor = Color.Parse(color);
        this.SaveCurrentPageBackground();
    }

    /// <summary>
    /// Uklada nastaveni barvy pozadi do aplikace 
    /// </summary>
    private void SaveCurrentPageBackground()
    {
        //Preferences.Set("pb", this.BackgroundColor.ToHex());

        //AppDbContextFactory factory = new AppDbContextFactory();
        //using (var db = factory.CreateDbContext(null))
        //{
        //    BarvaPozadi bp;
        //    if (this.BackgroundColor.ToHex() == "#00FF00")
        //        bp = BarvaPozadi.Zelena;
        //    else if (this.BackgroundColor.ToHex() == "#FF0000")
        //        bp = BarvaPozadi.Cervena;
        //    else
        //        bp = BarvaPozadi.Modra;

        //    var pozadiDb = db.Pozadi.SingleOrDefault();

        //    if(pozadiDb == null)
        //    {
        //        pozadiDb = new Pozadi() { BarvaPozadi = bp };
        //        db.Pozadi.Add(pozadiDb);
        //    }
        //    else
        //    {
        //        pozadiDb.BarvaPozadi = bp;
        //    }

        //    db.SaveChanges();
        //}
    }

   

    /// <summary>
    /// Nacita barvu pozadi po spusteni aplikace
    /// </summary>
    private void LoadPageBackground()
    {
        //this.BackgroundColor = Color.Parse(Preferences.Get("pb", this.BackgroundColor.ToHex()));

        /*
        string strColor = Preferences.Get("pb", null);
        if (strColor != null)
        {
            this.BackgroundColor = Color.Parse(strColor);
        }*/


        //AppDbContextFactory factory = new AppDbContextFactory();
        //using (var db = factory.CreateDbContext(null))
        //{
        //    var barva = (db.Pozadi.SingleOrDefault() ?? new Pozadi() { BarvaPozadi = BarvaPozadi.Cervena }).BarvaPozadi;

        //    switch (barva)
        //    {
        //        case BarvaPozadi.Cervena:
        //            this.BackgroundColor = Color.FromHex("#FF0000");
        //            break;

        //        case BarvaPozadi.Modra:
        //            this.BackgroundColor = Color.FromHex("#0000FF");
        //            break;

        //        case BarvaPozadi.Zelena:
        //            this.BackgroundColor = Color.FromHex("#00FF00");
        //            break;

        //    }
        //}
    }

  


}

