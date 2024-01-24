using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class GravatarService
{
    private readonly HttpClient _httpClient;

    public GravatarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GravatarData> GetGravatarDataAsync(string email)
    {
        try
        {
            var apiUrl = $"https://gravatar.com/{email}.json";

            var response = await _httpClient.GetAsync(apiUrl);

            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // not found for the email
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var gravatarJson = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<GravatarData>(gravatarJson);
            }

            // I would usually log anyother unexprected codes here
            return null; 
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request exceptions 
            // I would use something like polly for retries usually
            throw new Exception("Error fetching Gravatar data. Check your internet connection.", ex);
        }
        catch (JsonException ex)
        {
            // Handle JSON parsing exceptions
            throw new Exception("Error parsing Gravatar JSON response.", ex);
        }
        catch (Exception ex)
        {
            // Handle other unexpected exceptions
            throw new Exception("An unexpected error occurred while fetching Gravatar data.", ex);
        }
    }

    public class GravatarData
    {
        [JsonPropertyName("entry")]
        public List<GravatarEntry> Entries { get; set; }
    }

    public class GravatarEntry
    {
        [JsonPropertyName("profileUrl")]
        public string DisplayName { get; set; }

        [JsonPropertyName("name")]
        public GravatarName Name { get; set; }
    }

    public class GravatarName
    {
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }
    }
}
