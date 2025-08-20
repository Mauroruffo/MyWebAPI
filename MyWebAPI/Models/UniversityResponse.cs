using System.Text.Json.Serialization;

namespace MyWebAPI.Models.Response
{
    public class UniversityResponse
    {
        [JsonPropertyName("state-province")]
        public string StateProvince { get; set; }

        [JsonPropertyName("alpha_two_code")]
        public string AlphaTwoCode { get; set; }

        [JsonPropertyName("domains")]
        public List<string> Domains { get; set; }

        [JsonPropertyName("web-pages")]
        public List<string> WebPages { get; set; }

        [JsonPropertyName("country")]
        public string Country {  get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class GenericResponse<TEntity> where TEntity : class
    {
        public string Middleware { get; set; }

        public List<TEntity> Entity { get; set; }
    }
}
