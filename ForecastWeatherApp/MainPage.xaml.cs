
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ForecastWeatherApp;

public partial class MainPage : ContentPage
{
    private const string apiKey = "b7f0c8571amsh85a169f6a469054p1aa249jsn02b58b9acd9c";
    private const string apiUrl = "https://weatherapi-com.p.rapidapi.com/forecast.json";
    private const int days = 7;

    public List<WeatherItem> WeatherForecast { get; set; }

    public MainPage()
    {
        InitializeComponent();
        WeatherForecast = new List<WeatherItem>();
        WeatherListView.ItemsSource = WeatherForecast;
    }

    private async void GetWeatherButton_Clicked(object sender, EventArgs e)
    {
        string location = LocationEntry.Text;

        if (!string.IsNullOrWhiteSpace(location))
        {
            WeatherForecast = await GetWeatherForecast(location);
            WeatherListView.ItemsSource = WeatherForecast;
        }
    }

    private async Task<List<WeatherItem>> GetWeatherForecast(string location)
    {
        try
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiUrl}?q={location}&days={days}");

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var root = JsonConvert.DeserializeObject<Root>(body);
                List<WeatherItem> weatherForecast = new List<WeatherItem>();

                foreach (var forecastday in root.forecast.forecastday)
                {
                    WeatherItem weatherItem = new WeatherItem
                    {
                        Date = forecastday.date,
                        MinTemperatureC = forecastday.day.mintemp_c,
                        MaxTemperatureC = forecastday.day.maxtemp_c,
                        MinTemperatureF = forecastday.day.mintemp_f,
                        MaxTemperatureF = forecastday.day.maxtemp_f
                    };

                    weatherForecast.Add(weatherItem);
                }

                return weatherForecast;
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request exceptions
            await DisplayAlert("Error", $"HTTP request exception: {ex.Message}", "OK");
        }
        catch (JsonException ex)
        {
            // Handle JSON deserialization exceptions
            await DisplayAlert("Error", $"JSON deserialization exception: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            // Handle other general exceptions
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }

        return null; // Return null or an empty list to indicate failure
    }

    
    private void WeatherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is WeatherItem weatherItem)
        {
            Navigation.PushAsync(new DetailsPage(weatherItem));
        }

     ((CollectionView)sender).SelectedItem = null;
    }
}

public class WeatherItem
{
    public string Date { get; set; }
    public double MinTemperatureC { get; set; }
    public double MaxTemperatureC { get; set; }
    public double MinTemperatureF { get; set; }
    public double MaxTemperatureF { get; set; }
}

public class Astro
{
    public string sunrise { get; set; }
    public string sunset { get; set; }
    public string moonrise { get; set; }
    public string moonset { get; set; }
    public string moon_phase { get; set; }
    public string moon_illumination { get; set; }
    public int is_moon_up { get; set; }
    public int is_sun_up { get; set; }
}

public class Condition
{
    public string text { get; set; }
    public string icon { get; set; }
    public int code { get; set; }
}

public class Current
{
    public int last_updated_epoch { get; set; }
    public string last_updated { get; set; }
    public double temp_c { get; set; }
    public double temp_f { get; set; }

    public Condition condition { get; set; }
    
    public int humidity { get; set; }
  
}

public class Day
{
    public double maxtemp_c { get; set; }
    public double maxtemp_f { get; set; }
    public double mintemp_c { get; set; }
    public double mintemp_f { get; set; }

    public Condition condition { get; set; }
   
}

public class Forecast
{
    public List<Forecastday> forecastday { get; set; }
}

public class Forecastday
{
    public string date { get; set; }
    public int date_epoch { get; set; }
    public Day day { get; set; }
    public Astro astro { get; set; }
    public List<Hour> hour { get; set; }
}

public class Hour
{
    public int time_epoch { get; set; }
    public string time { get; set; }
    public double temp_c { get; set; }
    public double temp_f { get; set; }
   
    public Condition condition { get; set; }
  
    public int humidity { get; set; }
    public int cloud { get; set; }
    
}

public class Location
{
    public string name { get; set; }
    public string region { get; set; }
    public string country { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    
   
}

public class Root
{
    public Location location { get; set; }
    public Current current { get; set; }
    public Forecast forecast { get; set; }
}
