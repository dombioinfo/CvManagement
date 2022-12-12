namespace BlazorBaseModel.Db
{
    public class WeatherForecast : GenericObject
    {
        public WeatherForecast() : base()
        {
            Date = DateTime.Now;
            TemperatureC = 20;
            Summary = "Fait pas chaud";
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
