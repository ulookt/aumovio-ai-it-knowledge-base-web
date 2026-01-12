using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AiItKnowledgeBase.Web.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"]
                      ?? throw new Exception("OpenAI API key not configured.");
        }

        public async Task<string> GetAnswerAsync(string question, string context)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { 
                        role = "system", 
                        content = 
                            "You are an IT support assistant. Answer ONLY using the provided context. " +
                            "If the answer is not available in the context, clearly state that and advise contacting IT support. " +
                            "Provide a helpful, detailed response suitable for an end user. " +
                            "When appropriate, include step-by-step instructions or troubleshooting guidance."
                    },
                    new
                    {
                        role = "user",
                        content =
                            "Context:\n" +
                            context +
                            "\n\nQuestion:\n" +
                            question +
                            "\n\nAnswer in a clear and concise manner."                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);

            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()!;
        }
    }
}
