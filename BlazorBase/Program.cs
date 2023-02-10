using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorBase.Service;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorBaseModel.Model;
using AutoMapper;
using BlazorBaseModel.Db;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services.AddHttpClient();
// builder.Services.AddSingleton<HttpClient>(httpClient => new HttpClient()
// {
//     BaseAddress = new Uri("https://api:7031/api")
// });

builder.Services.AddHttpClient("HttpClientWithSSLUntrusted", c =>
{
    c.BaseAddress = new Uri("https://localhost:7031");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    ServerCertificateCustomValidationCallback =
        (httpRequestMessage, cert, cetChain, policyErrors) =>
        {
            return true;
        }
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

// builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddSingleton<GenericObjectService<ProfilDto>>();
builder.Services.AddSingleton<PersonneService>();
builder.Services.AddSingleton<AdresseService>();
builder.Services.AddSingleton<CandidatureService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
