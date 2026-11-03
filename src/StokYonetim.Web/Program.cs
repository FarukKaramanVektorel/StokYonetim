using StokYonetim.Inf;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionStrings = builder.Configuration.GetConnectionString("StokDb")
    ?? throw new InvalidOperationException("StokDb bağlantı cümlesi bulunamadı");

builder.Services.AddInf(connectionStrings);

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
