using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using System.Net;
using System.Text.Json;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("dog")]
    public class DogController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;

        public DogController(ILogger<UniversityController> logger)
        {
            _logger = logger;
        }

        [Produces("application/json")]
        [HttpGet("/facts")]
        public async Task<IActionResult> GetDogFacts ()
        {
            // Create HTTPClient
            HttpClient client = new HttpClient();

            // Define endpoint
            string API_Url = "https://dogapi.dog/api/v2/facts";

            try
            {
                // Send GET Request
                var response = await client.GetAsync(API_Url);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    // Read and display response
                    string responseJsonString = await response.Content.ReadAsStringAsync();
                    var DogFact = JsonSerializer.Deserialize<DogBreedsList>(responseJsonString) ?? new DogBreedsList();

                    // Dispose HTTPClient
                    client.Dispose();

                    return Ok(DogFact);

                }
                else
                {
                    throw new Exception("IsSuccessfulStatusCode = False");
                }
            }
            catch (Exception ex)
            {
                // Dispose HTTPClient
                client.Dispose();

                // Return error response
                var errorResponse = ex.Message;

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);

            }
        }
    }
}
