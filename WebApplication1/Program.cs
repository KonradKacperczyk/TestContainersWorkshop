using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestcontainersApi;
using TestcontainersApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(SampleController).Assembly);

builder.Services.AddOptions<MongoDbOptions>().BindConfiguration("MongoDb");
builder.Services.TryAddSingleton<IMongoClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MongoDbOptions>>();
    var optionsInstance = options.Value;
    var mongoClient = new MongoClient(new MongoUrl(optionsInstance.Uri));

    return mongoClient;
});

var app = builder.Build();

app.UseCors();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

public partial class Program { }