using System.Text.Json.Serialization;

namespace MyWebAPI.Models
{
    public class DogBreedsList
    {
        [JsonPropertyName("data")]
        public List<DogBreeds> dogBreedsList { get; set; }
    }

    public class DogBreeds
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attributes")]
        public DogBreedFacts Attributes { get; set; }

    }

    public class DogBreedFacts
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }

    }

}
