using System.Threading;
using System.Timers;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Pages;
public partial class CounterPage : ComponentBase
{
    public string PageTitle { get; set; } = "Counter";
    public int currentCount { get; set; } = 0;
    private System.Timers.Timer time { get; set; } = default!;

    private void IncrementCount()
    {
        currentCount++;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            time = new System.Timers.Timer();
            //Set the time interval.
            time.Interval = 1000;
            time.Elapsed += OnTimeInterval;
            time.AutoReset = true;
            // Start the timer.
            time.Enabled = true;
        }
        base.OnAfterRender(firstRender);
    }

    private async void OnTimeInterval(object sender, ElapsedEventArgs e)
    {
        IncrementCount();
        await InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        // While navigating to other components, Dispose method will be called and clean up the Timer function.
        time?.Dispose();
    }
}
