using Microsoft.EntityFrameworkCore;
using PnsApp.Maui.Data;
using PnsApp.WebApi.PomocneTridy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<TransientTrida>();
builder.Services.AddSingleton<SingletonTrida>();
builder.Services.AddScoped<ScopeTrida>();


builder.Services.AddDbContext<AppDbContext>((optionsBuilder) =>
{
    optionsBuilder.UseSqlServer("Server=192.168.1.57;Database=LuRaMi;User Id=lurami_user;Password=lurami_user;TrustServerCertificate=True");
    //            optionsBuilder.UseSqlServer("Server=192.168.88.209;Database=LuRaMi3;User Id=lurami_user;Password=lurami;Persist Security Info=True;Encrypt=True;TrustServerCertificate=True");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
