
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

	private int? _dbId = null;
	/// <summary>
	/// Konstruktor DetailuZákazníka
	/// </summary>
	public DetailZakaznika(int? dbId)
	{
		InitializeComponent();

		//if (dbId == null)   //nový záznam
		//{
		//	this.BindingContext = _model = new DetailZakaznikaViewModel();
  //      }
		//else	//editace záznamu
		//{
		//	AppDbContextFactory factory = new AppDbContextFactory();
		//	using (var db = factory.CreateDbContext(null))
		//	{
		//		this.BindingContext = _model = ZakaznikMapper.ToViewModel(db.Zakaznik.Where(zakaznik => zakaznik.Id == dbId.Value)).Single();
		//	}
		//}
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
		//AppDbContextFactory factory = new AppDbContextFactory();
		//using (var db = factory.CreateDbContext(null))
		//{
		//	if (this._dbId == null)
		//	{
		//		var zak = ZakaznikMapper.ToEntity(Model);
		//		db.Zakaznik.Add(zak);
		//	}
		//	else
		//	{
		//		var zak = db.Zakaznik.Find(_dbId.Value);
		//		ZakaznikMapper.ToEntity(Model, zak);
		//	}
		//	db.SaveChanges();
		//}

		//OnAddedItem(new ItemEventArgs() { Model = _model });
		//Navigation.PopAsync();
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