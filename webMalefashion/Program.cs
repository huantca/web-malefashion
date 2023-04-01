using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using webMalefashion.Controllers;
using webMalefashion.Models;
using webMalefashion.Responsitory;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//phân loại memnu
var connectionString = builder.Configuration.GetConnectionString("MaleFashionContext");
builder.Services.AddDbContext<MalefashionContext>(s => s.UseSqlServer(connectionString));
builder.Services.AddScoped<ILoaiBrandResponsitory, LoaiBrandRespository>();
//csdl
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    options.JsonSerializerOptions.WriteIndented = true;
//});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Index}");

app.Run();
