using System.Text.Json.Serialization;

namespace NetBot.Models;

public class JokeApiResponse
{
    [JsonConstructor]
    public JokeApiResponse(
        bool error,
        string category,
        string type,
        string? joke,
        string? setup,
        string? delivery,
        Flags flags,
        int id,
        bool safe,
        string lang
    )
    {
        Error = error;
        Category = category;
        Type = type;
        Joke = joke;
        Setup = setup;
        Delivery = delivery;
        Flags = flags;
        Id = id;
        Safe = safe;
        Lang = lang;
    }

    [JsonPropertyName("error")]
    public bool Error { get; }

    [JsonPropertyName("category")]
    public string Category { get; }

    [JsonPropertyName("type")]
    public string Type { get; }

    [JsonPropertyName("joke")]
    public string? Joke { get; }
    
    [JsonPropertyName("setup")]
    public string? Setup { get; }

    [JsonPropertyName("delivery")]
    public string? Delivery { get; }

    [JsonPropertyName("flags")]
    public Flags Flags { get; }

    [JsonPropertyName("id")]
    public int Id { get; }

    [JsonPropertyName("safe")]
    public bool Safe { get; }

    [JsonPropertyName("lang")]
    public string Lang { get; }
}