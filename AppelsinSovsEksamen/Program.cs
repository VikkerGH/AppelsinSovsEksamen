using Domain.Persistence;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".AppelsinSovs.Session";
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Generisk repository til alle modeller (User, Product, HighScore, Kategori osv.)
builder.Services.AddScoped(typeof(IRepository<>), typeof(InDatabasePersist<>));
// Game får sit eget repository, da det skal hente Kategori med via Include
builder.Services.AddScoped<IRepository<Domain.Models.Game>, GameRepository>();

builder.Services.AddScoped<Domain.Services.ProductService>();
builder.Services.AddScoped<HighScoreService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<Domain.Services.UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
