using Pomocnik.Data;
using Pomocnik.Models;
using System.Globalization;

namespace Pomocnik.Views;

public partial class MaterialEditPage : ContentPage
{
    private readonly AppDatabase _db;
    private readonly Material _model;

    public MaterialEditPage(AppDatabase db, Material? model = null)
    {
        InitializeComponent();
        _db = db;
        _model = model ?? new Material();

        // Pre-fill
        NameEntry.Text = _model.Name;
        CategoryEntry.Text = _model.Category;
        UnitPicker.SelectedItem = string.IsNullOrWhiteSpace(_model.Unit) ? "mb" : _model.Unit;
        StdLenEntry.Text = _model.StdLengthM?.ToString(CultureInfo.InvariantCulture);
        PriceEntry.Text = (_model.PriceNetCents / 100.0).ToString(CultureInfo.InvariantCulture);
        VatEntry.Text = _model.VatRatePct.ToString(CultureInfo.InvariantCulture);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("B³¹d", "Podaj nazwê materia³u.", "OK");
            return;
        }

        _model.Name = NameEntry.Text!.Trim();
        _model.Category = string.IsNullOrWhiteSpace(CategoryEntry.Text) ? "profile" : CategoryEntry.Text!.Trim();
        _model.Unit = (string?)UnitPicker.SelectedItem ?? "mb";

        if (double.TryParse(StdLenEntry.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var stdLen))
            _model.StdLengthM = stdLen;
        else
            _model.StdLengthM = null;

        if (!double.TryParse(PriceEntry.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var pricePln))
        {
            await DisplayAlert("B³¹d", "Nieprawid³owa cena.", "OK");
            return;
        }
        _model.PriceNetCents = (int)Math.Round(pricePln * 100);

        if (!int.TryParse(VatEntry.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var vat))
        {
            await DisplayAlert("B³¹d", "Nieprawid³owy VAT.", "OK");
            return;
        }
        _model.VatRatePct = vat;

        await _db.SaveMaterialAsync(_model);
        await Navigation.PopAsync(); // wróæ do listy
    }
}
