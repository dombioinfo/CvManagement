namespace BlazorBaseModel;

public class WeatherForecast
{
    public WeatherForecast() {
        Date = DateTime.Now;
        TemperatureC = 20;
        Summary = "Fait pas chaud";
    }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
