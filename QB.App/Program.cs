using QB.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAzureAppConfiguration();
builder.Services.Configure<QboAuthTokens>(setting =>
{  
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
