using BadAdvisor.Mvc.Data;
using BadAdvisor.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

/*builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var builtConfig = config.Build();

    var appConfigConnectionString = builtConfig[];
    if (!string.IsNullOrEmpty(appConfigConnectionString))
    {
        config.AddAzureAppConfiguration(options =>
            options.Connect(appConfigConnectionString));
    }
});*/


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<ISanitizerService, SanitizerService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
