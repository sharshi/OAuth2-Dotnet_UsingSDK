using QB.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<QboAuthTokens>(setting =>
{
    string connectionString = builder.Configuration.GetConnectionString("APP_CONFIG_CONNECTIONSTRING");
    
    builder.Configuration.AddAzureAppConfiguration(connectionString);
    #if DEBUG
    builder.Configuration.AddJsonFile("appsettings.json");
    #endif
    builder.Configuration.GetSection("QB").Bind(setting);
});  

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();
