using SoapCore;
using SoapCore_Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorService, AuthorService>();

var app = builder.Build();

app.UseSoapEndpoint<IAuthorService>("/Service.asmx", new SoapEncoderOptions());


app.Urls.Add("http://localhost:5000");

app.MapGet("/", () => "Hello World!");

app.MapGet("/{cityName}/weather", GetWeatherByCity);

app.Run();


Weather GetWeatherByCity(string cityName)
{
    app.Logger.LogInformation($"Weather requested for {cityName}.");
    var weather = new Weather(cityName);
    return weather;
}

public record Weather
{
    public string City { get; set; }

    public Weather(string city)
    {
        City = city;
        Conditions = "Cloudy";
        // Temperature here is in celsius degrees, hence the 0-40 range.
        Temperature = new Random().Next(0,40).ToString();
    }

    public string Conditions { get; set; }
    public string Temperature { get; set; }
}