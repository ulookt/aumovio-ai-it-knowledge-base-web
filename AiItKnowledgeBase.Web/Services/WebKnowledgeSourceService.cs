using System.Text.RegularExpressions;

namespace AiItKnowledgeBase.Web.Services
{
    public class WebKnowledgeSourceService
    {
        private readonly HttpClient _httpClient;

        public WebKnowledgeSourceService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> LoadFromUrlAsync(string url)
        {
            var html = await _httpClient.GetStringAsync(url);

            var text = Regex.Replace(html, "<.*?>", string.Empty);
            text = Regex.Replace(text, @"\s+", " ").Trim();

            return text;
        }
    }
}
