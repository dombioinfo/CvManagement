using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using BlazorBaseModel;
using BlazorBaseModel.Db;

namespace BlazorBase.Pages
{
    public partial class FetchDataPage : ComponentBase
    {
        private string PageTitle { get; set; } = "Fetch Data";
        
        [Inject]
        protected WeatherForecastService ForecastService { get; set; } = default!;

        private IEnumerable<WeatherForecast> forecasts { get ; set; } = default!;
        
        private WeatherForecast forecast { get ; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
            forecast = await ForecastService.GetForecastByIdAsync(1);
        }
    }
}

