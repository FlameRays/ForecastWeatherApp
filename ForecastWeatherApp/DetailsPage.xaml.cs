namespace ForecastWeatherApp;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(WeatherItem weatherItem)
	{
		InitializeComponent();
        BindingContext = weatherItem;
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}