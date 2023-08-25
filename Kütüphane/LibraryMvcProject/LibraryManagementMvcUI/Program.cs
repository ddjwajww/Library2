using LibraryManagementMvcUI.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

//Aþaðýdaki ikili IoC ye, Web servisler ile haberleþmek için bize lazým olan HttpClient nesnesi üretmek ve
//HttpApiService nesnesi üretmek için direktiflerini vermiþ oluyor
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
