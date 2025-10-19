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


app.MapGet("/me", async (IHttpClientFactory httpClientFactory) =>
{
    var catFact = await GetRandomCatFactAsync(httpClientFactory);

    var response = new UserResponse
    {
        Fact = catFact.Fact
    };

    return Results.Ok(response);
});


app.Run();

// get random cat fact from https://catfact.ninja/fact
static async Task<CatFact> GetRandomCatFactAsync(IHttpClientFactory httpClientFactory)
{
    try
    {
        using var client = httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<CatFact>("https://catfact.ninja/fact")
         ?? CatFact.Fallback;
    }
    catch (Exception)
    {
        return CatFact.Fallback;
    }
}