using InternshipForm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InternshipForm.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connString));



// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
    pattern: "{controller=RegisterUser}/{action=UserRegister}/{id?}");

app.Run();
