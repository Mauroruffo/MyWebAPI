using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.Json;
using MyWebAPI.Models.Request;
using MyWebAPI.Models.Response;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("university")]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;

        public UniversityController(ILogger<UniversityController> logger)
        {
            _logger = logger;
        }

        [Produces("application/json")]
        [HttpGet("get_universities")]
        public async Task<IActionResult> GetUniversities([FromQuery] UniversityRequest request, CancellationToken cancellationToken)
        {
            // Create a new HttpClient instance
            HttpClient httpClient = new HttpClient();

            // Define the API endpoint
            string ApiURL = "http://universities.hipolabs.com/search";

            var queryParams = new Dictionary<string, string?>
            {
                { "name", request.Name },
                { "country", request.Country}
            };

            // Add queryParams to ApiURL
            string ApiURL_WithQuery = QueryHelpers.AddQueryString(ApiURL, queryParams);

            try
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "UniversitiesAPI");
                // if using auth header JWT:
                // httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "Your Oauth Token");

                // Send GET request
                var response = await httpClient.GetAsync(ApiURL_WithQuery, cancellationToken);

                // If POST then
                // var response = await httpClient.PostAsync(ApiURL, content, cancellationToken);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    // Read and display the response
                    string responseJsonString = await response.Content.ReadAsStringAsync(cancellationToken);
                    var ListUniversities = JsonSerializer.Deserialize<List<UniversityResponse>> (responseJsonString) ?? new List<UniversityResponse>();

                    // Create parent Json
                    var GenResponse = new GenericResponse<UniversityResponse>()
                    {
                        Middleware = "UniversitiesAPI",
                        Entity = ListUniversities
                    };

                    // Dispose HttpClient
                    httpClient.Dispose();

                    return Ok(GenResponse);

                }
                else
                {
                    throw new Exception("IsSuccesfulStatusCode = False");
                }

            }
            catch (Exception ex) 
            {
                // Dispose HttpClient
                httpClient.Dispose();

                // Return error response
                var errorResponse = ex.Message;

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

    }
}
