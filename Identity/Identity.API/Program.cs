using Identity.API.Middlewares;
using Carter;
using Identity.API.DependencyConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigMasstransitRabbitMQ(builder.Configuration);
DependencyConfig.Register(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<ApiUserMiddleware>();
app.UseMiddleware<ApiResponseMiddleware>();

app.MapCarter();

app.Run();
