using System.Text.Json.Serialization;

namespace NetBot.Models;

public class Flags
{
    [JsonConstructor]
    public Flags(
        bool nsfw,
        bool religious,
        bool political,
        bool racist,
        bool sexist,
        bool @explicit
    )
    {
        Nsfw = nsfw;
        Religious = religious;
        Political = political;
        Racist = racist;
        Sexist = sexist;
        Explicit = @explicit;
    }

    [JsonPropertyName("nsfw")]
    public bool Nsfw { get; }

    [JsonPropertyName("religious")]
    public bool Religious { get; }

    [JsonPropertyName("political")]
    public bool Political { get; }

    [JsonPropertyName("racist")]
    public bool Racist { get; }

    [JsonPropertyName("sexist")]
    public bool Sexist { get; }

    [JsonPropertyName("explicit")]
    public bool Explicit { get; }
}