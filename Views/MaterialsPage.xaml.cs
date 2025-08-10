using Pomocnik.Data;
using Pomocnik.Models;

namespace Pomocnik.Views;

public partial class MaterialsPage : ContentPage
{
    private readonly AppDatabase _db;

    public MaterialsPage(AppDatabase db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Jednorazowy seed przy pierwszym starcie
        await _db.SeedAsync();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var items = await _db.GetMaterialsAsync();
        MaterialsList.ItemsSource = items;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MaterialEditPage(_db)); // przejdziemy do formularza
    }

    private async void OnEditInvoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipe && swipe.BindingContext is Material m)
            await Navigation.PushAsync(new MaterialEditPage(_db, m));
    }

    private async void OnDeleteInvoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipe && swipe.BindingContext is Material m)
        {
            var ok = await DisplayAlert("Usuñ", $"Usun¹æ '{m.Name}'?", "Tak", "Nie");
            if (ok)
            {
                await _db.DeleteMaterialAsync(m);
                await LoadAsync();
            }
        }
    }
}
