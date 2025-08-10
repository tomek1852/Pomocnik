namespace Pomocnik;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Używamy Shell — nie owijamy go w NavigationPage
        MainPage = new AppShell();
    }
}
