//THIS CODE IS FROM HIS EXAMPLE
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Model;

namespace backend.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public async Task<Topic[]> GetTopics()
    {
        string url = $"{baseAPI}topics";
        return await http.GetFromJsonAsync<Topic[]>(url);
    }

    public async Task<Topic> GetTopic(int id)
    {
        string url = $"{baseAPI}topic/{id}";
        return await http.GetFromJsonAsync<Topic>(url);
    }

    public async Task<Comment[]> GetComments(int topicid)
    {
        string url = $"{baseAPI}{topicid}/comments";
        return await http.GetFromJsonAsync<Comment[]>(url);
    }

    public async Task<Topic> CreateTopic(string title, string description, string user, DateTime date, int votes)
    {
        string url = $"{baseAPI}topics";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { title, description, user, date, votes });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Topic? newTopic = JsonSerializer.Deserialize<Topic>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return newTopic;
    }


    public async Task<Comment> CreateComment(string content, int postId, int userId)
    {
        string url = $"{baseAPI}posts/{postId}/comments";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { content, userId });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return newComment;
    }

    //Topic upvote-downvote
    public async Task<Topic> UpvoteTopic(int topicid)
    {
        string url = $"{baseAPI}topic/{topicid}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Topic? updatedTopic = JsonSerializer.Deserialize<Topic>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updatedTopic;
    }

    public async Task<Topic> DownvoteTopic(int topicid)
    {
        string url = $"{baseAPI}topic/{topicid}/downvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Topic? updatedDownTopic = JsonSerializer.Deserialize<Topic>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
        // Return the updated post (vote increased)
        return updatedDownTopic;
    }

    //Comment upvote-downvote
    public async Task<Comment> UpvoteComment(int topicid, int commentid)
    {
        string url = $"{baseAPI}topic/{topicid}/comment/{commentid}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Comment? updatedComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updatedComment;
    }

    public async Task<Comment> DownvoteComment(int topicid, int commentid)
    {
        string url = $"{baseAPI}topic/{topicid}/comment/{commentid}/downvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Comment? updateDownComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updateDownComment;
    }

    public record CommentDTO(string description, string user, DateTime date, int votes, long topicid);
    public record TopicDTO(string title, string description, string user, DateTime date, int votes);
}
