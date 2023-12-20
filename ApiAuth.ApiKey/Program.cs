using ApiAuth.ApiKey.Filters;
using ApiAuth.ApiKey.Middleware;
using ApiAuth.ApiKey.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// api key using middware
//app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapGet("api/test", () =>
//{
//    return Results.Ok(new { success = true });
//}).AddEndpointFilter<ApiKeyEndpointFilter>(); 

app.MapControllers();

app.Run();
