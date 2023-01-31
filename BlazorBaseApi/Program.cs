using AutoMapper;
using BlazorBaseApi;
using BlazorBaseApi.Hubs;
using Microsoft.EntityFrameworkCore;
using BlazorBaseModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MysqlDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
builder.Services.AddCors(policy =>
    {
        policy.AddPolicy("CorsPolicy", opt => opt
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });
// Auto Mapper Configurations
// var mapperConfig = new MapperConfiguration(mc =>
// {
//     mc.AddUser(new BlazorBaseModel.Model.User());
// });
// IMapper mapper = mapperConfig.CreateMapper();
// builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// app.MapHub<AppHub>();
app.Run();
