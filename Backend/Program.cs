using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using MySql.EntityFrameworkCore.Extensions;
using Model;
using Service;
using System;
using System.Configuration;

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
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<TopicContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();


//Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.

builder.Services.Configure<JsonOptions>(options =>
{
     //Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
     //der refererer til hinanden i en cykel.
     //(altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


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

app.MapGet("/api/topic/{id}", (DataService service, int id) =>
{
    return service.GetTopic(id);
});

app.MapPost("/api/topics", (DataService service, TopicDTO data) =>
{
    return service.CreateTopic(data.title, data.description, data.user, data.date, data.votes);
});

//COMMENTS
app.MapGet("/api/{topicid}/comments", (DataService service, int topicid) =>
{
    return service.GetComments(topicid);
});

app.MapGet("/api/topics/{topicid}/comments/{commentid}", (DataService service, int topicid, int commentid) =>
{
    return service.GetComment(commentid);
});

app.MapPost("/api/comment", (DataService service, CommentDTO data) =>
{
    return service.CreateComment(data.description, data.user, data.topicid);
});

app.MapPut("/api/topic/{topicid}/upvote", (DataService service, int topicid) =>
{
    Console.WriteLine($"Received PUT request for topicid: {topicid}");
    var result = service.UpvoteTopic(topicid);
    Console.WriteLine($"Upvote result: {result}");
    return result;
});

app.MapPut("/api/topic/{topicid}/downvote", (DataService service, int topicid) =>
{
    Console.WriteLine($"Received PUT request for topicid: {topicid}");
    var result = service.DownvoteTopic(topicid);
    Console.WriteLine($"Downvote result: {result}");
    return result;
});

app.MapPut("/api/topic/{topicid}/comment/{commentid}/upvote", (DataService service, int commentid, int topicid) =>
{
    Console.WriteLine($"Received PUT request for commentid: {commentid}");
    var result = service.UpvoteComment(commentid, topicid);
    Console.WriteLine($"Upvote result: {result}");
    return result;
});

app.MapPut("/api/topic/{topicid}/comment/{commentid}/downvote", (DataService service, int commentid, int topicid) =>
{
    Console.WriteLine($"Received PUT request for commentid: {commentid}");
    var result = service.DownvoteComment(commentid, topicid);
    Console.WriteLine($"Downvote result: {result}");
    return result;
});


app.Run();

record CommentDTO(string description, string user, int votes, int topicid);
record TopicDTO(string title, string description, string user, DateTime date, int votes);