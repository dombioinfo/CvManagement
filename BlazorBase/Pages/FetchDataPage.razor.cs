using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using BlazorBaseModel;
using BlazorBaseModel.Model;

namespace BlazorBase.Pages
{
    public partial class FetchDataPage : ComponentBase
    {
        private string PageTitle { get; set; } = "Fetch Data";

        [Inject]
        protected GenericObjectService<UserDto> UserService { get; set; } = default!;
        private IEnumerable<UserDto> users { get; set; } = default!;


        // private WeatherForecast forecast { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            // forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
            // forecast = await ForecastService.GetForecastByIdAsync(1);

            users = await UserService.GetGenericObjectListAsync();
        }
    }
}

