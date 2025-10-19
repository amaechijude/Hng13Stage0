using Hng13Stage0;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/me", async (IHttpClientFactory httpClientFactory, ILogger<Program> logger) =>
{
    var catFact = await GetRandomCatFactAsync(httpClientFactory, logger);

    var response = new UserResponse
    {
        Fact = catFact.Fact
    };

    return Results.Ok(response);
});


app.Run();

// get random cat fact from https://catfact.ninja/fact
static async Task<CatFact> GetRandomCatFactAsync(IHttpClientFactory httpClientFactory, ILogger logger)
{
    try
    {
        using var client = httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<CatFact>("https://catfact.ninja/fact")
         ?? CatFact.Fallback;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to fetch cat fact.");
        return CatFact.Fallback;
    }
}