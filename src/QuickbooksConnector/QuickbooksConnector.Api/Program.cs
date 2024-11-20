using Microsoft.AspNetCore.Server.Kestrel.Core;
using QuickbooksConnector.Api.Filters;
using QuickbooksConnector.Services.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var httpsConfig = builder.Configuration.GetSection("Kestrel:Https");
var certPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, httpsConfig["CertificatePath"]));
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(Int32.Parse(httpsConfig["Port"]), listenOptions =>
    {
        listenOptions.UseHttps(
            certPath,
            httpsConfig["CertificatePassword"]);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
