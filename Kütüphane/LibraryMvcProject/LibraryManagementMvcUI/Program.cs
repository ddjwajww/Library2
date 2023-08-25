using LibraryManagementMvcUI.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

//A�a��daki ikili IoC ye, Web servisler ile haberle�mek i�in bize laz�m olan HttpClient nesnesi �retmek ve
//HttpApiService nesnesi �retmek i�in direktiflerini vermi� oluyor
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpApiService, HttpApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "adminPanelDefault",
    areaName: "AdminPanel",
    pattern: "{area}/{controller=Auth}/{action=LogIn}/{id?}"
    );

app.Run();
