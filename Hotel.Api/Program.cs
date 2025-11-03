using Hotel.Api;

var builder = WebApplication.CreateBuilder(args);
var config = new Config(builder.Configuration);

config.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
