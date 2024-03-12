using PnsApp.Maui.Data;
using PnsApp.Maui.ViewModels;

namespace PnsApp.Maui.Pages;

public partial class DetailZakaznika : ContentPage
{
	/// <summary>
	/// Promena pro model
	/// </summary>
	private DetailZakaznikaViewModel _model;
	
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

	/// <summary>
	/// Konstruktor DetailuZ�kazn�ka
	/// </summary>
	public DetailZakaznika()
	{
		_model = new DetailZakaznikaViewModel();
		InitializeComponent();

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
	private void ulozitZakaznika_btn_Clicked(object sender, EventArgs e)
    {
		AppDbContextFactory factory = new AppDbContextFactory();
		using (var db = factory.CreateDbContext(null))
		{
			db.Zakaznik.Add(new Zakaznik() { Jmeno = _model.Jmeno, Prijmeni = _model.Prijmeni, Email = _model.Email, Telefon = _model.Telefon });
			db.SaveChanges();
		}

		OnAddedItem(new ItemEventArgs() { Model = _model });
		Navigation.PopAsync();
    }

	/// <summary>
	/// Metoda pro tla�itko zavrit, pouze se zepta jestli chcete opustit bez ulozeni
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private async void detailZakaznika_btn_Clicked(object sender, EventArgs e)
    {
		if (await DisplayAlert("Upozorn�n�", "V�n� chcete opustit str�nku bez ulo�en�?", "Ano", "Ne"))
		{
			await Navigation.PopAsync();
		}
    }

   /// <summary>
   /// Metoda vyvola event pro smazani OnDeleted a zavre akno
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    private void detailZakaznikaSmazat_Clicked(object sender, EventArgs e)
    {
		OnDeleted(new ItemEventArgs() { Model = _model });
		Navigation.PopAsync();
    }

	/// <summary>
	/// Deklarace pro udalost Deleted
	/// </summary>
	public event ItemEventHandler Deleted;

	/// <summary>
	/// Metoda vyvolava udalost Deleted
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnDeleted(ItemEventArgs e)
	{
		Deleted?.Invoke(this, e);
	}

	/// <summary>
	/// Deklarace pro udalost AddItem
	/// </summary>
	public event ItemEventHandler AddedItem;

	/// <summary>
	/// Metoda vyvolava udalost AddedItem
	/// </summary>
	/// <param name="ea"></param>
	protected virtual void OnAddedItem(ItemEventArgs ea)
	{
		AddedItem?.Invoke(this, ea);
	}
}

/// <summary>
///  Vlastn� argumenty pro event
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