using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using Model;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<TopicContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();

// Seed data hvis nødvendigt.
//using (var scope = app.Services.CreateScope())
//{
//    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
//    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
//}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


// DataService fås via "Dependency Injection" (DI)
app.MapGet("/", (DataService service) =>
{
    return new { message = "Hello World!" };
});

//TOPICS
app.MapGet("/api/topics", (DataService service) =>
{
    return service.GetTopics();
});

app.MapGet("/api/topics/{id}", (DataService service, int id) =>
{
    return service.GetTopic(id);
});

app.MapPost("/api/topics", (DataService service) =>
{
    return service.CreateTopic();
});

////COMMENTS
//app.MapGet("/api/topics/{id}/comments", (DataService service, int id) =>
//{
//    return service.GetComments(id);
//});

app.Run();
