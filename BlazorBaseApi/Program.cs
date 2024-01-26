using AutoMapper;
using BlazorBaseApi;
using BlazorBaseApi.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
builder.Services.AddDbContext<MysqlDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
*/
builder.Services.AddDbContext<SqliteDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
        //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        options.UseSqlite(connectionString);
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
//     mc.CreateMap<User, UserDto>();
//     mc.CreateMap<Profil, ProfilDto>();
//     mc.CreateMap<Personne, PersonneDto>();
//     mc.CreateMap<Candidature, CandidatureDto>();
//     mc.CreateMap<Adresse, AdresseDto>();
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
